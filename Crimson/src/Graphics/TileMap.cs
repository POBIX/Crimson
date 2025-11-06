using ImGuiNET;
using TiledSharp; // TiledSharp is deprecated, but it provides support for collisions whereas TiledCS doesn't.

namespace Crimson;

public record struct TileLayer
{
    public string Name { get; init; }
    public float Opacity { get; init; }
    public bool Visible { get; init; }
    public Vector2 Offset { get; init; }
    public IDictionary<string, string> Properties { get; init; }
    public List<Tile> Tiles { get; internal set; }
    public TileMap TileMap { get; init; }
}

public record struct Tile
{
    public int ID { get; init; }
    public Texture Texture { get; init; }
    public Vector2 WorldPosition { get; init; }
    public Vector2 Position { get; set; }
    public IDictionary<string, string> Properties { get; init; }
    public TileLayer Layer { get; init; }
    public Rect Clip { get; init; }
    public BoxCollider[] Colliders { get; init; }
    public bool FlipH { get; init; }
    public bool FlipV { get; init; }
    public bool FlipD { get; init; }
}

public record struct TileObject
{
    public int ID { get; init; }
    public string Name { get; init; }
    public string Type { get; init; }
    public Vector2 Position { get; init; }
    public Vector2 Size { get; init; }
    public float Rotation { get; init; }
    public bool FlipH { get; init; }
    public bool FlipV { get; init; }
    public IDictionary<string, string> Properties { get; init; }
}

public record struct TileLabel
{
    public int ID { get; init; }
    public string Name { get; init; }
    public string Type { get; init; }
    public Vector2 Position { get; init; }
    public Vector2 Size { get; init; }
    public float Rotation { get; init; }
    public TmxAlignment Alignment { get; init; }
    public ImFontPtr Font { get; init; }
    public string Text { get; init; }
    public int FontSize { get; init; }
    public Color Color { get; init; }
    public IDictionary<string, string> Properties { get; init; }
    public bool Visible { get; init; }
}

public class TileMap : Component
{
    public List<Tile> Tiles { get; } = new();
    public List<TileLayer> Layers { get; } = new();
    public List<TileObject> Objects { get; } = new();
    public List<TileLabel> Labels { get; } = new();

    public Vector2 TileSize { get; private set; }
    public Vector2 MapSize { get; private set; }

    public Texture Texture { get; private set; }

    private TmxMap map;

    public string MapFile { get; private set; }

    public TileLayer CustomTilesLayer { get; private set; }

    public IDictionary<string, string> Properties { get; private set; }

    private static ComputeShader tileSetter;

    static TileMap()
    {
        tileSetter = new();
        tileSetter.AttachText(Resources.Read("shaders/set-tile.comp"));
        tileSetter.SetUniform("CAM_SIZE", Camera.CurrentResolution);
        Engine.Resize += (_, _) => tileSetter.SetUniform("CAM_SIZE", Camera.CurrentResolution);
    }

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

    private static string GetImagePath(string mapFile, string image)
    {
        if (Path.IsPathFullyQualified(image))
            return image;
        if (Path.GetDirectoryName(mapFile) == Path.GetDirectoryName(image))
            return image;
        return Path.Combine(Path.GetDirectoryName(mapFile) ?? ".", image);
    }

    private IEnumerable<BoxCollider> GetColliders(TmxTilesetTile tile, Vector2 offset, bool flipH, bool flipV, bool flipD)
    {
        if (tile == null) yield break;

        foreach (TmxObjectGroup group in tile.ObjectGroups)
        {
            foreach (TmxObject obj in group.Objects)
            {
                var b = AddComponent<BoxCollider>();

                b.Size = new Vector2((float)obj.Width, (float)obj.Height);
                b.Offset = new((float)obj.X, (float)obj.Y);
                if (flipD)
                {
                    b.Size = new(b.Size.y, b.Size.x);
                    Vector2 v = TileSize - b.Size;
                    // has to be multiplied by 2 in y because ??
                    b.Offset = v * new Vector2(1, 2) - new Vector2(b.Offset.y, b.Offset.x);
                }
                Vector2 s = TileSize - b.Size;
                if (flipH) b.Offset = new(s.x - b.Offset.x, b.Offset.y);
                if (flipV) b.Offset = new(b.Offset.x, s.y - b.Offset.y);

                b.Offset += offset - s / 2;

                yield return b;
            }
        }
    }

