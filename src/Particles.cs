using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using OpenGL;

namespace Crimson;

public struct Particle
{
    public Color Color { get; set; }
    public Vector2 Size { get; set; }
    public float Lifetime { get; set; }
    public Vector2 Gravity { get; set; }
}

public class ParticleEmitter : DrawableObject
{
    public Vector2 Size { get; set; }

    private static Material defMat;
    private Material material = null;
    public override Material Material
    {
        get => material ?? defMat;
        set => material = value;
    }

    [SuppressMessage("ReSharper", "NotAccessedField.Local")] // they are used only in the compute shader.
    [StructLayout(LayoutKind.Sequential, Size = 64)]
    private struct ShaderParticle
    {
        public Color color;
        public Vector2 gravity;
        public Vector2 velocity;
        public Vector2 position;
        public Vector2 size;
        public Vector2 initialPos;
        public float lifetime;
        public float elapsed;
    }

    private int particlesNum;

    private ComputeShader shader = new();
    private ShaderBuffer<ShaderParticle> compSSBO;
    private ShaderParticle[] compParticles;

    // public Texture Texture { get; private set; }
    internal int ParticlesNum
    {
        get => particlesNum;
        set
        {
            particlesNum = value;
            compParticles = new ShaderParticle[ParticlesNum];
        }
    }

    private static readonly float[] Vertices =
    {
        -0.5f, 0.5f,
        -0.5f, -0.5f,
        0.5f, -0.5f,
        0.5f, -0.5f,
        0.5f, 0.5f,
        -0.5f, 0.5f,
    };

    static ParticleEmitter()
    {
        defMat = new();
        defMat.AttachShaderText(ShaderType.Fragment, Resources.Read("shaders/texture.frag"));
        defMat.AttachShaderText(ShaderType.Vertex, Resources.Read("shaders/texture.vert"));
        defMat.Link();

        defMat.FeedBuffer(Vertices);
        defMat.BindVAO();
        defMat.EnableVertexAttribArray("VERTEX");
        defMat.VertexAttribPointer("VERTEX", IntPtr.Zero, 2);
    }

    private Texture Texture { get; set; }
    public override void Start()
    {
        shader.AttachText(Resources.Read("shaders/particle.comp"));
        compSSBO = new(shader, "particlesSSBO");
        // compSSBO = new(3);

        Texture = new(Size);
        shader.SetUniformImage("TEXTURE", Texture, BufferAccess.ReadWrite, 0);

        compSSBO.SetData(compParticles);

        // fragSSBO = new(Material, "particlesSSBO");
        // fragSSBO.SetData(compParticles);
    }

    public override void Update(float delta)
    {
        shader.SetUniform("DELTA", delta);
        shader.SetUniform("PROGRAM", 0);
        compSSBO.SetData(compParticles);

        shader.Dispatch(ParticlesNum / 8, 1, 1);

        compSSBO.GetData(ref compParticles);
        compSSBO.SetData(compParticles);

        Texture.Clear();
        shader.SetUniform("PROGRAM", 1);
        shader.Dispatch(ParticlesNum / 8,1 , 1);

        Material.UpdateUniforms();
    }

    public override void Frame(float delta) { }
    public override void SetScene(Scene value) { }
    public override void Draw()
    {
        Material.SetUniform("TRANSFORM", Camera.GetTransform(Position, 0, Size), false);
        // Material.SetUniform("PARTICLES", ParticlesNum);
        // Material.SetUniform("POSITION", Position - Camera.CurrentOrigin);
        Texture.Bind(0);
        Material.SetUniform("TEXTURE", 0);

        Graphics.DrawArrays(PrimitiveType.Triangles, 0, 6);
    }

    private int addIndex = 0;

    private static Random rnd = new();
    public void Add(Particle p) =>
        compParticles[addIndex++] = new()
        {
            color = p.Color,
            lifetime = p.Lifetime,
            elapsed = p.Lifetime, // so that it gets reset
            size = p.Size,
            gravity = p.Gravity,
            initialPos = new(rnd.Next(0, (int)Size.x), rnd.Next(0, (int)Size.y)),
            position = Vector2.Zero,
            velocity = Vector2.Zero
        };
}
