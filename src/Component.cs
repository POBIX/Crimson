using System.Collections.Generic;

namespace Crimson
{
    public class Component
    {
        /// <summary> The entity the component is attached to. </summary>
        public Entity Parent { get; internal set; }

        /// <inheritdoc cref="Entity.Material"/>
        public Material Material => Parent.Material;

        /// <inheritdoc cref="Entity.Scene"/>
        public Scene Scene => Parent.Scene;

        /// <inheritdoc cref="Entity.Position"/>
        public virtual Vector2 Position
        {
            get => Parent.Position;
            set => Parent.Position = value;
        }

        public bool IsValid => Parent != null && Parent.IsValid;

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

        /// <inheritdoc cref="Entity.GetComponent"/>
        public T GetComponent<T>() where T : class => Parent.GetComponent<T>();
        /// <inheritdoc cref="Entity.GetComponents"/>
        public IEnumerable<T> GetComponents<T>() where T : class => Parent.GetComponents<T>();
        /// <inheritdoc cref="Entity.AddComponent"/>
        public T AddComponent<T>() where T : Component, new() => Parent.AddComponent<T>();
        /// <inheritdoc cref="Entity.RemoveComponent"/>
        public void RemoveComponent<T>() where T : class => Parent.RemoveComponent<T>();
        /// <inheritdoc cref="Entity.RemoveComponents"/>
        public void RemoveComponents<T>() where T : class => Parent.RemoveComponents<T>();
        /// <inheritdoc cref="Entity.RemoveComponent"/>
        public void RemoveComponent(Component c) => Parent.RemoveComponent(c);
        /// <inheritdoc cref="Entity.HasComponent"/>
        public bool HasComponent<T>(out T component) where T : class => Parent.HasComponent(out component);
    }
}
