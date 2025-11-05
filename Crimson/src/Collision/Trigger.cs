namespace Crimson;

public class Trigger : Component
{
    private List<ICollide> colliders;

    private HashSet<Entity> collisions = new();
    public IReadOnlyCollection<Entity> Collisions => collisions;

    public event Action<Entity> Entered;
    public event Action<Entity> Exited;

    public override void Start()
    {
        base.Start();
        colliders = GetComponents<ICollide>().Where(c => !c.Block).ToList();
        Entity.ComponentAdded += c =>
        {
            if (c is ICollide l && !l.Block) colliders.Add(l);
        };
        Entity.ComponentRemoved += c =>
        {
            if (c is ICollide l && !l.Block) colliders.Remove(l);
        };
    }

    public override void Update(float delta)
    {
        base.Update(delta);

        // array and not IEnumerable in order to avoid multiple enumeration
        Controller[] controllers = Scene.GetComponentsOfType<Controller>().ToArray();
        foreach (ICollide collider in colliders)
        {
            foreach (Controller controller in controllers)
            {
                Entity e = controller.Entity;

                bool colliding = false;
                bool contains = collisions.Contains(e);

                foreach (ICollide c in controller.Colliders)
                {
                    // Some collision algorithms assume objects are not in collision to begin with, and would output false
                    // in case they're already inside of each other.
                    // If they were colliding during the last frame, use an algorithm which doesn't do that.
                    if (contains)
                    {
                        if (c.IsStillCollidingAny(c, collider, controller.Velocity))
                        {
                            colliding = true;
                            break;
                        }
                    }
                    else if (c.IsCollidingAny(c, collider, controller.Velocity, out _))
                    {
                        colliding = true;
                        break;
                    }
                }
                if (colliding && !contains)
                {
                    Entered?.Invoke(e);
                    collisions.Add(e);
                }
                else if (!colliding && contains)
                {
                    Exited?.Invoke(e);
                    collisions.Remove(e);
                }
            }
        }
    }
}
