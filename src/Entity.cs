namespace Crimson;

public sealed class Entity : DrawableObject, IDisposable
{
    private List<Component> components = new();

    /// <summary> The entity's current scene. </summary>
    public Scene Scene { get; private set; }

    /// <summary> The entity's material. </summary>
    public override Material Material { get; set; }

    /// <summary> The entity's name. </summary>
    public string Name { get; set; }

    /// <summary> Has Start() been called for this entity? </summary>
    public bool Started { get; private set; }

    /// <summary> Has Awake() been called for this entity? </summary>
    public bool Awoke { get; private set; }

    /// <summary> Is this entity attached to any scene?
    /// (will be false before adding and after destroying an entity)
    /// </summary>
    public bool IsValid => Scene != null;

    /// <summary>
    /// The entity's unique identifier
    /// </summary>
    public uint ID { get; }
    private static uint idCounter = 0;

    public event Action<Component> ComponentAdded;
    public event Action<Component> ComponentRemoved;
    public event Action Destroyed;

    private Queue<Component> removalQueue = new();

    public Entity() => ID = idCounter++;

    /// <summary>
    /// Gets the first component of type <typeparamref name="T"/> in the entity
    /// </summary>
    public T GetComponent<T>() where T : class
    {
        foreach (Component c in components)
        {
            if (c is T t)
                return t;
        }

        return null;
    }

    /// <summary>
    /// Returns all components of type <typeparamref name="T"/> in the entity
    /// </summary>
    public IEnumerable<T> GetComponents<T>() where T : class
    {
        foreach (Component c in components)
        {
            if (c is T t)
                yield return t;
        }
    }

    /// <summary>
    /// Adds a component to the entity.
    /// </summary>
    /// <param name="c">The component to add</param>
    public void AddComponent(Component c)
    {
        c.Entity = this;
        if (Awoke) c.Awake();
        if (Started) c.Start();
        components.Add(c);
        ComponentAdded?.Invoke(c);
    }

    /// <summary>
    /// Adds a component to the entity.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>The instance of the component that was added.</returns>
    public T AddComponent<T>() where T : Component, new()
    {
        var t = new T();
        AddComponent(t);
        return t;
    }

    /// <summary>
    /// Removes the first component of type <typeparamref name="T"/>
    /// </summary>
    public void RemoveComponent<T>() where T : class
    {
        foreach (Component comp in components)
        {
            if (comp is T)
            {
                RemoveComponent(comp);
                break;
            }
        }
    }

    /// <summary>
    /// Remove a specific component from the entity.
    /// </summary>
    /// <param name="c">The component to remove</param>
    public void RemoveComponent(Component c) => removalQueue.Enqueue(c);
    private void DoRemoveComponent(Component c)
    {
        components.Remove(c);
        ComponentRemoved?.Invoke(c);
        if (c is IDisposable d) d.Dispose();
    }

    /// <summary>
    /// Removes all components of type <typeparamref name="T"/>
    /// </summary>
    public void RemoveComponents<T>() where T : class
    {
        foreach (Component comp in components)
        {
            if (comp is T)
                RemoveComponent(comp);
        }
    }

    /// <summary>
    /// Does the entity have a component of type <typeparamref name="T"/>?
    /// </summary>
    /// <param name="component">The component instance if found, null otherwise.</param>
    public bool HasComponent<T>(out T component) where T : class
    {
        foreach (Component c in components)
        {
            if (c is T t)
            {
                component = t;
                return true;
            }
        }

        component = null;
        return false;
    }

    internal void Awake()
    {
        Awoke = true;
        Material = GetComponent<Material>() ?? AddComponent<Material>();
        // ReSharper disable once ForCanBeConvertedToForeach (collection will be modified)
        for (int i = 0; i < components.Count; i++)
            components[i].Awake();
    }

    public override void Start()
    {
        Started = true;
        // ReSharper disable once ForCanBeConvertedToForeach (collection will be modified)
        for (int i = 0; i < components.Count; i++)
            components[i].Start();
    }

    private void RemoveQueue()
    {
        while (removalQueue.Count != 0)
            DoRemoveComponent(removalQueue.Dequeue());
    }

    public override void Update(float delta)
    {
        RemoveQueue();
        // ReSharper disable once ForCanBeConvertedToForeach (collection will be modified)
        for (int i = 0; i < components.Count; i++)
        {
            if (!components[i].Paused)
                components[i].Update(delta);
        }
    }

    public override void Frame(float delta)
    {
        RemoveQueue();
        // ReSharper disable once ForCanBeConvertedToForeach (collection will be modified)
        for (int i = 0; i < components.Count; i++)
        {
            if (!components[i].Paused)
                components[i].Frame(delta);
        }
    }

    public override void SetScene(Scene value)
    {
        Scene = value;
        if (value != null) Awake();
    }

    public override void Draw()
    {
        // ReSharper disable once ForCanBeConvertedToForeach (collection will be modified)
        for (int i = 0; i < components.Count; i++)
            components[i].Draw();
    }

    /// <summary>
    /// Destroys the entity - stops updating and drawing it.
    /// </summary>
    public void Destroy() => Scene.Destroy(this);

    public override void OnDestroy() => Destroyed?.Invoke();

    public static bool operator ==(Entity a, Entity b)
    {
        if (a is null) return b is null;
        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b) => !(a == b);

    private bool Equals(Entity e) => e != null && ID == e.ID;

    public override bool Equals(object obj) =>
        ReferenceEquals(this, obj) || obj is Entity e && Equals(e);

    public override int GetHashCode() => (int)ID;

    public void Dispose()
    {
        foreach (IDisposable d in GetComponents<IDisposable>())
        {
            d.Dispose();
        }
        GC.SuppressFinalize(this);
    }

    // no finalizer since it sometimes causes a crash due to an OpenGL.Net bug.
    // ~Entity() => Dispose();
    ~Entity()
    {
        Console.WriteLine($"Entity {Name} was finalized. This causes a memory leak." +
                          " Call Destroy() or Dispose() when you're done using it.");
    }
}
