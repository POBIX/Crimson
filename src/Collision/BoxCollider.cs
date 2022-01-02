namespace Crimson;

public struct BoxCollisionInfo
{
    public Vector2 Normal { get; set; }
    public float Time { get; set; }
    public BoxCollider Target { get; set; }
    public Entity Entity { get; set; }
}

public class BoxCollider : Component, ICollide<BoxCollider, BoxCollider, BoxCollisionInfo>,
    IRaycast<BoxCollider>
{
    public Rect Bounds => new(Position + Offset, Size);
    public Vector2 Offset { get; set; }
    public Vector2 Size { get; set; }

    public bool Block { get; set; } = true;
    public CollisionLayer Layer { get; set; }

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

    private bool IsColliding(BoxCollider source, BoxCollider target, Vector2 origin, Vector2 velocity,
                             out BoxCollisionInfo info)
    {
        info = new() { Target = target, Entity = target.Entity };

        // if the objects share no common layers, there's no collision.
        if ((source.Layer & target.Layer) == 0) return false;

        Rect a = source.Bounds;
        Rect b = new(target.Bounds.Position - target.Size / 2 - a.Size / 2, target.Size + a.Size);
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

    public bool IsColliding(BoxCollider source, BoxCollider target, Vector2 velocity, out BoxCollisionInfo info) =>
        IsColliding(source, target, source.Bounds.Position, velocity, out info);

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

        return a.x < b.x + b.w && a.x + a.w > b.x &&
               a.y < b.y + b.h && a.y + a.h > b.y;
    }

    private Entity targetEntity = new();
    public bool Intersects(BoxCollider collider, Vector2 origin, Vector2 velocity, CollisionLayer layer, out RaycastHit hit)
    {
        BoxCollider target = new() { Block = true, Offset = origin, Size = Vector2.One, Layer = layer };
        targetEntity.AddComponent(target);
        if (IsColliding(target, collider, velocity, out BoxCollisionInfo info))
        {
            hit = new() { collider = info.Entity, normal = info.Normal, point = origin + velocity * (info.Time) };
            return true;
        }
        hit = default;
        return false;
    }
}
