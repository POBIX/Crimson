using System;
using System.Collections.Generic;
using GLFW;
using GLMouse = GLFW.MouseButton;

namespace Crimson
{
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

    public abstract class InputBase<T> where T : unmanaged, Enum
    {
        protected byte[] prevState;
        protected byte[] currState;
        protected byte[] nextState;

        protected InputBase(int scancodes)
        {
            prevState = new byte[scancodes];
            currState = new byte[scancodes];
            nextState = new byte[scancodes];
        }

        internal virtual void Update()
        {
            prevState = currState.Clone() as byte[];
            currState = nextState.Clone() as byte[];
        }

        private static bool IsKeySet(byte[] arr, T key)
        {
            // must cast to object then int because c# is a good language with truly excellent generics.
            int k = (int)(object)key;
            int index = k / 8; // get the index of the byte in the array that the key is in
            int bit = k % 8; // get its specific bit number
            return ((arr[index] >> bit) & 1) == 1; // check whether that bit is set
        }

        protected static void SetKey(byte[] arr, T key, bool value)
        {
            int k;
            unsafe
            {
                // C# has some truly excellent generics. If you want to get the actual int representation of the enum,
                // you either have to say "int k = (int)(object)key", which causes boxing, or do this.
                // thank you c#.
                k = *(int*)&key;
            }
            int index = k / 8; // get the index of the byte in the array that the key is in
            int bit = k % 8; // get its specific bit number
            int v = value ? 1 : 0;
            // modified the array to have that bit set to the correct value, i think
            // i don't know why this would work any more than you do.
            arr[index] = (byte)(arr[index] & ~(1 << bit) | (v << bit));
        }

        protected bool IsKeyDown(T key) => IsKeySet(currState, key);
        protected bool IsKeyUp(T key) => !IsKeySet(currState, key);
        protected bool IsKeyPressed(T key) => IsKeySet(currState, key) && !IsKeySet(prevState, key);
        protected bool IsKeyReleased(T key) => !IsKeySet(currState, key) && IsKeySet(prevState, key);
    }

    public class Keyboard : InputBase<Key>
    {
        // 44 because we have 348 scancodes.
        // we use each bit as a bool, 348 / 8 = 43.5 -> 44.
        private Keyboard() : base(44) =>
            Glfw.SetKeyCallback(Engine.handle, callback);

        // this field exists because of a "callback was made on a garbage collected delegate" exception
        private KeyCallback callback = InputCallback;

        internal static Keyboard USingleton { get; private set; }
        internal static Keyboard FSingleton { get; private set; }
        internal static Keyboard Singleton { get; set; }

        internal static void Init()
        {
            USingleton = new Keyboard();
            FSingleton = new Keyboard();
        }

        internal override void Update()
        {
            base.Update();
            Singleton = this;
        }

        private static void InputCallback(IntPtr window, Keys key, int scancode, InputState state, ModifierKeys mods)
        {
            SetKey(USingleton.nextState, (Key)key, state != InputState.Release);
            SetKey(FSingleton.nextState, (Key)key, state != InputState.Release);
        }

        public static bool IsDown(Key key) => Singleton.IsKeyDown(key);
        public static bool IsUp(Key key) => Singleton.IsKeyUp(key);
        public static bool IsPressed(Key key) => Singleton.IsKeyPressed(key);
        public static bool IsReleased(Key key) => Singleton.IsKeyReleased(key);
    }

    public class Mouse : InputBase<MouseButton>
    {
        // 1 because we have 8 scancodes. we use each bit as a bool, 8 / 8 = 1.
        private Mouse() : base(1) =>
            Glfw.SetMouseButtonCallback(Engine.handle, callback);

        // this field exists because of a "callback was made on a garbage collected delegate" exception
        private MouseButtonCallback callback = InputCallback;

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

        internal static Mouse FSingleton { get; private set; }
        internal static Mouse USingleton { get; private set; }
        internal static Mouse Singleton { get; set; }

        internal static void Init()
        {
            FSingleton = new Mouse();
            USingleton = new Mouse();
        }

        private static void InputCallback(IntPtr window, GLMouse button, InputState state, ModifierKeys mods)
        {
            SetKey(FSingleton.nextState, (MouseButton)button, state != InputState.Release);
            SetKey(USingleton.nextState, (MouseButton)button, state != InputState.Release);
        }

        public static bool IsDown(MouseButton key) => Singleton.IsKeyDown(key);
        public static bool IsUp(MouseButton key) => Singleton.IsKeyUp(key);
        public static bool IsPressed(MouseButton key) => Singleton.IsKeyPressed(key);
        public static bool IsReleased(MouseButton key) => Singleton.IsKeyReleased(key);

        internal override void Update()
        {
            base.Update();
            Glfw.GetCursorPosition(Engine.handle, out x, out y);
            Singleton = this;
        }
    }
    //
    // public static class Input
    // {
    //     private struct Button<T> where T : unmanaged, Enum
    //     {
    //         public T code;
    //         public InputBase<T> input;
    //     }
    //
    //     private static Dictionary<string, List<Button<object>>> actions = new();
    //
    //     public static void AddAction(string name, Key key)
    //     {
    //         if (actions.ContainsKey(name)) actions[name]
    //         actions.Add(name, key);
    //     }
    //
    //     public static bool IsDown(string name)
    //     {
    //         return false;
    //     }
    // }
}
