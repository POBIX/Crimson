using ImGuiNET;
using DotTiled;
using DotTiled.Serialization;
using Object = DotTiled.Object;
using TilesetTile = DotTiled.Tile;
using DTileLayer = DotTiled.TileLayer;
using DTileObject = DotTiled.TileObject;

namespace Crimson;

public struct TileLayer
{
    public string Name { get; init; }
    public float Opacity { get; init; }
    public bool Visible { get; init; }
    public Vector2 Offset { get; init; }
    public List<IProperty> Properties { get; init; }
    public List<Tile> Tiles { get; internal set; }
    public TileMap TileMap { get; init; }
}

public struct Tile
{
    public uint ID { get; init; }
    public Texture Texture { get; init; }
    public Vector2 WorldPosition { get; init; }
    public Vector2 Position { get; set; }
    public List<IProperty> Properties { get; init; }
    public TileLayer Layer { get; init; }
    public Rect Clip { get; init; }
    public BoxCollider[] Colliders { get; init; }
    public bool FlipH { get; init; }
    public bool FlipV { get; init; }
    public bool FlipD { get; init; }
}

public struct TileObject
{
    public uint ID { get; init; }
    public string Name { get; init; }
    public string Type { get; init; }
    public Vector2 Position { get; init; }
    public Vector2 Size { get; init; }
    public float Rotation { get; init; }
    public bool FlipH { get; init; }
    public bool FlipV { get; init; }
    public List<IProperty> Properties { get; init; }
}

public struct TileLabel
{
    public int ID { get; init; }
    public string Name { get; init; }
    public string Type { get; init; }
    public Vector2 Position { get; init; }
    public Vector2 Size { get; init; }
    public float Rotation { get; init; }
    public TextHorizontalAlignment HAlignment { get; init; }
    public TextVerticalAlignment VAlignment { get; init; }
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

    private Map map;

    public string MapFile { get; private set; }

    public TileLayer CustomTilesLayer { get; private set; }

    public List<IProperty> Properties { get; private set; }

    private static ComputeShader tileSetter;

    static TileMap()
    {
        tileSetter = new();
        tileSetter.AttachText(Resources.Read("shaders/set-tile.comp"));
        tileSetter.SetUniform("CAM_SIZE", Camera.CurrentResolution);
        Engine.Resize += (_, _) => tileSetter.SetUniform("CAM_SIZE", Camera.CurrentResolution);
    }

    private static Tileset GetTileset(Map map, uint gid)
    {
        foreach (Tileset t in map.Tilesets)
        {
            if (gid >= t.FirstGID.Value)
                return t;
        }
        return null;
    }

    private static Rect GetSourceRect(Tileset tileset, uint gid)
    {
        float x = 0, y = 0;

        for (int i = 0; i < tileset.TileCount; i++)
        {
            if (i == gid - tileset.FirstGID.Value)
                return new(x * tileset.TileWidth, y * tileset.TileHeight, tileset.TileWidth, tileset.TileHeight);

            if (++x == (float)tileset.Image.Value.Width.Value / tileset.TileWidth)
            {
                x = 0;
                y++;
            }
        }
        return default;
    }

    private IEnumerable<BoxCollider> GetColliders(TilesetTile tile, Vector2 offset, bool flipH, bool flipV, bool flipD)
    {
        if (tile == null) yield break;

        foreach (Object obj in tile.ObjectLayer.Value.Objects)
        {
            var b = AddComponent<BoxCollider>();

            b.Size = new Vector2(obj.Width, obj.Height);
            b.Offset = new(obj.X, obj.Y);
            if (flipD)
            {
                b.Size = new(b.Size.y, b.Size.x);
                Vector2 v = TileSize - b.Size;
                // has to be multiplied by 2 in y because ??
                b.Offset = v * new Vector2(1, 2) - new Vector2(b.Offset.y, b.Offset.x);
            }
            Vector2 s = b.Size;
            if (flipH) b.Offset = new(s.x - b.Offset.x, b.Offset.y);
            if (flipV) b.Offset = new(b.Offset.x, s.y - b.Offset.y);

            b.Offset += offset;

            yield return b;
        }
    }

