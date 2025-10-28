namespace Crimson;

public abstract class InputBase
{
    public abstract Type Type { get; }

    internal virtual void Init() { }
    protected internal virtual void Update() { }

    protected internal virtual void SetUpdate() { }
    protected internal virtual void SetFrame() { }
}

public abstract class InputBase<T> : InputBase where T : unmanaged, Enum
{
    protected static InputBase<T> Singleton { get; private set; }

    protected InputBase() => Singleton = this;

    public override Type Type => typeof(T);

    protected abstract bool IsDownImpl(T key);
    protected abstract bool IsUpImpl(T key);
    protected abstract bool IsPressedImpl(T key);
    protected abstract bool IsReleasedImpl(T key);

    /// <summary> Is <paramref name="key"/> being held down? </summary>
    public static bool IsDown(T key) => Singleton.IsDownImpl(key);
    /// <summary> Is <paramref name="key"/> not held down? </summary>
    public static bool IsUp(T key) => Singleton.IsUpImpl(key);
    /// <summary>
    /// Has <paramref name="key"/> just been pressed?
    /// Returns true only for the first frame in which the button is held down.
    /// </summary>
    public static bool IsPressed(T key) => Singleton.IsPressedImpl(key);
    /// <summary>
    /// Has <paramref name="key"/> just been released?
    /// Returns true only for the first frame in which the button is let go.
    /// </summary>
    public static bool IsReleased(T key) => Singleton.IsReleasedImpl(key);
}

public abstract class ScancodeBase<T> : InputBase<T> where T : unmanaged, Enum
{
    protected struct State
    {
        public byte[] prev;
        public byte[] curr;
        public byte[] next;

        private int size;

        public State(int size)
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

    protected State state;
    protected State update;
    protected State frame;

    /// <param name="scancodeMax">The highest scancode in <typeparamref name="T"/></param>
    protected ScancodeBase(int scancodeMax)
    {
        update = new(scancodeMax);
        frame = new(scancodeMax);
        state = update;
    }

    protected internal override void Update() => state.Update();

    private static unsafe void GetIndex(T key, out int index, out int bit)
    {
        int k = *(int*)&key; // c# has some horrible generics. this is the best way to cast a generic enum to int.
        index = k / 8; // get the index of the byte in the array that the key is in
        bit = k % 8; // get its specific bit number
    }

    private static bool IsKeySet(byte[] arr, T key)
    {
        GetIndex(key, out int index, out int bit);
        return ((arr[index] >> bit) & 1) == 1; // check whether that bit is set
    }

    protected static void SetKey(byte[] arr, T key, bool value)
    {
        GetIndex(key, out int index, out int bit);
        int v = value ? 1 : 0;
        // modify the array to have that bit set to the correct value
        arr[index] = (byte)(arr[index] & ~(1 << bit) | (v << bit));
    }

    protected override bool IsDownImpl(T key) => IsKeySet(state.curr, key);
    protected override bool IsUpImpl(T key) => !IsKeySet(state.curr, key);
    protected override bool IsPressedImpl(T key) => IsKeySet(state.curr, key) && !IsKeySet(state.prev, key);
    protected override bool IsReleasedImpl(T key) => !IsKeySet(state.curr, key) && IsKeySet(state.prev, key);

    protected internal override void SetFrame() => state = frame;
    protected internal override void SetUpdate() => state = update;
}
