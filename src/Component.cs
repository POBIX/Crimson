using System.Collections.Generic;

namespace Crimson
{
    public class Component
    {
        /// <summary> The entity the component is attached to. </summary>
        public Entity Entity { get; internal set; }

        /// <inheritdoc cref="Crimson.Entity.Material"/>
        public Material Material => Entity.Material;

        /// <inheritdoc cref="Crimson.Entity.Scene"/>
        public Scene Scene => Entity.Scene;

        /// <inheritdoc cref="Crimson.Entity.Position"/>
        public virtual Vector2 Position
        {
            get => Entity.Position;
            set => Entity.Position = value;
        }

        public bool IsValid => Entity != null && Entity.IsValid;

        /// <summary>
        /// Gets called as soon as the entity is added to the scene, and before <see cref="Start"/>
        /// </summary>
        public virtual void Awake() { }
        /// <summary>
        /// Gets called after the scene has been initialized.
        /// </summary>
        public virtual void Start() { }
        /// <summary>
        /// Gets called every physics frame.
        /// </summary>
        /// <param name="delta">The constant physics step.</param>
        public virtual void Update(float delta) { }
        /// <summary>
        /// Gets called every frame.
        /// </summary>
        /// <param name="delta">The non constant time in seconds since the last frame.</param>
        public virtual void Frame(float delta) { }
        /// <summary>
        /// Gets called while the Material is in use.
        /// </summary>
        public virtual void Draw() { }

        /// <inheritdoc cref="Crimson.Entity.GetComponent"/>
        public T GetComponent<T>() where T : class => Entity.GetComponent<T>();
        /// <inheritdoc cref="Crimson.Entity.GetComponents"/>
        public IEnumerable<T> GetComponents<T>() where T : class => Entity.GetComponents<T>();
        /// <inheritdoc cref="Crimson.Entity.AddComponent"/>
        public T AddComponent<T>() where T : Component, new() => Entity.AddComponent<T>();
        /// <inheritdoc cref="Crimson.Entity.RemoveComponent"/>
        public void RemoveComponent<T>() where T : class => Entity.RemoveComponent<T>();
        /// <inheritdoc cref="Crimson.Entity.RemoveComponents"/>
        public void RemoveComponents<T>() where T : class => Entity.RemoveComponents<T>();
        /// <inheritdoc cref="Crimson.Entity.RemoveComponent"/>
        public void RemoveComponent(Component c) => Entity.RemoveComponent(c);
        /// <inheritdoc cref="Crimson.Entity.HasComponent"/>
        public bool HasComponent<T>(out T component) where T : class => Entity.HasComponent(out component);
    }
}
