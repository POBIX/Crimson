using OpenGL;

namespace Crimson;

public class Sprite : Component
{
    public Texture Texture
    {
        get => texture;
        set
        {
            texture = value;
            source = new Rect(new(), texture.Size);
            Clip = new Rect(new(), texture.Size);
        }
    }

    private Vector2 flip = new(1, 1);

    public Vector2 Scale { get; set; } = Vector2.One;
    public float Rotation { get; set; } = 0;
    public bool FlipH
    {
        get => flipH;
        set
        {
            flipH = value;
            flip.x = FlipH ? -1 : 1;
        }
    }

    public bool FlipV
    {
        get => flipV;
        set
        {
            flipV = value;
            flip.y = FlipV ? -1 : 1;
        }
    }

    public Vector2 Size => Texture.Size;

    public Vector2 Offset { get; set; }

    private Rect source;
    public Rect Source => source;

    private int framesH = 1;
    private int framesV = 1;
    private int frameH;
    private int frameV;
    private Texture texture;
    private Rect clip;
    private bool flipV = false;
    private bool flipH = false;

    public int FramesH
    {
        get => framesH;
        set
        {
            framesH = value;
            source.w = Clip.Size.x / Size.x / Frames.x;
        }
    }

    public int FramesV
    {
        get => framesV;
        set
        {
            framesV = value;
            source.h = Clip.Size.y / Size.y / Frames.y;
        }
    }

    public int FrameH
    {
        get => frameH;
        set
        {
            frameH = value;
            source.x = Clip.Position.x / Size.x + Clip.Size.x / Size.x * (Frame.x / Frames.x);
        }
    }

    public int FrameV
    {
        get => frameV;
        set
        {
            frameV = value;
            source.y = Clip.Position.y / Size.y + Clip.Size.y / Size.y * (Frame.y / Frames.y);
        }
    }

    public Vector2 Frames
    {
        get => new(FramesH, FramesV);
        set
        {
            FramesH = (int)value.x;
            FramesV = (int)value.y;
        }
    }

    public new Vector2 Frame
    {
        get => new(FrameH, FrameV);
        set
        {
            FrameH = (int)value.x;
            FrameV = (int)value.y;
        }
    }

    public Rect Clip
    {
        get => clip;
        set
        {
            clip = value;
            source.Position = Clip.Position / Size + Clip.Size / Size * (Frame / Frames);
            source.Size = Clip.Size / (Size * Scale) / Frames;
        }
    }

    private static float[] vertices =
    {
        -0.5f, 0.5f, 0, 1,
        -0.5f, -0.5f, 0, 0,
        0.5f, -0.5f, 1, 0,
        0.5f, -0.5f, 1, 0,
        0.5f, 0.5f, 1, 1,
        -0.5f, 0.5f, 0, 1
    };

    public override void Start()
    {
        base.Start();

        Material.InitShaderText(Resources.Read("shaders/sprite.vert"), Resources.Read("shaders/sprite.frag"));

        Material.FeedBuffer(vertices);
        Material.BindVAO();

        Material.VertexAttribPointer("VERTEX", IntPtr.Zero, 4, out uint vLoc);
        Material.EnableVertexAttribArray(vLoc);

        Material.EnableVertexAttribArray("TEX_COORDS", out uint tcLoc);
        Material.VertexAttribPointer(tcLoc, new(2 * sizeof(float)), 4);
    }

    public override void Draw()
    {
        base.Draw();
        if (Texture == null) return;

        Vector2 scale = Clip.Size / Frames * Scale * flip;
        Material.SetUniform("TRANSFORM", Camera.GetTransform(Position + Offset, Rotation, scale), false);

        Material.BindVAO();

        Material.SetUniform("TEXTURE", Texture, 0);

        Material.SetUniform("SOURCE_SIZE", Source.Size);
        Material.SetUniform("SOURCE_POS", Source.Position);

        Graphics.DrawArrays(PrimitiveType.Triangles, 0, 6);
    }

    public void NextFrame()
    {
        if (++FrameH >= FramesH)
        {
            FrameH = 0;
            if (++FrameV >= FramesV)
                FrameV = 0;
        }
    }
}
