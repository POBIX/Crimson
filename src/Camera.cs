using OpenGL;

namespace Crimson;

public enum Offset
{
    TopLeft,
    Center
}

public class Camera : Component
{
    private Vector2 target;
    private Offset offset;
    private Vector2 actualOffset;
    private Vector2 virtualResolution;

    public float Interpolation { get; set; }
    public float Zoom { get; set; } = 1;

    public bool SyncToPhysics { get; set; } = true;

    public Offset Offset
    {
        get => offset;
        set
        {
            offset = value;
            CalcOffset();
        }
    }

    public Vector2 VirtualResolution
    {
        get => virtualResolution;
        set
        {
            virtualResolution = value;
            CalcSize();
            CalcOffset();
        }
    }

    public Vector2 Origin { get; private set; }

    public Matrix Transform { get; private set; } = Matrix.Identity;

    public static Camera Current { get; private set; }

    public static Matrix CurrentTransform => Current?.Transform ?? Matrix.Identity;
    public static Matrix Ortho { get; private set; } = Matrix.Ortho(0, Engine.Width, Engine.Height, 0, -1, 1);

    public static Vector2 CurrentOrigin => Current?.Origin ?? new();
    public static Vector2 CurrentResolution => Current?.VirtualResolution ?? Engine.Size;

    internal Vector2 scale = Vector2.One;

    static Camera() => Engine.Resize += (w, h) => Ortho = Matrix.Ortho(0, w, h, 0, -1, 1);

    public override void Start() => Engine.Resize += (_, _) => CalcSize();

    private void Lerp()
    {
        Origin = Mathf.Lerp(Origin, target - actualOffset, Interpolation).Rounded();
        Transform = Matrix.Scaling(new(scale, 0)) * Matrix.Translation(new(-Origin, 0));
    }

    public override void Frame(float delta)
    {
        base.Frame(delta);
        if (!SyncToPhysics) Lerp();
    }

    public override void Update(float delta)
    {
        base.Update(delta);
        if (SyncToPhysics) Lerp();
    }

    public void MoveTo(Vector2 position) => target = position;

    public void Activate() => Current = this;

    public void SetOffset(Vector2 offset) => actualOffset = offset;

    /// <summary>
    /// Returns a transformation matrix relative to the camera.
    /// </summary>
    /// <param name="position">The translation</param>
    /// <param name="rotation">The rotation (in radians)</param>
    /// <param name="size">The scale</param>
    public static Matrix GetTransform(Vector2 position, float rotation, Vector2 size) =>
        Ortho * CurrentTransform * Matrix.Transformation(position, rotation, size);

    private void CalcSize()
    {
        float aspect = VirtualResolution.x / VirtualResolution.y;

        int width = Engine.Width;
        int height = (int)(width / aspect + 0.5f);

        if (height > VirtualResolution.x)
        {
            height = Engine.Height;
            width = (int)(height * aspect + 0.5f);
        }

        int x = Engine.Width / 2 - width / 2;
        int y = Engine.Height / 2 - height / 2;

        Gl.Viewport(x, y, width, height);

        scale = Engine.Size / VirtualResolution;
    }

    private void CalcOffset() => actualOffset = offset switch
    {
        Offset.Center => VirtualResolution / 2,
        Offset.TopLeft => new(),
        _ => new()
    };
}