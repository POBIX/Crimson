using System.Runtime.InteropServices;
using GLFW;

namespace Crimson;

public enum JoyButton
{
    A = GamePadButton.A,
    B = GamePadButton.B,
    X = GamePadButton.X,
    Y = GamePadButton.Y,

    LBumper = GamePadButton.LeftBumper,
    RBumper = GamePadButton.RightBumper,

    Select = GamePadButton.Back,
    Start = GamePadButton.Start,

    // GLFW seems to have the wrong codes starting here (tested on xbox one controller)
    LStick = 8,
    RStick = 9,

    DPUp = 10,
    DPRight = 11,
    DPDown = 12,
    DPLeft = 13,

    // playstation names
    Cross = A,
    Circle = B,
    Square = X,
    Triangle = Y,
    L1 = LBumper,
    R1 = RBumper,
    L3 = LStick,
    R3 = RStick
}

public enum JoyAxis
{
    LeftX = GamePadAxis.LeftX,
    LeftY = GamePadAxis.LeftY,
    RightX = GamePadAxis.RightX,
    RightY = GamePadAxis.RightY,
    LTrigger = GamePadAxis.LeftTrigger,
    RTrigger = GamePadAxis.RightTrigger,

    // playstation names
    L2 = LTrigger,
    R2 = RTrigger
}

public class Gamepad : InputBase<JoyButton>
{
    private class ButtonState
    {
        public InputState[] prev;
        public InputState[] curr;
        public InputState[] next;

        public ButtonState()
        {
            // initialize them all so that it doesn't crash on first read
            next = Glfw.GetJoystickButtons(Joystick.Joystick1);
            prev = curr = new InputState[next.Length];
            // both arrays point to the same address so there's no need to initialize both.
            for (int i = 0; i < next.Length; i++)
                curr[i] = InputState.Release;
        }

        public bool Down(JoyButton button) =>
            curr[(int)button] != InputState.Release;
        public bool Up(JoyButton button) =>
            curr[(int)button] == InputState.Release;
        public bool Pressed(JoyButton button) =>
            curr[(int)button] != InputState.Release && prev[(int)button] == InputState.Release;
        public bool Released(JoyButton button) =>
            curr[(int)button] == InputState.Release && prev[(int)button] != InputState.Release;

        public void Update()
        {
            prev = curr;
            curr = next;
            next = Glfw.GetJoystickButtons(Joystick.Joystick1);
        }
    }

    private class JoyState
    {
        public float[] prev;
        public float[] curr;
        public float[] next;

        public JoyState()
        {
            // initialize them all so that it doesn't crash on first read
            next = Glfw.GetJoystickAxes(Joystick.Joystick1);
            prev = curr = new float[next.Length];
            // both arrays point to the same address so there's no need to initialize both.
            for (int i = 0; i < next.Length; i++)
                curr[i] = 0;
        }

        public bool Down(JoyAxis axis, bool positive, float deadzone) =>
            positive ? curr[(int)axis] > deadzone : -curr[(int)axis] < -deadzone;

        public bool Up(JoyAxis axis, bool positive, float deadzone) =>
            positive ? curr[(int)axis] < deadzone : -curr[(int)axis] > -deadzone;

        public bool Pressed(JoyAxis axis, bool positive, float deadzone) =>
            (positive ? curr[(int)axis] > deadzone : -curr[(int)axis] < -deadzone) &&
            (positive ? prev[(int)axis] < deadzone : -prev[(int)axis] > -deadzone);

        public bool Released(JoyAxis axis, bool positive, float deadzone) =>
            (positive ? curr[(int)axis] < deadzone : -curr[(int)axis] > -deadzone) &&
            (positive ? prev[(int)axis] > deadzone : -prev[(int)axis] < -deadzone);

        public float Axis(JoyAxis axis) => curr[(int)axis];

        public void Update()
        {
            prev = curr;
            curr = next;
            next = Glfw.GetJoystickAxes(Joystick.Joystick1);

            /// set trigger ranges between 0 and 1 instead of -1 and 1
            next[(int)JoyAxis.LTrigger] = (next[(int)JoyAxis.LTrigger] + 1) / 2;
            next[(int)JoyAxis.RTrigger] = (next[(int)JoyAxis.RTrigger] + 1) / 2;
        }
    }

    private class State
    {
        public ButtonState buttons = new();
        public JoyState joy = new();

        public void Update()
        {
            buttons.Update();
            joy.Update();
        }
    }

    private static State frame = new();
    private static State update = new();
    private static State state = frame;

    protected internal override void Update()
    {
        base.Update();
        state.Update();
    }

    protected internal override void SetFrame() => state = frame;
    protected internal override void SetUpdate() => state = update;

    protected override bool IsDownImpl(JoyButton key) => state.buttons.Down(key);
    protected override bool IsUpImpl(JoyButton key) => state.buttons.Up(key);
    protected override bool IsPressedImpl(JoyButton key) => state.buttons.Pressed(key);
    protected override bool IsReleasedImpl(JoyButton key) => state.buttons.Released(key);

    public static float GetAxis(JoyAxis axis) => state.joy.Axis(axis);
    public static bool IsDown(JoyAxis axis, bool positive, float deadzone = 0.15f) =>
        state.joy.Down(axis, positive, deadzone);
    public static bool IsUp(JoyAxis axis, bool positive, float deadzone = 0.15f) =>
        state.joy.Up(axis, positive, deadzone);
    public static bool IsPressed(JoyAxis axis, bool positive, float deadzone = 0.15f) =>
        state.joy.Pressed(axis, positive, deadzone);
    public static bool IsReleased(JoyAxis axis, bool positive, float deadzone = 0.15f) =>
        state.joy.Released(axis, positive, deadzone);
}

public class JoyButtonAction : InputActionBase<JoyButton>
{
    public JoyButtonAction(params JoyButton[] keys) : base(keys) { }

    public override bool IsDown() => Check(Gamepad.IsDown);
    public override bool IsUp() => Check(Gamepad.IsUp);
    public override bool IsPressed() => Check(Gamepad.IsPressed);
    public override bool IsReleased() => Check(Gamepad.IsReleased);
}

public struct JoyValue
{
    public JoyAxis Axis { get; set; }
    public bool Positive { get; set; }
    public float Deadzone { get; set; }

    public JoyValue(JoyAxis axis, bool positive, float deadzone = 0.15f)
    {
        Axis = axis;
        Positive = positive;
        Deadzone = deadzone;
    }
}

public class JoyAxisAction : IInputAction
{
    private List<JoyValue> axes;

    public JoyAxisAction(params JoyValue[] axes) =>
        this.axes = axes.ToList();

    private bool Check(Func<JoyAxis, bool, float, bool> func)
    {
        foreach (JoyValue axis in axes)
        {
            if (func(axis.Axis, axis.Positive, axis.Deadzone))
                return true;
        }
        return false;
    }

    public bool IsDown() => Check(Gamepad.IsDown);
    public bool IsUp() => Check(Gamepad.IsUp);
    public bool IsPressed() => Check(Gamepad.IsPressed);
    public bool IsReleased() => Check(Gamepad.IsReleased);

    void IInputAction.Add(IEnumerable<object> axes) => Add(axes.Cast<JoyValue>());
    void IInputAction.Add(object axis) => Add((JoyValue)axis);
    public void Add(IEnumerable<JoyValue> axes) => this.axes.AddRange(axes);
    public void Add(params JoyValue[] axes) => Add((IEnumerable<JoyValue>)axes);
    public void Add(JoyValue axis) => axes.Add(axis);
}
