using TiledSharp; // TiledSharp is deprecated, but it provides support for collisions whereas TiledCS doesn't.

namespace Crimson;

public struct TileLayer
{
    public string Name { get; init; }
    public float Opacity { get; init; }
    public bool Visible { get; init; }
    public Vector2 Offset { get; init; }
    public IDictionary<string, string> Properties { get; init; }
    public List<Tile> Tiles { get; internal set; }
    public TileMap TileMap { get; init; }
}

public struct Tile
{
    public int ID { get; init; }
    public Texture Texture { get; init; }
    public Vector2 WorldPosition { get; init; }
    public Vector2 Position { get; set; }
    public IDictionary<string, string> Properties { get; init; }
    public TileLayer Layer { get; init; }
    public Rect Clip { get; init; }
    public BoxCollider[] Colliders { get; init; }
}

public class TileMap : Component
{
    public List<TileLayer> Layers { get; } = new();
    public List<Tile> Tiles { get; } = new();

    public Vector2 TileSize { get; private set; }
    public Vector2 MapSize { get; private set; }

    public Texture Texture { get; private set; }

    private TmxMap map;

    public string MapFile { get; private set; }

    public TileLayer CustomTilesLayer { get; private set; }

    private ComputeShader tileSetter;

    private static TmxTileset GetTileset(TmxMap map, int gid)
    {
        foreach (TmxTileset t in map.Tilesets)
        {
            if (gid >= t.FirstGid)
                return t;
        }
        return null;
    }

    private static Rect GetSourceRect(TmxTileset tileset, int gid)
    {
        float x = 0, y = 0;

        for (int i = 0; i < tileset.TileCount; i++)
        {
            if (i == gid - tileset.FirstGid)
                return new(x * tileset.TileWidth, y * tileset.TileHeight, tileset.TileWidth, tileset.TileHeight);

            if (++x == tileset.Image.Width / tileset.TileWidth)
            {
                x = 0;
                y++;
            }
        }
        return new();
    }

    private static string GetImagePath(string mapFile, string image) =>
        Path.IsPathFullyQualified(image) ? image : Path.Combine(Path.GetDirectoryName(mapFile) ?? ".", image);

    private IEnumerable<BoxCollider> GetColliders(TmxTilesetTile tile, Vector2 offset)
    {
        foreach (TmxObjectGroup group in tile.ObjectGroups)
        {
            foreach (TmxObject obj in group.Objects)
            {
                var b = AddComponent<BoxCollider>();
                b.Offset = offset + new Vector2((float)obj.X, (float)obj.Y);
                b.Size = new Vector2((float)obj.Width, (float)obj.Height);
                yield return b;
            }
        }
    }

    public void Load(string mapFile)
    {
        map = new(mapFile);
        TileSize = new(map.TileWidth, map.TileHeight);
        MapSize = new(map.Width, map.Height);
        MapFile = mapFile;

        Dictionary<int, Texture> textureCache = new();
        using Entity spriteEntity = new();

        foreach (TmxLayer layer in map.Layers)
        {
            List<Tile> layerTiles = new(layer.Tiles.Count);

            TileLayer tileLayer = new()
            {
                Name = layer.Name,
                Opacity = (float)layer.Opacity,
                Visible = layer.Visible,
                Offset = new((float)(layer.OffsetX ?? 0), (float)(layer.OffsetY ?? 0)),
                Properties = layer.Properties,
                Tiles = layerTiles
            };

            foreach (TmxLayerTile tile in layer.Tiles)
            {
                // if empty tile
                if (tile.Gid == 0) continue;

                TmxTileset tileset = GetTileset(map, tile.Gid);

                var sprite = spriteEntity.AddComponent<Sprite>();

                // load texture from cache/create it if non existent
                if (!textureCache.TryGetValue(tile.Gid, out Texture tex))
                {
                    tex = new(GetImagePath(mapFile, tileset.Image.Source));
                    textureCache.Add(tile.Gid, tex);
                }
                sprite.Texture = tex;

                sprite.Clip = GetSourceRect(tileset, tile.Gid);
                sprite.Offset = new(
                    tile.X * TileSize.x + (float)(layer.OffsetX ?? 0),
                    tile.Y * TileSize.y + (float)(layer.OffsetY ?? 0)
                );
                sprite.FlipH = tile.HorizontalFlip;
                sprite.FlipV = tile.VerticalFlip;

                BoxCollider[] colliders = GetColliders(tileset.Tiles[tile.Gid - 1], sprite.Offset).ToArray();

                layerTiles.Add(new()
                {
                    ID = tile.Gid,
                    Position = new(tile.X, tile.Y),
                    WorldPosition = sprite.Offset,
                    Texture = tex,
                    Properties = tileset.Tiles[tile.Gid - 1].Properties,
                    Layer = tileLayer,
                    Clip = sprite.Clip,
                    Colliders = colliders
                });
            }
            Layers.Add(tileLayer);
            Tiles.AddRange(tileLayer.Tiles);
        }
        CustomTilesLayer = new TileLayer { Name = "Crimson#CustomTiles", Tiles = new(), TileMap = this };
        Layers.Add(CustomTilesLayer);

        Texture = new(
            Mathf.Max(Engine.Width, (int)(MapSize.x * TileSize.x)),
            Mathf.Max(Engine.Height, (int)(MapSize.y * TileSize.y))
        );
        Sprite s = AddComponent<Sprite>();
        s.Offset = Texture.Size / 2;
        s.Texture = Texture;
        s.FlipV = true;
        s.Start();

        using Framebuffer fbo = new();
        Texture.Bind(0);
        fbo.AttachTexture(Texture, 0);

        spriteEntity.Material = spriteEntity.AddComponent<Material>();
        spriteEntity.Start();
        Material prevMat = Material.Current;
        spriteEntity.Material.Use();
        spriteEntity.Draw();
        prevMat?.Use();

        tileSetter = new();
        tileSetter.AttachText(Resources.Read("shaders/set-tile.comp"));
        tileSetter.SetUniform("SCREEN_SIZE", Engine.Size);
        Engine.Resize += (_, _) => tileSetter.SetUniform("SCREEN_SIZE", Engine.Size);
    }

