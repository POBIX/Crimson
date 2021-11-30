using GLFW;

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

public class Keyboard : ScancodeBase<Key>
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
        SetKey(((Keyboard)Singleton).update.next, (Key)key, state != InputState.Release);
        SetKey(((Keyboard)Singleton).frame.next, (Key)key, state != InputState.Release);
    }
}

public class KeyboardAction : InputActionBase<Key>
{
    public KeyboardAction(params Key[] keys) : base(keys) { }

    public override bool IsDown() => Check(Keyboard.IsDown);
    public override bool IsUp() => Check(Keyboard.IsUp);
    public override bool IsPressed() => Check(Keyboard.IsPressed);
    public override bool IsReleased() => Check(Keyboard.IsReleased);
}
