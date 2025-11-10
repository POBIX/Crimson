using System.Runtime.CompilerServices;

namespace Crimson;

public abstract class SceneGenerator
{
    /// <summary>
    /// The object you're currently editing.
    /// </summary>
    public SceneObject Current { get; private set; }

    public abstract SceneObject Get();

    protected Entity Spawn(Action ent, Vector2 offset, [CallerMemberName] string name = "") =>
        Obj<Entity>(ent, offset, name);

    protected Entity Spawn(Action ent, [CallerMemberName] string name = "") => Spawn(ent, Vector2.Zero, name);
    protected Entity Spawn(Action ent, float x, float y, [CallerMemberName] string name = "") => Spawn(ent, new(x, y), name);

    protected T Comp<T>(Action<T> awake = null) where T : Component, new()
    {
        T t = ((Entity)Current).AddComponent<T>();
        awake?.Invoke(t);
        return t;
    }

    protected T Obj<T>(Action obj, Vector2 offset = default, [CallerMemberName] string name = "") where T : SceneObject, new()
    {
        SceneObject prev = Current;
        T t = new()
        {
            LocalPosition = offset,
            Parent = prev,
            Name = name
        };
        Current = t;
        obj();
        Current = prev; // makes nested spawns work (you'll keep editing the wrong object after exiting the function).
        return t;
    }

    protected T Obj<T>(Action obj, float x, float y, [CallerMemberName] string name = "") where T : SceneObject, new() =>
        Obj<T>(obj, new(x, y), name);

    protected void Group(string name) => Current.Groups.Add(name);
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
