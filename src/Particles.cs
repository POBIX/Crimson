using System;
using System.Collections.Generic;
using Crimson.Scenes;
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

        internal Vector2 position;

        private Vector2 vel;
        private Vector2 velocity;

        public Particle(Vector2 velocity, Color color, float lifetime, Vector2 size, Vector2 gravity)
        {
            vel = Velocity = velocity;
            Color = color;
            Lifetime = lifetime;
            Size = size;
            Gravity = gravity;
        }

        public Particle(Vector2 velocity, Color color, float lifetime, Vector2 size) :
            this(velocity, color, lifetime, size, new(0, 9.8f)) { }

        internal void Update(float delta)
        {
            vel += Gravity;
            position += vel * delta;
            if ((TimeElapsed += delta) > Lifetime)
            {
                position = new();
                vel = new();
                TimeElapsed = 0;
            }
        }
    }

    public class ParticleEmitter : IDrawableObject
    {
        private const int MaxParticles = 256; // due to size limitations on uniform array

        private List<Particle> particles = new(MaxParticles);
        private static Material material;
        Material IDrawableObject.Material => material;

        public Vector2 Position { get; set; }
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
            material = new();
            material.AttachShaderText(ShaderType.Fragment, Resources.Read("shaders/particle.frag"));
            material.AttachShaderText(ShaderType.Vertex, Resources.Read("shaders/basic.vert"));
            material.Link();

            material.FeedBuffer(Vertices);
            material.BindVAO();
            material.EnableVertexAttribArray("VERTEX");
            material.VertexAttribPointer("VERTEX", IntPtr.Zero, 2);

            for (int i = 0; i < MaxParticles; i++)
            {
                posCache[i] = $"particles[{i}].position";
                colorCache[i] = $"particles[{i}].color";
                sizeCache[i] = $"particles[{i}].size";
            }
        }

        void ISceneObject.Update(float delta)
        {
            if (Stopped || Paused) return;
            foreach (var p in particles)
                p.Update(delta);
        }

        public void Start() { }
        public void Frame(float delta) { }
        public void SetScene(Scene value) { }

        void IDrawableObject.Draw()
        {
            if (Stopped) return;

            material.SetUniform("TRANSFORM", Camera.GetTransform(Position, 0, Size), false);

            material.SetUniform("PARTICLES", particles.Count);
            Vector2 basePos = Position - Camera.CurrentOrigin;
            for (int i = 0; i < particles.Count; i++)
            {
                material.SetUniform(posCache[i], basePos + particles[i].position);
                material.SetUniform(colorCache[i], particles[i].Color);
                material.SetUniform(sizeCache[i], particles[i].Size);
            }

            Graphics.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }

        public void Add(Particle p)
        {
            if (particles.Count >= MaxParticles)
                throw new InsufficientMemoryException("A particle emitter can only have up to 256 particles.");
            particles.Add(p);
        }
    }
}
