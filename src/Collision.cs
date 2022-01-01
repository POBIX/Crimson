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
        colliders = GetComponents<ICollide>().ToList();
        Entity.ComponentAdded += c =>
        {
            if (c is ICollide l) colliders.Add(l);
        };
        Entity.ComponentRemoved += c =>
        {
            if (c is ICollide l) colliders.Remove(l);
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

public class Trigger : Component
{
    private List<ICollide> colliders;

    private HashSet<Entity> collisions = new();

    public event Action<Entity> Entered;
    public event Action<Entity> Exited;

    public override void Start()
    {
        base.Start();
        colliders = GetComponents<ICollide>().ToList();
        Entity.ComponentAdded += c =>
        {
            if (c is ICollide l) colliders.Add(l);
        };
        Entity.ComponentRemoved += c =>
        {
            if (c is ICollide l) colliders.Remove(l);
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

/// <summary> Used internally. You're probably looking for the generic version. </summary>
public interface ICollide
{
    internal bool IsStillCollidingAny(ICollide a, ICollide b, Vector2 velocity);
    internal bool IsCollidingAny(ICollide a, ICollide b, Vector2 velocity, out object info);
    internal void RespondAny(Controller body, Vector2 velocity, List<object> collisions);
}

public interface ICollide<in T1, in T2, TInfo> : ICollide where TInfo : new()
{
    bool ICollide.IsCollidingAny(ICollide a, ICollide b, Vector2 velocity, out object info)
    {
        if (a is T1 t1 && b is T2 t2 && IsColliding(t1, t2, velocity, out TInfo tInfo))
        {
            info = tInfo;
            return true;
        }
        info = new TInfo();
        return false;
    }

    void ICollide.RespondAny(Controller body, Vector2 velocity, List<object> collisions) =>
        Respond(body, velocity, collisions.Cast<TInfo>().ToList());

    bool ICollide.IsStillCollidingAny(ICollide a, ICollide b, Vector2 velocity) =>
        IsStillColliding((T1)a, (T2)b, velocity);

    bool IsColliding(T1 a, T2 b, Vector2 velocity, out TInfo info);
    void Respond(Controller body, Vector2 velocity, List<TInfo> collisions);

    /// <summary>
    /// Implement only if your algorithm assumes objects are NOT in collision to begin with.
    /// This will only ever get called if objects were found to be colliding in the previous frame (using <seealso cref="IsColliding"/>),
    /// and is only used with triggers.
    /// </summary>
    bool IsStillColliding(T1 a, T2 b, Vector2 velocity) => IsColliding(a, b, velocity, out _);
}

public struct BoxCollisionInfo
{
    public Vector2 Normal { get; set; }
    public float Time { get; set; }
    public BoxCollider Target { get; set; }
    public Entity Entity { get; set; }
}

public class BoxCollider : Component, ICollide<BoxCollider, BoxCollider, BoxCollisionInfo>
{
    public Rect Bounds => new(Position + Offset, Size);
    public Vector2 Offset { get; set; }
    public Vector2 Size { get; set; }

    public bool Block { get; set; } = true;

    private static bool GetBroadphase(Vector2 pos, Rect rect, Vector2 vel)
    {
        Vector2 size = rect.Size;
        var r = new Rect
        {
            x = vel.x > 0 ? pos.x : pos.x + vel.x,
            y = vel.y > 0 ? pos.y : pos.y + vel.y,
            w = vel.x > 0 ? vel.x + size.x : size.x - vel.x,
            h = vel.y > 0 ? vel.y + size.y : size.y - vel.y
        };
        return r.Intersects(rect);
    }

    public bool IsColliding(BoxCollider source, BoxCollider target, Vector2 velocity, out BoxCollisionInfo info)
    {
        info = new() { Target = target, Entity = target.Entity };
        Rect a = source.Bounds;
        Rect b = new(target.Bounds.Position - target.Size / 2 - a.Size / 2, target.Size + a.Size);
        Vector2 origin = a.Position;
        if (!GetBroadphase(origin, Bounds, velocity))
            return false;
        Vector2 tNear = (b.Position - origin) / velocity;
        Vector2 tFar = (b.Position + b.Size - origin) / velocity;

        if (float.IsNaN(tFar.y) || float.IsNaN(tFar.x) || float.IsNaN(tNear.y) || float.IsNaN(tNear.x))
            return false;

        if (tNear.x > tFar.x) Mathf.Swap(ref tNear.x, ref tFar.x);
        if (tNear.y > tFar.y) Mathf.Swap(ref tNear.y, ref tFar.y);

        if (tNear.x > tFar.y || tNear.y > tFar.x) return false;

        info.Time = MathF.Max(tNear.x, tNear.y);
        float timeFar = MathF.Min(tFar.x, tFar.y);

        if (tNear.x > tNear.y)
            info.Normal = velocity.x < 0 ? Vector2.Right : Vector2.Left;
        else if (tNear.x < tNear.y)
            info.Normal = velocity.y < 0 ? Vector2.Down : Vector2.Up;
        else
            info.Normal = Vector2.Zero;

        return timeFar >= 0 && info.Time >= 0 && info.Time < 1;
    }

    public void Respond(Controller body, Vector2 velocity, List<BoxCollisionInfo> collisions)
    {
        collisions.Sort((a, b) => a.Time.CompareTo(b.Time));

        foreach (BoxCollisionInfo info in collisions)
        {
            if (!info.Target.Block) continue;
            // if multiple collisions were registered this frame, and one of them was going into a corner,
            // ignore it (or else you would get stuck on corners between blocks).
            // only one collision means you were going into a real corner, so we shouldn't let you pass.
            if (info.Normal == Vector2.Zero && collisions.Count != 1) continue;

            // stop body from actually moving into the collider
            body.Position += velocity * info.Time;
            float rem = 1 - info.Time;
            float dot = (velocity.x * info.Normal.y + velocity.y * info.Normal.x) * rem;
            velocity.x = dot * info.Normal.y;
            velocity.y = dot * info.Normal.x;
        }
        Position += velocity;
    }

    public bool IsStillColliding(BoxCollider ac, BoxCollider bc, Vector2 velocity)
    {
        Rect a = new(ac.Position + velocity - ac.Size / 2, ac.Size);
        Rect b = new(bc.Position - bc.Size / 2, bc.Size);

        return a.x < b.x + b.w &&
               a.x + a.w > b.x &&
               a.y < b.y + b.h &&
               a.y + a.h > b.y;

    }
}
