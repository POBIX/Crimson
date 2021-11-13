using GLFW;
using GLMouse = GLFW.MouseButton;

namespace Crimson;

public enum Key
{
    Unknown = Keys.Unknown,
    Space = Keys.Space,
    Apostrophe = Keys.Apostrophe,
    Comma = Keys.Comma,
    Minus = Keys.Minus,
    Period = Keys.Period,
    Slash = Keys.Slash,

    R0 = Keys.Alpha0,
    R1 = Keys.Alpha1,
    R2 = Keys.Alpha2,
    R3 = Keys.Alpha3,
    R4 = Keys.Alpha4,
    R5 = Keys.Alpha5,
    R6 = Keys.Alpha6,
    R7 = Keys.Alpha7,
    R8 = Keys.Alpha8,
    R9 = Keys.Alpha9,

    Semicolon = Keys.SemiColon,
    Equals = Keys.Equal,

    A = Keys.A,
    B = Keys.B,
    C = Keys.C,
    D = Keys.D,
    E = Keys.E,
    F = Keys.F,
    G = Keys.G,
    H = Keys.H,
    I = Keys.I,
    J = Keys.J,
    K = Keys.K,
    L = Keys.L,
    M = Keys.M,
    N = Keys.N,
    O = Keys.O,
    P = Keys.P,
    Q = Keys.Q,
    R = Keys.R,
    S = Keys.S,
    T = Keys.T,
    U = Keys.U,
    V = Keys.V,
    W = Keys.W,
    X = Keys.X,
    Y = Keys.Y,
    Z = Keys.Z,
    LeftBracket = Keys.LeftBracket,
    Backslash = Keys.Backslash,
    RightBracket = Keys.RightBracket,
    Escape = Keys.Escape,
    Enter = Keys.Enter,
    Tab = Keys.Tab,
    Backspace = Keys.Backspace,
    Insert = Keys.Insert,
    Delete = Keys.Delete,
    Right = Keys.Right,
    Left = Keys.Left,
    Down = Keys.Down,
    Up = Keys.Up,
    PageUp = Keys.PageUp,
    PageDown = Keys.PageDown,
    Home = Keys.Home,
    End = Keys.End,
    CapsLock = Keys.CapsLock,
    ScrollLock = Keys.ScrollLock,
    NumLock = Keys.NumLock,
    PrintScreen = Keys.PrintScreen,
    PauseBreak = Keys.Pause,
    F1 = Keys.F1,
    F2 = Keys.F2,
    F3 = Keys.F3,
    F4 = Keys.F4,
    F5 = Keys.F5,
    F6 = Keys.F6,
    F7 = Keys.F7,
    F8 = Keys.F8,
    F9 = Keys.F9,
    F10 = Keys.F10,
    F11 = Keys.F11,
    F12 = Keys.F12,
    Pad0 = Keys.Numpad0,
    Pad1 = Keys.Numpad1,
    Pad2 = Keys.Numpad2,
    Pad3 = Keys.Numpad3,
    Pad4 = Keys.Numpad4,
    Pad5 = Keys.Numpad5,
    Pad6 = Keys.Numpad6,
    Pad7 = Keys.Numpad7,
    Pad8 = Keys.Numpad8,
    Pad9 = Keys.Numpad9,
    PadPeriod = Keys.NumpadDecimal,
    PadSlash = Keys.NumpadDivide,
    PadStar = Keys.NumpadMultiply,
    PadMinus = Keys.NumpadSubtract,
    PadPlus = Keys.NumpadAdd,
    PadEnter = Keys.NumpadEnter,
    PadEquals = Keys.NumpadEqual,
    Shift = Keys.LeftShift,
    Ctrl = Keys.LeftControl,
    Alt = Keys.LeftAlt,
    RShift = Keys.RightShift,
    RCtrl = Keys.RightControl,
    RAlt = Keys.RightAlt,
    Menu = Keys.Menu
}

public enum MouseButton
{
    Left = GLMouse.Left,
    Right = GLMouse.Right,
    Middle = GLMouse.Middle,
    Back = GLMouse.Button4,
    Forward = GLMouse.Button5,
}

internal struct KeyState
{
    public byte[] prev;
    public byte[] curr;
    public byte[] next;

    private int size;

    public KeyState(int size)
    {
        this.size = size;
        prev = new byte[size];
        curr = new byte[size];
        next = new byte[size];
    }

    public void Update()
    {
        Array.Copy(curr, prev, size);
        Array.Copy(next, curr, size);
    }
}

public abstract class InputBase
{
    internal KeyState state;
    internal KeyState update;
    internal KeyState frame;

    /// <param name="scancodeMax">The highest scancode in <typeparamref name="T"/></param>
    protected InputBase(int scancodeMax)
    {
        update = new(scancodeMax);
        frame = new(scancodeMax);
        state = update;
    }

    internal virtual void Init() { }

    internal virtual void Update() =>
        state.Update();
}

public abstract class InputBase<T> : InputBase where T : unmanaged, Enum
{
    protected static InputBase<T> Singleton { get; private set; }

    /// <inheritdoc cref="InputBase(int)"/>
    protected InputBase(int scancodeMax) : base(scancodeMax)
    {
        Singleton = this;
    }

    private static unsafe bool IsKeySet(byte[] arr, T key)
    {
        int k = *(int*)&key; // c# has some horrible generics. this is the best way to cast a generic enum to int.
        int index = k / 8; // get the index of the byte in the array that the key is in
        int bit = k % 8; // get its specific bit number
        return ((arr[index] >> bit) & 1) == 1; // check whether that bit is set
    }