    private void LoadTiles(Entity spriteEntity, string mapFile)
    {
        Dictionary<uint, Texture> textureCache = new();

        foreach (var layer in map.Layers.OfType<DTileLayer>())
        {
            List<Tile> layerTiles = new(layer.Data.Value.GlobalTileIDs.Value.Length);

            TileLayer tileLayer = new()
            {
                Name = layer.Name,
                Opacity = layer.Opacity,
                Visible = layer.Visible,
                Offset = new(layer.OffsetX, layer.OffsetY),
                Properties = layer.Properties,
                Tiles = layerTiles
            };

            for (int x = 0; x < layer.Width; x++)
            {
                for (int y = 0; y < layer.Height; y++)
                {
                    uint gid = layer.GetGlobalTileIDAtCoord(x, y);

                    // if empty tile
                    if (gid == 0) continue;

                    // flip flags are encoded inside the GID
                    const int h = 1 << 31, v = 1 << 30, d = 1 << 29, r = 1 << 28;
                    bool flipD = (gid & d) != 0;

                    var sprite = spriteEntity.AddComponent<Sprite>();
                    sprite.FlipH = (gid & h) != 0;
                    sprite.FlipV = (gid & v) != 0;
                    sprite.Rotation = flipD ? -Mathf.Pi / 2 : 0;

                    // we need to clear them to get the real GID
                    gid &= ~(h | v | d | r);

                    Tileset tileset = GetTileset(map, gid);

                    // load texture from cache/create it if non existent
                    if (!textureCache.TryGetValue(gid, out Texture tex))
                    {
                        // thank you for using absolute paths tiled
                        tex = new(Path.GetFullPath(
                            Path.Combine(Path.GetDirectoryName(mapFile) ?? "", Path.Combine(
                                Path.GetDirectoryName(tileset.Source.Value) ?? "", tileset.Image.Value.Source.Value)
                            ))
                        );
                        textureCache.Add(gid, tex);
                    }

                    sprite.Texture = tex;
                    sprite.Clip = GetSourceRect(tileset, gid);
                    sprite.Offset = new(
                        x * TileSize.x + layer.OffsetX + TileSize.x / 2,
                        y * TileSize.y + layer.OffsetY + TileSize.y / 2
                    );

                    BoxCollider[] colliders = GetColliders(
                        GetTile(tileset, gid), sprite.Offset, sprite.FlipH, sprite.FlipV, flipD
                    ).ToArray();

                    layerTiles.Add(new()
                    {
                        ID = gid,
                        Position = new(x, y),
                        WorldPosition = sprite.Offset,
                        Texture = tex,
                        Properties = tileset.Tiles.FirstOrDefault(t => t.ID == gid - tileset.FirstGID.Value)?.Properties,
                        Layer = tileLayer,
                        Clip = sprite.Clip,
                        Colliders = colliders,
                        FlipH = sprite.FlipH,
                        FlipV = sprite.FlipV,
                        FlipD = flipD
                    });
                }
            }

            Layers.Add(tileLayer);
                Tiles.AddRange(tileLayer.Tiles);
        }
        CustomTilesLayer = new TileLayer { Name = "Crimson#CustomTiles", Tiles = new(), TileMap = this };
        Layers.Add(CustomTilesLayer);
    }

    private void DrawTiles(Entity spriteEntity)
    {
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
    }

    private void LoadObjects()
    {
        bool addedLabels = false;
        ImGuiIOPtr io = ImGui.GetIO();
        foreach (ObjectLayer layer in map.Layers.OfType<ObjectLayer>())
        {
            foreach (Object obj in layer.Objects)
            {
                if (obj is TextObject text)
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
                        Font = io.Fonts.AddFontFromFileTTF(Engine.GUI.GetFontPath(text.FontFamily),
                            text.PixelSize),
                        Color = Color.Byte(text.Color.R, text.Color.G, text.Color.B),
                        Rotation = obj.Rotation,
                        Visible = obj.Visible,
                        Position = new(obj.X, obj.Y),
                        Size = new(obj.Width, obj.Height),
                        Text = text.Text,
                        HAlignment = text.HorizontalAlignment,
                        VAlignment = text.VerticalAlignment
                    });
                }
                else if (obj is DTileObject tile)
                {
                    Objects.Add(new TileObject
                    {
                        ID = tile.ID.Value,
                        Name = tile.Name,
                        Position = new(tile.X + TileSize.x / 2, tile.Y - TileSize.y / 2),
                        Properties = tile.Properties,
                        Rotation = tile.Rotation,
                        Size = new(tile.Width, tile.Height),
                        Type = tile.Type,
                        FlipH = (tile.FlippingFlags & FlippingFlags.FlippedHorizontally) != 0,
                        FlipV = (tile.FlippingFlags & FlippingFlags.FlippedVertically) != 0
                    });
                }
            }
        }
        if (addedLabels) Engine.GUI.RecreateFontDeviceTexture();
    }

    public void Load(string mapFile)
    {
        map = Loader.Default().LoadMap(mapFile);
        TileSize = new Vector2(map.TileWidth, map.TileHeight);
        MapSize = new Vector2(map.Width, map.Height);
        MapFile = mapFile;
        Properties = map.Properties;

        LoadObjects();

        // Zoom on cameras breaks stuff pretty badly.
        Camera prevCam = Camera.Current;
        Camera.Deactivate();

        Entity spriteEntity = new();
        Scene.AddObject(spriteEntity);
        LoadTiles(spriteEntity, mapFile);
        DrawTiles(spriteEntity);
        Scene.Destroy(spriteEntity);

        prevCam?.Activate();
    }

    private static TilesetTile GetTile(Tileset tileset, uint gid) =>
        tileset.Tiles.FirstOrDefault(v => v.ID == gid - tileset.FirstGID.Value);

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

    private static TilesetTile GetTileOrNull(Tileset tileset, uint id) =>
        tileset.Tiles.FirstOrDefault(t => t.ID == id - tileset.FirstGID.Value);

    private Tile ConstructTile(uint id, Vector2 mapCoords, TileLayer layer)
    {
        Tileset tileset = GetTileset(map, id);
        TilesetTile tile = GetTileOrNull(tileset, id);
        Vector2 coords = ToWorld(mapCoords);
        return new Tile
        {
            ID = id,
            Clip = GetSourceRect(tileset, id),
            Position = mapCoords,
            WorldPosition = coords,
            Texture = new(Path.GetFullPath(tileset.Image.Value.Source.Value)),
            Properties = tile?.Properties,
            Layer = layer,
            Colliders = GetColliders(tile, coords, false, false, false).ToArray(),
            FlipH = false,
            FlipV = false,
            FlipD = false
        };
    }

    public void SetTile(Vector2 mapCoords, uint id)
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
