namespace Crimson;

public abstract class SceneGenerator
{
    public Scene Scene { get; set; }
    /// <summary>
    /// The object you're currently editing.
    /// </summary>
    public static SceneObject Current { get; private set; }

    public abstract void Start();

    protected Entity Spawn(Action ent, Vector2 offset, string name = "")
    {
        SceneObject prev = Current;
        Entity e = new()
        {
            LocalPosition = offset,
            Name = name,
            Parent = prev
        };
        Current = e;
        Scene.AddObject(e);
        ent();
        Current = prev; // makes nested Spawns work (you'll keep editing the wrong object after exiting ent).
        return e;
    }

    protected Entity Spawn(Action ent, string name = "") => Spawn(ent, Vector2.Zero, name);
    protected Entity Spawn(Action ent, float x, float y, string name = "") => Spawn(ent, new(x, y), name);

    protected static T Comp<T>(Action<T> awake = null) where T : Component, new()
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
        Scene.AddObject(t);
        obj(t);
        Current = prev; // makes nested spawns work (you'll keep editing the wrong object after exiting the function).
        return t;
    }

    protected T Obj<T>(Action<T> obj, float x, float y) where T : SceneObject, new() => Obj(obj, new(x, y));

    protected static void Group(string name) => Current.Groups.Add(name);
}
