using GLFW;
using GLMouse = GLFW.MouseButton;

namespace Crimson;

public enum MouseButton
{
    Left = GLMouse.Left,
    Right = GLMouse.Right,
    Middle = GLMouse.Middle,
    Back = GLMouse.Button4,
    Forward = GLMouse.Button5,
}

public class Mouse : ScancodeBase<MouseButton>
{
    internal Mouse() : base(8) { }

    // these are fields in order to prevent garbage collection of the delegates.
    private static readonly MouseButtonCallback Callback = InputCallback;
    private static readonly MouseCallback ScrollCallback = ScrollInputCallback;

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

    private class ScrollState
    {
        public float curr;
        public float next;

        public void Update()
        {
            curr = next;
            next = 0;
        }
    }

    private static ScrollState updateScroll = new();
    private static ScrollState frameScroll = new();
    private static ScrollState scroll = frameScroll;

    /// <summary> How much was the scroll wheel moved this frame? </summary>
    public static float Scroll => scroll.curr;

    internal override void Init()
    {
        base.Init();
        Glfw.SetMouseButtonCallback(Engine.handle, Callback);
        Glfw.SetScrollCallback(Engine.handle, ScrollCallback);
    }

    private static void ScrollInputCallback(IntPtr window, double x, double y) =>
        scroll.next = (float)y;

    private static void InputCallback(IntPtr window, GLFW.MouseButton button, InputState state, ModifierKeys mods)
    {
        SetKey(((Mouse)Singleton).update.next, (MouseButton)button, state != InputState.Release);
        SetKey(((Mouse)Singleton).frame.next, (MouseButton)button, state != InputState.Release);
    }

    protected internal override void Update()
    {
        base.Update();
        Glfw.GetCursorPosition(Engine.handle, out x, out y);
        scroll.Update();
    }

    public static void SetPosition(float x, float y) =>
        Glfw.SetCursorPosition(Engine.handle, x, y);

    public static void SetPosition(Vector2 p) => SetPosition(p.x, p.y);

    protected internal override void SetFrame()
    {
        base.SetFrame();
        scroll = frameScroll;
    }
    protected internal override void SetUpdate()
    {
        base.SetUpdate();
        scroll = updateScroll;
    }
}

public class MouseAction : InputActionBase<MouseButton>
{
    public MouseAction(params MouseButton[] keys) : base(keys) { }

    public override bool IsDown() => Check(Mouse.IsDown);
    public override bool IsUp() => Check(Mouse.IsUp);
    public override bool IsPressed() => Check(Mouse.IsPressed);
    public override bool IsReleased() => Check(Mouse.IsReleased);
}
