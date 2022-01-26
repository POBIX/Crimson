namespace Crimson;

public abstract class SceneObject
{
    public abstract void Start();
    public abstract void Update(float delta);
    public abstract void Frame(float delta);

    public abstract void SetScene(Scene value);

    public abstract void OnDestroy();

    public SceneObject Parent { get; set; }
    public virtual Vector2 LocalPosition { get; set; }
    public virtual Vector2 Position
    {
        get => LocalPosition + (Parent?.Position ?? Vector2.Zero);
        set => LocalPosition = value - (Parent?.Position ?? Vector2.Zero);
    }
    public List<string> Groups { get; set; } = new();
    public bool Paused { get; set; } = false;
}

public abstract class DrawableObject : SceneObject
{
    public abstract Material Material { get; set; }
    public abstract void Draw();

    public bool Hidden { get; set; } = false;
}

public sealed partial class Scene
{
    private List<SceneObject> scene = new();
    public IReadOnlyCollection<SceneObject> Objects => scene.AsReadOnly();

    private Queue<(SceneObject, bool)> removalQueue = new();

    public bool Started { get; private set; } = false;

    public void AddObject(SceneObject o)
    {
        o.SetScene(this);
        scene.Add(o);
        if (Started) o.Start();
    }

    /// <summary>
    /// Calls the <seealso cref="SceneObject.Start"/> method on every object added to the scene.
    /// </summary>
    public void Start()
    {
        Started = true;

        // ReSharper disable once ForCanBeConvertedToForeach (collection will be modified)
        for (int i = 0; i < scene.Count; i++)
            scene[i].Start();
    }

    private void RemoveQueue()
    {
        while (removalQueue.Count != 0)
        {
            (SceneObject obj, bool disp) = removalQueue.Dequeue();
            if (disp) DoDestroy(obj);
            else DoDestroyNoDispose(obj);
        }
    }

    /// <summary>
    /// Calls the <seealso cref="SceneObject.Update"/> method on every object added to the scene.
    /// </summary>
    public void Update(float delta)
    {
        RemoveQueue();
        // ReSharper disable once ForCanBeConvertedToForeach (collection will be modified)
        for (int i = 0; i < scene.Count; i++)
        {
            if (!scene[i].Paused)
                scene[i].Update(delta);
        }
    }

    /// <summary>
    /// Calls the <seealso cref="SceneObject.Frame"/> method on every object added to the scene.
    /// </summary>
    public void Frame(float delta)
    {
        RemoveQueue();
        // ReSharper disable once ForCanBeConvertedToForeach (collection will be modified)
        for (int i = 0; i < scene.Count; i++)
        {
            if (!scene[i].Paused)
                scene[i].Frame(delta);
        }
    }

    /// <summary>
    /// Calls the <seealso cref="SceneObject.Draw"/> method on every object added to the scene.
    /// </summary>
    public void Draw()
    {
        // ReSharper disable once ForCanBeConvertedToForeach (collection will be modified)
        for (int i = 0; i < scene.Count; i++)
        {
            if (scene[i] is DrawableObject d && !d.Hidden)
            {
                d.Material.Use();
                d.Draw();
            }
        }
    }

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
        AddObject(t);
        if (autoStart) t.Begin();
        return t;
    }

    /// <summary>
    /// Returns of every component of type <typeparam name="T"/> in the scene, on every entity.
    /// </summary>
    public IEnumerable<T> GetComponentsOfType<T>() where T : class
    {
        foreach (SceneObject o in scene)
        {
            if (o is Entity e)
            {
                foreach (T t in e.GetComponents<T>())
                    yield return t;
            }
        }
    }

    /// <summary>
    /// Destroys an object - stops updating and drawing it.
    /// Automatically disposes IDisposables.
    /// </summary>
    /// <param name="o">The entity to destroy</param>
    public void Destroy(SceneObject o) => removalQueue.Enqueue((o, true));
    private void DoDestroy(SceneObject o)
    {
        DoDestroyNoDispose(o);
        o.OnDestroy();
        if (o is IDisposable d) d.Dispose();
    }

    /// <summary>
    /// Destroys an object - stops updating and drawing it.
    /// Does not dispose IDisposables.
    /// </summary>
    /// <param name="o">The entity to destroy</param>
    public void DestroyNoDispose(SceneObject o) => removalQueue.Enqueue((o, false));
    private void DoDestroyNoDispose(SceneObject o)
    {
        o.SetScene(null);
        scene.Remove(o);
    }

    public void Reset()
    {
        foreach (SceneObject obj in scene)
            Destroy(obj);
        scene.Clear();
    }

    /// <summary>
    /// Finds an entity by name. Returns null if not found.
    /// </summary>
    public Entity FindEntity(string name)
    {
        foreach (SceneObject o in scene)
        {
            if (o is Entity e)
            {
                if (e.Name == name)
                    return e;
            }
        }
        return null;
    }

    public IEnumerable<T> GetGroup<T>(string name) where T : class
    {
        foreach (SceneObject obj in scene)
        {
            if (obj is T t && obj.Groups.Contains(name))
                yield return t;
        }
    }

    public IEnumerable<SceneObject> GetGroup(string name) => GetGroup<SceneObject>(name);

    public SceneGenerator Load(SceneGenerator scene)
    {
        scene.Scene = this;
        scene.Start();
        if (!Started) Start();
        return scene;
    }

    public T Load<T>() where T : SceneGenerator, new() => Load(new T()) as T;
}