    private void LoadTiles(Entity spriteEntity, string mapFile)
    {
        Dictionary<int, Texture> textureCache = new();

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
                    tile.X * TileSize.x + (float)(layer.OffsetX ?? 0) + TileSize.x / 2,
                    tile.Y * TileSize.y + (float)(layer.OffsetY ?? 0) + TileSize.y / 2
                );
                sprite.FlipH = tile.HorizontalFlip;
                sprite.FlipV = tile.VerticalFlip;
                sprite.Rotation = tile.DiagonalFlip ? -Mathf.Pi / 2 : 0;

                BoxCollider[] colliders = GetColliders(
                    GetTile(tileset, tile.Gid), sprite.Offset, sprite.FlipH, sprite.FlipV, tile.DiagonalFlip
                ).ToArray();

                layerTiles.Add(new()
                {
                    ID = tile.Gid,
                    Position = new(tile.X, tile.Y),
                    WorldPosition = sprite.Offset,
                    Texture = tex,
                    Properties = tileset.Tiles.Values.FirstOrDefault(t => t.Id == tile.Gid - tileset.FirstGid)?.Properties ??
                                 new Dictionary<string, string>(),
                    Layer = tileLayer,
                    Clip = sprite.Clip,
                    Colliders = colliders,
                    FlipH = tile.HorizontalFlip,
                    FlipV = tile.VerticalFlip,
                    FlipD = tile.DiagonalFlip
                });
            }
            Layers.Add(tileLayer);
            Tiles.AddRange(tileLayer.Tiles);
        }
        CustomTilesLayer = new TileLayer { Name = "Crimson#CustomTiles", Tiles = new(), TileMap = this };
        Layers.Add(CustomTilesLayer);

    }

    private void DrawTiles(Entity spriteEntity)
    {
        spriteEntity.Material = spriteEntity.AddComponent<Material>();
        spriteEntity.Start();
        Material prevMat = Material.Current;
        spriteEntity.Material.Use();
        Texture = Framebuffer.Draw(
            Mathf.Max(Engine.Width, (int)(MapSize.x * TileSize.x)),
            Mathf.Max(Engine.Height, (int)(MapSize.y * TileSize.y)),
            spriteEntity.Draw
        );
        prevMat?.Use();

        Sprite s = AddComponent<Sprite>();
        s.Offset = Texture.Size / 2;
        s.Texture = Texture;
        s.FlipV = true;
        s.DisposeTexture = false;
        s.Start();
    }

    private void LoadObjects()
    {
        bool addedLabels = false;
        ImGuiIOPtr io = ImGui.GetIO();
        foreach (TmxObjectGroup group in map.ObjectGroups)
        {
            foreach (TmxObject obj in group.Objects)
            {
                if (obj.Text != null && obj.Text.Value != "")
                {
                    addedLabels = true;
                    // bool filter = false;
                    // if (obj.Properties.TryGetValue("Filter", out string f))
                    //     filter = bool.Parse(f);

                    // Scene.AddObject(new Label
                    // {
                    //     Font = new(Font.GetPath(obj.Text.FontFamily), obj.Text.PixelSize, filter),
                    //     Color = Color.Byte(obj.Text.Color.R, obj.Text.Color.G, obj.Text.Color.B),
                    //     Rotation = (float)obj.Rotation,
                    //     Hidden = !obj.Visible,
                    //     Position = new((float)obj.X, (float)obj.Y),
                    //     Size = new((float)obj.Width, (float)obj.Height),
                    //     Text = obj.Text.Value,
                    //     Wrap = obj.Text.Wrap,
                    //     HAlignment = GetHAlignment(obj.Text.Alignment.Horizontal),
                    //     VAlignment = GetVAlignment(obj.Text.Alignment.Vertical)
                    // });
                    Labels.Add(new TileLabel
                    {
                        Font = io.Fonts.AddFontFromFileTTF(Engine.GUI.GetFontPath(obj.Text.FontFamily), obj.Text.PixelSize),
                        Color = Color.Byte(obj.Text.Color.R, obj.Text.Color.G, obj.Text.Color.B),
                        Rotation = (float)obj.Rotation,
                        Visible = obj.Visible,
                        Position = new((float)obj.X, (float)obj.Y),
                        Size = new((float)obj.Width, (float)obj.Height),
                        Text = obj.Text.Value,
                        Alignment = obj.Text.Alignment
                    });
                }
                else
                {
                    Objects.Add(new TileObject
                    {
                        ID = obj.Id,
                        Name = obj.Name,
                        Position = new((float)obj.X + TileSize.x / 2, (float)obj.Y - TileSize.y / 2),
                        Properties = obj.Properties,
                        Rotation = (float)obj.Rotation,
                        Size = new((float)obj.Width, (float)obj.Height),
                        Type = obj.Type,
                        FlipH = obj.Tile.HorizontalFlip,
                        FlipV = obj.Tile.VerticalFlip
                    });
                }
            }
        }
        if (addedLabels) Engine.GUI.RecreateFontDeviceTexture();
    }

    public void Load(string mapFile)
    {
        map = new(mapFile);
        TileSize = new Vector2(map.TileWidth, map.TileHeight);
        MapSize = new Vector2(map.Width, map.Height);
        MapFile = mapFile;
        Properties = map.Properties;

        LoadObjects();

        // Zoom on cameras breaks stuff pretty badly.
        Camera prevCam = Camera.Current;
        Camera.Deactivate();

        using (Entity spriteEntity = new())
        {
            LoadTiles(spriteEntity, mapFile);
            DrawTiles(spriteEntity);
        }

        prevCam?.Activate();
    }

    private static TmxTilesetTile GetTile(TmxTileset tileset, int gid) =>
        tileset.Tiles.Values.FirstOrDefault(v => v.Id == gid - tileset.FirstGid);

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
    public Vector2 ToMap(Vector2 worldCoords) => ((worldCoords - Position) / TileSize).Ceil();

    private static TmxTilesetTile GetTileOrNull(TmxTileset tileset, int id) =>
        tileset.Tiles.FirstOrDefault(t => t.Key == id - tileset.FirstGid).Value;

    private Tile ConstructTile(int id, Vector2 mapCoords, TileLayer layer)
    {
        TmxTileset tileset = GetTileset(map, id);
        TmxTilesetTile tile = GetTileOrNull(tileset, id);
        Vector2 coords = ToWorld(mapCoords);
        return new Tile
        {
            ID = id,
            Clip = GetSourceRect(tileset, id),
            Position = mapCoords,
            WorldPosition = coords,
            Texture = new(GetImagePath(MapFile, tileset.Image.Source)),
            Properties = tile?.Properties,
            Layer = layer,
            Colliders = GetColliders(tile, coords, false, false, false).ToArray(),
            FlipH = false,
            FlipV = false,
            FlipD = false
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

        Vector2 zoom = Camera.CurrentResolution / Engine.Size;
        Vector2 size = TileSize * zoom;

        tileSetter.SetUniform("POS", mapCoords * size + new Vector2(0, size.y)); // y axis is flipped
        tileSetter.SetUniform("SIZE", size);
        tileSetter.SetUniform("OUTPUT", Texture, BufferAccess.Write, 0);

        if (id != 0)
        {
            Tile t = ConstructTile(id, mapCoords, CustomTilesLayer);
            CustomTilesLayer.Tiles.Add(t);
            Tiles.Add(t);

            tileSetter.SetUniform("INPUT", t.Texture, BufferAccess.Read, 1);
            tileSetter.SetUniform("CLIP", t.Clip.Position);
            tileSetter.SetUniform("ERASE", false);
        }
        else tileSetter.SetUniform("ERASE", true);

        tileSetter.Dispatch(Engine.Width / 16, Engine.Height / 16, 1, MemoryBarriers.ShaderImageAccess);
    }

    public override void Draw()
    {
        base.Draw();

        foreach (TileLabel label in Labels)
        {
            ImGui.SetNextWindowPos(Camera.TransformPoint(label.Position));
            ImGui.SetNextWindowSize(label.Size / (Camera.CurrentResolution / Engine.Size));
            Graphics.DrawRect(Camera.TransformPoint(label.Position) + label.Size / (Camera.CurrentResolution / Engine.Size) / 2, label.Size / (Camera.CurrentResolution / Engine.Size), Color.Yellow);
            ImGui.SetNextWindowBgAlpha(0);
            ImGui.Begin("Label", ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoBackground);

            ImGui.Text(label.Text);

            ImGui.End();
        }
    }
}
