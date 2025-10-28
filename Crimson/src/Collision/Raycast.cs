namespace Crimson;

public struct RaycastHit
{
    public Entity target;
    public Vector2 point;
    public Vector2 normal;
    public Component collider;
}

/// <summary>
/// Used internally. You're probably looking for the generic version.
/// </summary>
public interface IRaycast
{
    bool IntersectsAny(object collider, Vector2 origin, Vector2 velocity, CollisionLayer layer, out RaycastHit hit);
}

public interface IRaycast<in T> : IRaycast
{
    bool Intersects(T collider, Vector2 origin, Vector2 velocity, CollisionLayer layer, out RaycastHit hit);
    bool IRaycast.IntersectsAny(object collider, Vector2 origin, Vector2 velocity, CollisionLayer layer ,out RaycastHit hit) =>
        Intersects((T)collider, origin, velocity, layer, out hit);
}

public static class Raycast
{
    public static IEnumerable<RaycastHit> IntersectsAll(Vector2 from, Vector2 to, CollisionLayer layer,
                                                        params Entity[] ignore)
    {
        Vector2 vel = to - from;
        foreach (IRaycast col in Engine.Scene.GetComponentsOfType<IRaycast>())
        {
            if (col is Component c && ignore.Contains(c.Entity)) continue;
            if (col.IntersectsAny(col, from, vel, layer, out RaycastHit hit))
                yield return hit;
        }
    }

    public static bool Intersects(Vector2 from, Vector2 to, CollisionLayer layer, out RaycastHit hit, params Entity[] ignore)
    {
        List<RaycastHit> hits = IntersectsAll(from, to, layer, ignore).ToList();
        if (hits.Count == 0) // if the ray hit nothing
        {
            hit = default;
            return false;
        }

        // get the closest collision
        hits.Sort((a, b) => a.point.SquareDistanceTo(from).CompareTo(b.point.SquareDistanceTo(from)));
        hit = hits[0];
        return true;
    }
}
