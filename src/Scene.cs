namespace Crimson;

public abstract class SceneObject
{
    public abstract void Start();
    public abstract void Update(float delta);
    public abstract void Frame(float delta);

    public abstract void SetScene(Scene value);

    public SceneObject Parent { get; set; }
    public virtual Vector2 LocalPosition { get; set; }
    public virtual Vector2 Position
    {
        get => LocalPosition + (Parent?.Position ?? Vector2.Zero);
        set => LocalPosition = value - (Parent?.Position ?? Vector2.Zero);
    }
}

public abstract class DrawableObject : SceneObject
{
    public abstract Material Material { get; set; }
    public abstract void Draw();
}

public sealed partial class Scene
{
    private List<SceneObject> scene = new();

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
        // ReSharper disable once ForCanBeConvertedToForeach (collection might be modified)
        for (int i = 0; i < scene.Count; i++)
            scene[i].Start();

        Started = true;
    }

    /// <summary>
    /// Calls the <seealso cref="SceneObject.Update"/> method on every object added to the scene.
    /// </summary>
    public void Update(float delta)
    {
        // ReSharper disable once ForCanBeConvertedToForeach (collection might be modified)
        for (int i = 0; i < scene.Count; i++)
            scene[i].Update(delta);
    }

    /// <summary>
    /// Calls the <seealso cref="SceneObject.Frame"/> method on every object added to the scene.
    /// </summary>
    public void Frame(float delta)
    {
        // ReSharper disable once ForCanBeConvertedToForeach (collection might be modified)
        for (int i = 0; i < scene.Count; i++)
            scene[i].Frame(delta);
    }

    /// <summary>
    /// Calls the <seealso cref="SceneObject.Draw"/> method on every object added to the scene.
    /// </summary>
    public void Draw()
    {
        // ReSharper disable once ForCanBeConvertedToForeach (collection might be modified)
        for (int i = 0; i < scene.Count; i++)
        {
            if (scene[i] is DrawableObject d)
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
    public void Destroy(SceneObject o)
    {
        DestroyNoDispose(o);
        if (o is IDisposable d) d.Dispose();
    }

    /// <summary>
    /// Destroys an object - stops updating and drawing it.
    /// Does not dispose IDisposables.
    /// </summary>
    /// <param name="o">The entity to destroy</param>
    public void DestroyNoDispose(SceneObject o)
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

    public void Load<T>() where T : SceneGenerator, new()
    {
        T t = new() { Scene = this };
        t.Start();
        Start();
    }
}