    protected static unsafe void SetKey(byte[] arr, T key, bool value)
    {
        int k = *(int*)&key; // c# has some horrible generics. this is the best way to cast a generic enum to int.
        int index = k / 8; // get the index of the byte in the array that the key is in
        int bit = k % 8; // get its specific bit number
        int v = value ? 1 : 0;
        // modify the array to have that bit set to the correct value
        arr[index] = (byte)(arr[index] & ~(1 << bit) | (v << bit));
    }

    /// <summary> Is <paramref name="key"/> being held down? </summary>
    public static bool IsDown(T key) => IsKeySet(Singleton.state.curr, key);
    /// <summary> Is <paramref name="key"/> not held down? </summary>
    public static bool IsUp(T key) => !IsKeySet(Singleton.state.curr, key);
    /// <summary>
    /// Has <paramref name="key"/> just been pressed?
    /// Returns true only for the first frame in which the button is held down.
    /// </summary>
    public static bool IsPressed(T key) => IsKeySet(Singleton.state.curr, key) && !IsKeySet(Singleton.state.prev, key);
    /// <summary>
    /// Has <paramref name="key"/> just been released?
    /// Returns true only for the first frame in which the button is let go.
    /// </summary>
    public static bool IsReleased(T key) => !IsKeySet(Singleton.state.curr, key) && IsKeySet(Singleton.state.prev, key);
}

public static class Input
{
    private static List<InputBase> sources = new();

    private static Dictionary<string, List<Key>> actions = new();

    internal static void Register(InputBase i) => sources.Add(i);

    internal static void Init()
    {
        Register(new Keyboard());
        Register(new Mouse());

        foreach (InputBase i in sources)
            i.Init();
    }

    internal static void Update()
    {
        foreach (InputBase i in sources)
            i.Update();
    }

    internal static void SetUpdate()
    {
        foreach (InputBase i in sources)
            i.state = i.update;
    }

    internal static void SetFrame()
    {
        foreach (InputBase i in sources)
            i.state = i.frame;
    }

    public static void AddAction(string name, params Key[] keys)
    {
        if (actions.ContainsKey(name))
        {
            foreach (Key key in keys)
                actions[name].Add(key);
        }
        else actions.Add(name, keys.ToList());
    }

    public static bool IsDown(string action)
    {
        foreach (Key key in actions[action])
        {
            if (Keyboard.IsDown(key))
                return true;
        }
        return false;
    }

    public static bool IsUp(string action)
    {
        foreach (Key key in actions[action])
        {
            if (Keyboard.IsUp(key))
                return true;
        }
        return false;
    }

    public static bool IsPressed(string action)
    {
        foreach (Key key in actions[action])
        {
            if (Keyboard.IsPressed(key))
                return true;
        }
        return false;
    }

    public static bool IsReleased(string action)
    {
        foreach (Key key in actions[action])
        {
            if (Keyboard.IsReleased(key))
                return true;
        }
        return false;
    }
}

public class Keyboard : InputBase<Key>
{
    // this is a field in order to prevent garbage collection of the delegate.
    private static readonly KeyCallback Callback = InputCallback;

    internal Keyboard() : base(348) { }

    internal override void Init()
    {
        base.Init();
        Glfw.SetKeyCallback(Engine.handle, Callback);
    }

    private static void InputCallback(IntPtr window, Keys key, int scancode, InputState state, ModifierKeys mods)
    {
        SetKey(Singleton.update.next, (Key)key, state != InputState.Release);
        SetKey(Singleton.frame.next, (Key)key, state != InputState.Release);
    }
}

public class Mouse : InputBase<MouseButton>
{
    internal Mouse() : base(8) { }

    // this is a field in order to prevent garbage collection of the delegate.
    private static readonly MouseButtonCallback Callback = InputCallback;

    private static double x, y;

    /// <summary> The mouse's X position relative to the camera. </summary>
    public static float X => (float)x + Camera.Current?.Origin.x ?? GlobalX;

    /// <summary> The mouse's Y position relative to the camera. </summary>
    public static float Y => (float)y + Camera.Current?.Origin.y ?? GlobalY;

    /// <summary> The mouse's position relative to the camera. </summary>
    public static Vector2 Position => new(X, Y);

    /// <summary> The mouse's X position relative to the window. </summary>
    public static float GlobalX => (float)x;

    /// <summary> The mouse's Y position relative to the window. </summary>
    public static float GlobalY => (float)y;

    /// <summary> The mouse's position relative to the window. </summary>
    public static Vector2 GlobalPosition => new(GlobalX, GlobalY);

    internal override void Init()
    {
        base.Init();
        Glfw.SetMouseButtonCallback(Engine.handle, Callback);
    }

    private static void InputCallback(IntPtr window, GLMouse button, InputState state, ModifierKeys mods)
    {
        SetKey(Singleton.update.next, (MouseButton)button, state != InputState.Release);
        SetKey(Singleton.frame.next, (MouseButton)button, state != InputState.Release);
    }

    internal override void Update()
    {
        base.Update();
        Glfw.GetCursorPosition(Engine.handle, out x, out y);
    }

    public static void SetPosition(float x, float y) =>
        Glfw.SetCursorPosition(Engine.handle, x, y);

    public static void SetPosition(Vector2 p) => SetPosition(p.x, p.y);
}