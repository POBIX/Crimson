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
    public float Rotation { get; set; }
}

public struct CoolerParticle
{
    public interface IValue<out T>
    {
        T GetValue();
    }

    public struct Value<T> : IValue<T>
    {
        public T Val { get; set; }
        T IValue<T>.GetValue() => Val;
    }

    public struct Range<T> : IValue<T>
    {
        public T Min { get; set; }
        public T Max { get; set; }

        private static Func<T, T, T> rand;

        public static void Register(Func<T, T, T> rand) =>
            Range<T>.rand = rand;

        T IValue<T>.GetValue() =>
            rand(Min, Max) ?? throw new Exception($"Range not implemented for {typeof(T)}. Use Range<{typeof(T)}.Register");
    }

    public struct TimeRange<T>
    {
        public IValue<T> Start { get; set; }
        public IValue<T> End { get; set; }
    }

    public TimeRange<Color> Color { get; set; }
    public IValue<float> Lifetime { get; set; }
    public TimeRange<Vector2> Gravity { get; set; }
    public TimeRange<float> Rotation { get; set; }
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
    [StructLayout(LayoutKind.Sequential, Size = 80)] // size must be a multiple of 16
    private struct ShaderParticle
    {
        public Color color;
        public Vector2 gravity;
        public Vector2 velocity;
        public Vector2 position;
        public Vector2 initialPos;
        public Vector2 size;
        public float lifetime;
        public float elapsed;
        public float delay;
        public float rotation;
        public byte visible; // bool
    }

    private int particlesNum;

    private ComputeShader shader = new();
    private ShaderBuffer<ShaderParticle> ssbo;
    private ShaderParticle[] compParticles;

    public float SpawnDelay { get; set; }
    public float Preprocess { get; set; }

    public Texture Texture { get; private set; }
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
        defMat.AttachShaderText(ShaderType.Fragment, Resources.Read("shaders/particles.frag"));
        defMat.AttachShaderText(ShaderType.Vertex, Resources.Read("shaders/particles.vert"));
        defMat.Link();

        defMat.FeedBuffer(Vertices);
        defMat.BindVAO();
        defMat.EnableVertexAttribArray("VERTEX");
        defMat.VertexAttribPointer("VERTEX", IntPtr.Zero, 2);
    }

    public override void Start()
    {
        shader.AttachText(Resources.Read("shaders/particle.comp"));
        ssbo = new(4);
        // compSSBO = new(3);

        Texture = new(Size);
        shader.SetUniform("TEXTURE", Texture, BufferAccess.ReadWrite, 0);

        ssbo.SetData(compParticles);

        // fragSSBO = new(Material, "particlesSSBO");
        // fragSSBO.SetData(compParticles);

        if (Preprocess != 0)
        {
            Stopwatch s = Stopwatch.StartNew();
            shader.SetUniform("DELTA", Preprocess);
            shader.SetUniform("PROGRAM", 2);
            shader.Dispatch(ParticlesNum / 8, 1, 1, MemoryBarriers.None);
            ssbo.GetData(ref compParticles);
            s.Stop();
            Console.WriteLine(s.ElapsedMilliseconds);
        }
    }

    private static void DrawParticles()
    {
        // Texture.Clear();
        // shader.SetUniform("PROGRAM", 1);
        // shader.Dispatch(ParticlesNum / 8, 1, 1);


    }

    public override void Update(float delta)
    {
        shader.SetUniform("DELTA", delta);
        shader.SetUniform("PROGRAM", 0);

        shader.Dispatch(ParticlesNum / 8, 1, 1, MemoryBarriers.ShaderImageAccess);

        ssbo.GetData(ref compParticles);
        ssbo.SetData(compParticles);
        DrawParticles();

        Material.UpdateUniforms();
    }

    public override void Frame(float delta) { }
    public override void Draw()
    {
        Material.SetUniform("TRANSFORM", Camera.GetTransform(Position, 0, Vector2.One), false);
        Material.SetUniform("TEXTURE", Texture, 0);
        Material.SetUniform("CAMERA", Camera.Ortho * Camera.CurrentTransform, false);

        Gl.DrawArraysInstanced(PrimitiveType.Triangles, 0, 6, ParticlesNum);
    }

    public override void OnDestroy() { }

    private int addIndex = 0;

    private static Random rnd = new();

    private float delay = 0;
    public void Add(Particle p) =>
        compParticles[addIndex++] = new()
        {
            color = p.Color,
            size = p.Size,
            gravity = p.Gravity,
            initialPos = new(rnd.Next(0, (int)Size.x), rnd.Next(0, (int)Size.y)),
            position = Vector2.Zero,
            velocity = Vector2.Zero,
            delay = delay += SpawnDelay,
            lifetime = p.Lifetime + delay,
            elapsed = p.Lifetime, // so that it gets reset
            rotation = p.Rotation,
            visible = 0
        };
}
