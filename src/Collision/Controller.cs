using System.Collections.ObjectModel;

namespace Crimson;

public class Controller : Component
{
    private List<ICollide> colliders;
    public ReadOnlyCollection<ICollide> Colliders => colliders.AsReadOnly();

    /// <summary>
    /// The controller's last reported velocity. (Updated with <see cref="Move"/>)
    /// </summary>
    public Vector2 Velocity { get; private set; }

    public override void Start()
    {
        base.Start();
        colliders = GetComponents<ICollide>().Where(c => c.Block).ToList();
        Entity.ComponentAdded += c =>
        {
            if (c is ICollide l && l.Block) colliders.Add(l);
        };
        Entity.ComponentRemoved += c =>
        {
            if (c is ICollide l && l.Block) colliders.Remove(l);
        };
    }

    public void Move(Vector2 velocity)
    {
        List<object> collisions = new();
        // list and not IEnumerable in order to avoid multiple enumeration
        List<ICollide> sceneColliders = Scene.GetComponentsOfType<ICollide>().ToList();
        foreach (ICollide collider in colliders)
        {
            foreach (ICollide c in sceneColliders)
            {
                if (c == collider) continue;
                if (collider.IsCollidingAny(collider, c, velocity, out object info))
                    collisions.Add(info);
            }
            collider.RespondAny(this, velocity, collisions);
        }

        Velocity = velocity;
    }
}
