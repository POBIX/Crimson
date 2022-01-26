namespace Crimson;

using static MathF;

public class Tweener<T> : SceneObject
{
    public T Initial { get; private set; }
    public T Target { get; set; }
    private Action<T> set;

    private Func<float, float> easing = Easing.Linear;

    public float Duration { get; set; }
    public float Elapsed { get; private set; }

    public Scene Scene { get; private set; }
    public override void SetScene(Scene value) => Scene = value;

    private static Func<T, T, float, T> calculate;

    public event Action Finished;

    public static void Register(Func<T, T, float, T> equation) => calculate = equation;

    public Tweener(Action<T> setter, T initial, T target, float duration)
    {
        Initial = initial;
        Target = target;
        Duration = duration;
        set = setter;
    }

    public override void Start() { }
    public override void Update(float delta) { }
    public override void OnDestroy() { }
    public override void Frame(float delta)
    {
        Elapsed += delta;
        if (Elapsed >= Duration)
        {
            set(Target);
            if (Finished == null)
                Scene.Destroy(this);
            else Finished();
            Finished = null; // clear its subscriptions
        }
        else set(calculate(Initial, Target, easing(Elapsed / Duration)));

    }

    public Tweener<T> Ease(Func<float, float> func)
    {
        easing = func;
        return this;
    }

    public Tweener<T> Queue(T initial, T target, float duration)
    {
        Finished += () =>
        {
            Initial = initial;
            Target = target;
            Duration = duration;
            Elapsed = 0;
        };
        return this;
    }

    public Tweener<T> Reverse()
    {
        T temp = Initial;
        Initial = Target;
        Target = temp;
        return this;
    }

    public Tweener<T> QueueReverse() => Queue(Target, Initial, Duration);
}

public partial class Scene
{
    public Tweener<T> Tween<T>(Action<T> setter, T initial, T target, float duration)
    {
        Tweener<T> t = new(setter, initial, target, duration);
        AddObject(t);
        return t;
    }

    public static void InitTweens()
    {
        Tweener<float>.Register((a, b, t) => a + (b - a) * t);
        Tweener<double>.Register((a, b, t) => a + (b - a) * t);
        Tweener<decimal>.Register((a, b, t) => a + (b - a) * (decimal)t);
        Tweener<short>.Register((a, b, t) => (short)(a + (b - a) * t));
        Tweener<int>.Register((a, b, t) => (int)(a + (b - a) * t));
        Tweener<long>.Register((a, b, t) => (long)(a + (b - a) * t));
        Tweener<ushort>.Register((a, b, t) => (ushort)(a + (b - a) * t));
        Tweener<uint>.Register((a, b, t) => (uint)(a + (b - a) * t));
        Tweener<ulong>.Register((a, b, t) => (ulong)(a + (b - a) * t));
        Tweener<byte>.Register((a, b, t) => (byte)(a + (b - a) * t));
        Tweener<sbyte>.Register((a, b, t) => (sbyte)(a + (b - a) * t));
        Tweener<Vector2>.Register((a, b, t) => a + (b - a) * t);
        Tweener<Vector3>.Register((a, b, t) => a + (b - a) * t);
        Tweener<Vector4>.Register((a, b, t) => a + (b - a) * t);
        Tweener<Color>.Register((a, b, t) => a + (b - a) * t);
    }
}

/// <summary>
/// Mathematical easing functions, primarily to be used with <seealso cref="Tweener{T}"/>s.
/// Functions from https://easings.net/
/// </summary>
public static class Easing
{
    private const float Pi2 = PI / 2;

    /// <summary>https://www.desmos.com/calculator/irgr21v943</summary>
    public static float Linear(float t) => t;

    /// <summary>https://www.desmos.com/calculator/hw6bxwz8nu</summary>
    public static float ElasticIn(float t) =>
        Sin(13 * Pi2 * t) * Pow(2, 10 * (t - 1));

    /// <summary>https://www.desmos.com/calculator/jqvr9ei7qg</summary>
    public static float ElasticOut(float t) =>
        Sin(-13 * Pi2 * (t + 1)) * Pow(2, -10 * t) + 1;

    /// <summary>https://www.desmos.com/calculator/8liwb1kmiu</summary>
    public static float ElasticInOut(float t)
    {
        if (t is 0 or 1) return t;
        const float c = 2 * PI / 4.5f;
        return t < 0.5 ?
            -Pow(2, 20 * t - 10) * -Sin((20 * t - 11.125f) * -c) / 2 :
            Pow(2, -20 * t + 10) * Sin((20 * t - 11.125f) * c) / 2 + 1;
    }

    /// <summary>https://www.desmos.com/calculator/ssmup8v7er</summary>
    public static float QuadIn(float t) => t * t;

