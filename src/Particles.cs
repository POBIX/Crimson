using System;
using System.Collections.Generic;
using OpenGL;

namespace Crimson
{
    public class Particle
    {
        public Vector2 Velocity
        {
            get => velocity;
            set
            {
                // cancel out our previous velocity without affecting anything else, then add the new one.
                vel -= Velocity;
                velocity = value;
                vel += Velocity;
            }
        }

        public Color Color { get; set; }
        public float Lifetime { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Gravity { get; set; }
        public float TimeElapsed { get; private set; }
        public ParticleEmitter Parent { get; internal set; }

        public Vector2 Position { get; set; }

        private Vector2 vel;
        private Vector2 velocity;

        private static Random rnd = new();
        private Vector2 RandomPos() => new(rnd.Next(0, (int)Parent.Size.x), rnd.Next(0, (int)Parent.Size.y));

        public Particle(Vector2 velocity, Color color, float lifetime, Vector2 size, Vector2 gravity)
        {
            vel = Velocity = velocity;
            Color = color;
            Lifetime = lifetime;
            Size = size;
            Gravity = gravity;
        }

        internal void Start() => Position = RandomPos();

        internal void Update(float delta)
        {
            vel += Gravity;
            Position += vel * delta;
            if ((TimeElapsed += delta) > Lifetime)
            {
                Position = RandomPos();
                vel = Vector2.Zero;
                TimeElapsed = 0;
            }
        }
    }

    public class ParticleEmitter : DrawableObject
    {
        private const int MaxParticles = 256; // due to size limitations on uniform array

        private List<Particle> particles = new(MaxParticles);
        private static Material defMat;
        private Material material = null;
        public override Material Material
        {
            get => material ?? defMat;
            set => material = value;
        }

        public Vector2 Size { get; set; }

        public bool Paused { get; set; }
        public bool Stopped { get; set; }

        // we cache strings such as "particles[2].position" instead of evaluating them every single frame
        // in order to save on memory allocation (~700 FPS improvement).
        private static string[] posCache = new string[MaxParticles];
        private static string[] colorCache = new string[MaxParticles];
        private static string[] sizeCache = new string[MaxParticles];

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
            defMat.AttachShaderText(ShaderType.Fragment, Resources.Read("shaders/particle.frag"));
            defMat.AttachShaderText(ShaderType.Vertex, Resources.Read("shaders/basic.vert"));
            defMat.Link();

            defMat.FeedBuffer(Vertices);
            defMat.BindVAO();
            defMat.EnableVertexAttribArray("VERTEX");
            defMat.VertexAttribPointer("VERTEX", IntPtr.Zero, 2);

            for (int i = 0; i < MaxParticles; i++)
            {
                posCache[i] = $"particles[{i}].position";
                colorCache[i] = $"particles[{i}].color";
                sizeCache[i] = $"particles[{i}].size";
            }
        }

        public override void Update(float delta)
        {
            if (Stopped || Paused) return;
            foreach (var p in particles)
                p.Update(delta);
            Material.UpdateUniforms();
        }

        public override void Start() { }
        public override void Frame(float delta) { }
        public override void SetScene(Scene value) { }

        public override void Draw()
        {
            if (Stopped) return;

            Material.SetUniform("TRANSFORM", Camera.GetTransform(Position, 0, Size), false);

            Material.SetUniform("PARTICLES", particles.Count);
            Vector2 basePos = Position - Camera.CurrentOrigin;
            for (int i = 0; i < particles.Count; i++)
            {
                Material.SetUniform(posCache[i], basePos + particles[i].Position);
                Material.SetUniform(colorCache[i], particles[i].Color);
                Material.SetUniform(sizeCache[i], particles[i].Size);
            }

            Graphics.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }

        public void Add(Particle p)
        {
            if (particles.Count >= MaxParticles)
                throw new InsufficientMemoryException("A particle emitter can only have up to 256 particles.");
            particles.Add(p);
            p.Parent = this;
            p.Start();
        }
    }
}
