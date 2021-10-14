using System;
using System.Collections.Generic;
using System.Text;
using OpenGL;

namespace Crimson
{
    public class Light : ISceneObject
    {
        /// <summary> The light's position. </summary>
        public Vector2 Position { get; set; }

        /// <summary> The light's radius. </summary>
        public float Radius { get; set; }

        /// <summary> The intensity of the light's emission. </summary>
        public float Strength { get; set; } = 1;

        /// <summary> The color of the light's emission. </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// The amount of ambient light in the scene.
        /// 1 is completely bright (lights won't work) and 0 is completely dark (you won't see anything without lights).
        /// </summary>
        public static float Ambience { get; set; } = 1;

        void ISceneObject.Start() { }
        void ISceneObject.Update(float delta) { }
        void ISceneObject.Frame(float delta) { }
        void ISceneObject.SetScene(Scene value) { }
    }

    public sealed partial class Scene
    {
        private List<Light> lights = new();

        internal void DrawLights(Material mat)
        {
            mat.SetUniform("LIGHTS", lights.Count);
            for (int i = 0; i < lights.Count; i++)
            {
                mat.SetUniform($"lights[{i}].pos", lights[i].Position - Camera.CurrentOrigin);
                mat.SetUniform($"lights[{i}].radius", lights[i].Radius);
                mat.SetUniform($"lights[{i}].strength", lights[i].Strength);
                mat.SetUniform($"lights[{i}].color", lights[i].Color);
            }
        }

        public Light CreateLight(Vector2 position, float radius, float strength, Color color)
        {
            Light l = new() {Position = position, Radius = radius, Strength = strength, Color = color};
            AddLight(l);
            return l;
        }

        public void AddLight(Light l) => lights.Add(l);
    }
}
