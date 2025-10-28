using System.Collections.ObjectModel;
using Crimson;
using GLMouse = GLFW.MouseButton;

namespace Crimson;

public interface IInputAction
{
    bool IsDown();
    bool IsUp();
    bool IsPressed();
    bool IsReleased();

    void Add(IEnumerable<object> keys);
    void Add(object key);
}

public abstract class InputActionBase<T> : IInputAction where T : unmanaged, Enum
{
    public List<T> keys;

    protected InputActionBase(T[] keys) =>
        this.keys = keys.ToList();

    protected bool Check(Func<T, bool> func)
    {
        foreach (T key in keys)
        {
            if (func(key))
                return true;
        }
        return false;
    }

    public abstract bool IsDown();
    public abstract bool IsUp();
    public abstract bool IsPressed();
    public abstract bool IsReleased();

    void IInputAction.Add(IEnumerable<object> keys) => Add(keys.Cast<T>());
    void IInputAction.Add(object key) => Add((T)key);
    public void Add(IEnumerable<T> keys) => this.keys.AddRange(keys);
    public void Add(params T[] keys) => Add((IEnumerable<T>)keys);
    public void Add(T key) => keys.Add(key);
}

public static class Input
{
    private static List<InputBase> handlers = new();

    public static ReadOnlyCollection<InputBase> Handlers => handlers.AsReadOnly();

    private static Dictionary<string, List<IInputAction>> actions = new();

    public static void AddHandler(InputBase i)
    {
        handlers.Add(i);
        i.Init();
    }

    internal static void Init()
    {
        AddHandler(new Keyboard());
        AddHandler(new Mouse());
        AddHandler(new Gamepad());
    }

    internal static void Update()
    {
        foreach (InputBase i in handlers)
            i.Update();
    }

    internal static void SetUpdate()
    {
        foreach (InputBase i in handlers)
            i.SetUpdate();
    }

    internal static void SetFrame()
    {
        foreach (InputBase i in handlers)
            i.SetFrame();
    }

    public static void AddAction(string name, params IInputAction[] keys)
    {
        if (actions.ContainsKey(name))
        {
            foreach (IInputAction a in keys)
                actions[name].Add(a);
        }
        else actions.Add(name, keys.ToList());
    }

    private static bool IsFunc(string action, Func<IInputAction, bool> func)
    {
        foreach (IInputAction a in actions[action])
        {
            if (func(a))
                return true;
        }
        return false;
    }

    public static bool IsDown(string action) => IsFunc(action, a => a.IsDown());
    public static bool IsUp(string action) => IsFunc(action, a => a.IsUp());
    public static bool IsPressed(string action) => IsFunc(action, a => a.IsPressed());
    public static bool IsReleased(string action) => IsFunc(action, a => a.IsReleased());
}
