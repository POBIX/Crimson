using System.Runtime.CompilerServices;

namespace Crimson;

public abstract class SceneGenerator
{
    /// <summary>
    /// The object you're currently editing.
    /// </summary>
    public SceneObject Current { get; private set; }

    public abstract SceneObject Root();

    protected Entity Spawn(Action ent, Vector2 offset, [CallerMemberName] string name = "")
    {
        SceneObject prev = Current;
        Entity e = new()
        {
            LocalPosition = offset,
            Name = name,
            Parent = prev
        };
        Current = e;
        ent();
        Current = prev; // makes nested Spawns work (you'll keep editing the wrong object after exiting ent).
        return e;
    }

    protected Entity Spawn(Action ent, [CallerMemberName] string name = "") => Spawn(ent, Vector2.Zero, name);
    protected Entity Spawn(Action ent, float x, float y, [CallerMemberName] string name = "") => Spawn(ent, new(x, y), name);

    protected T Comp<T>(Action<T> awake = null) where T : Component, new()
    {
        T t = ((Entity)Current).AddComponent<T>();
        awake?.Invoke(t);
        return t;
    }

    protected T Obj<T>(Action<T> obj, Vector2 offset = new()) where T : SceneObject, new()
    {
        SceneObject prev = Current;
        T t = new()
        {
            LocalPosition = offset,
            Parent = prev
        };
        Current = t;
        obj(t);
        Current = prev; // makes nested spawns work (you'll keep editing the wrong object after exiting the function).
        return t;
    }

    protected T Obj<T>(Action<T> obj, float x, float y) where T : SceneObject, new() => Obj(obj, new(x, y));

    protected void Group(string name) => Current.Groups.Add(name);

    protected void Map(string path)
    {
        Dictionary<string, Texture> textures = new();
        Dictionary<string, Color> colors = new();
        var lines = File.ReadAllLines(path);

        int i;
        for (i = 0; !lines[i].StartsWith("@map"); i++)
        {
            if (lines[i] == "") continue;
            string[] split = lines[i].Split('=');
            if (split[1][0] == '*')
                textures.Add(split[0], new Texture(split[1][1..]));
            else
                colors.Add(split[0], Color.Parse(split[1][1..]));
        }
        string[] sizeSplit = lines[i].Split(' ');
        int gridSize = int.Parse(sizeSplit[1]);
        Vector2 mapSize = new(int.Parse(sizeSplit[2]), int.Parse(sizeSplit[3]));
        string[] map = lines[++i].Split(',');

        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                string tile = map[(int)(y * mapSize.x + x)];
                if (tile == "0") continue;
                if (textures.TryGetValue(tile, out Texture texture))
                {
                    var s = ((Entity)Current).AddComponent<Sprite>();
                    s.Texture = texture;
                    s.DisposeTexture = false;
                    s.Offset = new(x * gridSize, y * gridSize);
                }
                else
                {
                    var r = ((Entity)Current).AddComponent<ColorRect>();
                    r.Offset = new(x * gridSize, y * gridSize);
                    r.Size = new(gridSize, gridSize);
                    r.Color = colors[tile];
                }
            }
        }
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class EditorAttribute : Attribute
{
    public string Name { get; set; }
    public Color Color { get; set; }
    public string Icon { get; set; }

    public EditorAttribute(Color color) => Color = color;
    public EditorAttribute() { }
}