    /// <summary>
    /// Returns the first tile at the given map coordinates, or null if tile is empty.
    /// </summary>
    public Tile? GetTile(Vector2 mapCoords)
    {
        foreach (Tile t in Tiles)
        {
            if (t.Position == mapCoords)
                return t;
        }
        return null;
    }

    /// <summary>
    /// Returns all tiles at the given map coordinates.
    /// </summary>
    public IEnumerable<Tile> GetTiles(Vector2 mapCoords)
    {
        foreach (Tile t in Tiles)
        {
            if (t.Position == mapCoords)
                yield return t;
        }
    }

    public Vector2 ToWorld(Vector2 mapCoords) => Position + mapCoords * TileSize;
    public Vector2 ToMap(Vector2 worldCoords) => (worldCoords - Position) / TileSize;

    private Tile ConstructTile(int id, Vector2 mapCoords, TileLayer layer)
    {
        TmxTileset tileset = GetTileset(map, id);
        TmxTilesetTile tile = tileset.Tiles[id - 1];
        Vector2 coords = ToWorld(mapCoords);
        return new Tile
        {
            ID = id,
            Clip = GetSourceRect(tileset, id),
            Position = mapCoords,
            WorldPosition = coords,
            Texture = new(GetImagePath(MapFile, tileset.Image.Source)),
            Properties = tile.Properties,
            Layer = layer,
            Colliders = GetColliders(tile, coords).ToArray()
        };
    }

    public void SetTile(Vector2 mapCoords, int id)
    {
        Tile? prev = GetTile(mapCoords);
        if (prev != null)
        {
            Tile p = prev.Value;
            p.Layer.Tiles.Remove(p);
            Tiles.Remove(p);
            foreach (BoxCollider c in p.Colliders)
                RemoveComponent(c);
        }

        tileSetter.SetUniform("POS", mapCoords * TileSize - TileSize / 2 * new Vector2(1, -1)); // y axis is flipped
        tileSetter.SetUniform("SIZE", TileSize);
        tileSetter.SetUniformImage("OUTPUT", Texture, BufferAccess.Write, 0);

        if (id != 0)
        {
            Tile t = ConstructTile(id, mapCoords, CustomTilesLayer);
            CustomTilesLayer.Tiles.Add(t);
            Tiles.Add(t);

            tileSetter.SetUniformImage("INPUT", t.Texture, BufferAccess.Read, 1);
            tileSetter.SetUniform("CLIP", t.Clip.Position);
            tileSetter.SetUniform("ERASE", false);
        }
        else tileSetter.SetUniform("ERASE", true);

        tileSetter.Dispatch(Engine.Width / 16, Engine.Height / 16, 1);
    }
}
