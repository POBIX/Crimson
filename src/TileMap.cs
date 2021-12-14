// TiledSharp is deprecated, but it provides support for collisions whereas TiledCS doesn't.

using OpenGL;
using TiledSharp;

namespace Crimson;

public class TileMap : Component
{
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

    Framebuffer fbo = new();
    Entity spriteEntity = new();

    public void Load(string mapFile)
    {
        TmxMap map = new(mapFile);

        foreach (TmxLayer layer in map.Layers)
        {
            foreach (TmxLayerTile tile in layer.Tiles)
            {
                // if empty tile
                if (tile.Gid == 0) continue;

                string GetPath(string file) =>
                    Path.IsPathFullyQualified(file) ? file : Path.Combine(Path.GetDirectoryName(mapFile) ?? ".", file);

                TmxTileset tileset = GetTileset(map, tile.Gid);

                var sprite = spriteEntity.AddComponent<Sprite>();
                sprite.Texture = new(GetPath(tileset.Image.Source));
                sprite.Clip = GetSourceRect(tileset, tile.Gid);
                sprite.Offset = new(
                    tile.X * map.TileWidth + (float)(layer.OffsetX ?? 0),
                    tile.Y * map.TileHeight + (float)(layer.OffsetY ?? 0)
                );
                sprite.FlipH = tile.HorizontalFlip;
                sprite.FlipV = tile.VerticalFlip;
                // RemoveComponent(sprite);

                // add the tile's colliders
                foreach (TmxObjectGroup group in tileset.Tiles[tile.Gid - 1].ObjectGroups)
                {
                    foreach (TmxObject obj in group.Objects)
                    {
                        var b = AddComponent<BoxCollider>();
                        b.Offset = sprite.Offset + new Vector2((float)obj.X, (float)obj.Y);
                        b.Size = new Vector2((float)obj.Width, (float)obj.Height);
                    }
                }
            }
        }

        Texture output = new(
            Mathf.Max(Engine.Width, map.Width * map.TileWidth),
            Mathf.Max(Engine.Height, map.Height * map.TileHeight)
        );
        Sprite s = AddComponent<Sprite>();
        s.Offset = output.Size / 2;
        s.Texture = output;
        s.FlipV = true;
        s.Start();

        output.Bind(0);
        fbo.AttachTexture(output, 0);

        spriteEntity.Material = spriteEntity.AddComponent<Material>();
        spriteEntity.Start();
        Material prevMat = Material.Current;
        spriteEntity.Material.Use();
        spriteEntity.Draw();
        prevMat?.Use();

        spriteEntity.Dispose();
    }
}