    /// <summary>https://www.desmos.com/calculator/rhmi8azyzt</summary>
    public static float QuadOut(float t) => -t * (t - 2);

    /// <summary>https://www.desmos.com/calculator/mykb4zblnn</summary>
    public static float QuadInOut(float t) =>
        t < 0.5f ?
            2 * t * t :
            1 - Pow(-2 * t + 2, 2) / 2;

    /// <summary>https://www.desmos.com/calculator/cyzzjegddv</summary>
    public static float CubeIn(float t) => Pow(t, 3);

    /// <summary> https://www.desmos.com/calculator/d37tvvraqk</summary>
    public static float CubeOut(float t) => 1 - Pow(t, 3);

    /// <summary>https://www.desmos.com/calculator/i1axctutgh</summary>
    public static float CubeInOut(float t) =>
        t < 0.5f ?
            4 * t * t * t :
            1 - Pow(-2 * t + 2, 3) / 2;

    /// <summary>https://www.desmos.com/calculator/suhb4dmy3v</summary>
    public static float QuartIn(float t) => Pow(t, 4);

    /// <summary>https://www.desmos.com/calculator/lbb11lxyun</summary>
    public static float QuartOut(float t) => 1 - Pow(t - 1, 4);

    /// <summary>https://www.desmos.com/calculator/jnnrevsbtd</summary>
    public static float QuartInOut(float t) =>
        t < 0.5f ?
            8 * t * t * t * t :
            1 - Pow(-2 * t + 2, 4) / 2;

    /// <summary>https://www.desmos.com/calculator/s2fxyphkim</summary>
    public static float QuintIn(float t) => Pow(t, 5);

    ///<summary> https://www.desmos.com/calculator/gqvmunvdvj</summary>
    public static float QuintOut(float t) => Pow(t - 1, 5) + 1;

    /// <summary>https://www.desmos.com/calculator/w3e9ykayf0</summary>
    public static float QuintInOut(float t) =>
        t < 0.5f ?
            16 * t * t * t * t * t :
            1 - Pow(-2 * t + 2, 5) / 2;

    ///<summary> https://www.desmos.com/calculator/g4deeruuws</summary>
    public static float SineIn(float t) => -Cos(Pi2 * t) + 1;

    /// <summary>https://www.desmos.com/calculator/xdiejgub3f</summary>
    public static float SineOut(float t) => Sin(Pi2 * t);

    /// <summary>https://www.desmos.com/calculator/mgi6cczpgn</summary>
    public static float SineInOut(float t) => (-Cos(PI * t) + 1) / 2;

    /// <summary>https://www.desmos.com/calculator/fk3e4gmqgt</summary>
    public static float CircIn(float t) => -Sqrt(1 - t * t) + 1;

    /// <summary>https://www.desmos.com/calculator/2kzr3kcfgs</summary>
    public static float CircOut(float t) => Sqrt(1 - Pow(t - 1, 2));

    /// <summary>https://www.desmos.com/calculator/girbtasxuj</summary>
    public static float CircInOut(float t) => t < 0.5f ?
        (1 - Sqrt(1 - Pow(2 * t, 2))) / 2 :
        (Sqrt(1 - Pow(-2 * t + 2, 2)) + 1) / 2;

    /// <summary>https://www.desmos.com/calculator/5vdv7x1gsb</summary>
    public static float ExpoIn(float t) => Pow(2, 10 * (t - 1));

    /// <summary>https://www.desmos.com/calculator/mywvdch4ho</summary>
    public static float ExpoOut(float t) => -Pow(2, -10 * t) + 1;

    /// <summary>https://www.desmos.com/calculator/plvorvyfio</summary>
    public static float ExpoInOut(float t)
    {
        if (t is 0 or 1) return t;
        return t < 0.5f ?
            Pow(2, 20 * t - 10) / 2 :
            (2 - Pow(2, -20 * t + 10)) / 2;
    }

    private const float Cb = 1.70518f;
    /// <summary>https://www.desmos.com/calculator/hu6twr6txx</summary>
    public static float BackIn(float t) =>
        (1 + Cb) * t * t * t - Cb * t * t;

    /// <summary>https://www.desmos.com/calculator/a71xhwahyh</summary>
    public static float BackOut(float t) =>
        1 + (1 + Cb) * Pow(t - 1, 3) + Cb * Pow(t - 1, 2);

    private const float Cb2 = Cb * 1.525f;
    /// <summary>https://www.desmos.com/calculator/dhuwnbfhlh</summary>
    public static float BackInOut(float t) => t < 0.5f ?
        Pow(2 * t, 2) * ((Cb2 + 1) * 2 * t - Cb2) / 2 :
        (Pow(2 * t - 2, 2) * ((Cb2 + 1) * (t * 2 - 2) + Cb2) + 2) / 2;
}
