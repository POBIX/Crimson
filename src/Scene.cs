using System;
using System.Collections.Generic;
using System.IO;
using Crimson.Scenes;

namespace Crimson
{
    public interface ISceneObject
    {
        void Start();
        void Update(float delta);
        void Frame(float delta);

        void SetScene(Scene value);
    }

    public interface IDrawableObject : ISceneObject
    {
        Material Material { get; }
        void Draw();
    }

    public sealed partial class Scene
    {
        private List<ISceneObject> scene = new();

        public bool Started { get; private set; } = false;

        public void AddObject(ISceneObject o)
        {
            o.SetScene(this);
            scene.Add(o);
            if (Started) o.Start();
        }

        /// <summary>
        /// Calls the <seealso cref="ISceneObject.Start"/> method on every object added to the scene.
        /// </summary>
        public void Start()
        {
            // ReSharper disable once ForCanBeConvertedToForeach (collection might be modified)
            for (int i = 0; i < scene.Count; i++)
                scene[i].Start();

            Started = true;
        }

        /// <summary>
        /// Calls the <seealso cref="ISceneObject.Update"/> method on every object added to the scene.
        /// </summary>
        public void Update(float delta)
        {
            // ReSharper disable once ForCanBeConvertedToForeach (collection might be modified)
            for (int i = 0; i < scene.Count; i++)
                scene[i].Update(delta);
        }

        /// <summary>
        /// Calls the <seealso cref="ISceneObject.Frame"/> method on every object added to the scene.
        /// </summary>
        public void Frame(float delta)
        {
            // ReSharper disable once ForCanBeConvertedToForeach (collection might be modified)
            for (int i = 0; i < scene.Count; i++)
                scene[i].Frame(delta);
        }

        /// <summary>
        /// Calls the <seealso cref="ISceneObject.Draw"/> method on every object added to the scene.
        /// </summary>
        public void Draw()
        {
            // ReSharper disable once ForCanBeConvertedToForeach (collection might be modified)
            for (int i = 0; i < scene.Count; i++)
            {
                if (scene[i] is IDrawableObject d)
                {
                    d.Material.Use();
                    d.Draw();
                }
            }
        }

        /// <summary>
        /// Adds a timer to the scene - it won't work without being attached to it.
        /// </summary>
        /// <param name="t">The timer to add</param>
        public void AddTimer(Timer t) => AddObject(t);

        /// <summary>
        /// Creates a timer and adds it to the scene.
        /// </summary>
        /// <param name="duration">How long before firing <param name="timeout"/></param>
        /// <param name="timeout">The action to perform on Timeout.</param>
        /// <param name="autoStart">Should the timer immediately start?</param>
        /// <param name="loop">Should the timer automatically reset after Timeout?</param>
        /// <param name="syncToPhysics">Should the timer update on each physics frame or frame?</param>
        /// <returns>The created timer</returns>
        public Timer CreateTimer(float duration, Action timeout, bool autoStart, bool loop, bool syncToPhysics = false)
        {
            var t = new Timer(duration, loop, syncToPhysics);
            t.Timeout += timeout;
            AddTimer(t);
            if (autoStart) t.Begin();
            return t;
        }

        /// <summary>
        /// Returns of every component of type <typeparam name="T"/> in the scene, on every entity.
        /// </summary>
        public IEnumerable<T> GetComponentsOfType<T>() where T : class
        {
            foreach (ISceneObject o in scene)
            {
                if (o is Entity e)
                {
                    foreach (T t in e.GetComponents<T>())
                        yield return t;
                }
            }
        }

        /// <summary>
        /// Loads a scene file.
        /// </summary>
        /// <param name="scene">The path of the scene file to load. </param>
        public void Load(string scene)
        {
            foreach (ISceneObject o in SceneReader.LoadScene(File.ReadAllText(scene)))
                AddObject(o);
            Start();
        }

        /// <summary>
        /// Destroys an object - stops updating and drawing it.
        /// Automatically disposes IDisposables.
        /// </summary>
        /// <param name="o">The entity to destroy</param>
        public void Destroy(ISceneObject o)
        {
            DestroyNoDispose(o);
            if (o is IDisposable d) d.Dispose();
        }

        /// <summary>
        /// Destroys an object - stops updating and drawing it.
        /// Does not dispose IDisposables.
        /// </summary>
        /// <param name="o">The entity to destroy</param>
        public void DestroyNoDispose(ISceneObject o)
        {
            o.SetScene(null);
            scene.Remove(o);
        }

        public void Reset() => scene.Clear();

        /// <summary>
        /// Finds an entity by name. Returns null if not found.
        /// </summary>
        public Entity FindEntity(string name)
        {
            foreach (ISceneObject o in scene)
            {
                if (o is Entity e)
                {
                    if (e.Name == name)
                        return e;
                }
            }
            return null;
        }

        public void Load<T>() where T : SceneGenerator, new()
        {
            T t = new() { Scene = this };
            t.Start();
        }
    }
}
