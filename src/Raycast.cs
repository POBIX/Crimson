using System;
using System.Linq;

namespace Crimson
{
    public struct RaycastHit
    {
        public Entity collider;
        public Vector2 point;
        public Vector2 normal;
        public float rayTime;
    }

    // public static class Raycast
    // {
    //     internal static bool IntersectsTarget(Vector2 origin, Vector2 dir, Rect target, out Vector2 normal, out float time)
    //     {
    //         normal = new();
    //         time = 0;
    //
    //         Vector2 tNear = (target.Position - origin) / dir;
    //         Vector2 tFar = (target.Position + target.Size - origin) / dir;
    //
    //         if (float.IsNaN(tFar.y) || float.IsNaN(tFar.x) || float.IsNaN(tNear.y) || float.IsNaN(tNear.x))
    //             return false;
    //
    //         if (tNear.x > tFar.x) Mathf.Swap(ref tNear.x, ref tFar.x);
    //         if (tNear.y > tFar.y) Mathf.Swap(ref tNear.y, ref tFar.y);
    //
    //         if (tNear.x > tFar.y || tNear.y > tFar.x) return false;
    //
    //         time = MathF.Max(tNear.x, tNear.y);
    //         float timeFar = MathF.Min(tFar.x, tFar.y);
    //
    //         if (tNear.x > tNear.y)
    //             normal = dir.x < 0 ? Vector2.Right : Vector2.Left;
    //         else if (tNear.x < tNear.y)
    //             normal = dir.y < 0 ? Vector2.Down : Vector2.Up;
    //         else
    //             normal = Vector2.Zero;
    //
    //         return timeFar >= 0 && time >= 0 && time < 1;
    //     }
    //
    //     private static bool IntersectsVel(Vector2 origin, Vector2 vel, out RaycastHit hit, ulong layer,
    //                                       params Entity[] ignore)
    //     {
    //         hit = new();
    //         foreach (ICollidable c in Engine.Scene.GetComponentsOfType<ICollidable>())
    //         {
    //             if (ignore.Contains(c.Collider.Parent) || (c.Layer & layer) == 0) continue;
    //
    //             Collider t = c.Collider;
    //             Rect e = new Rect(t.Position - t.Size / 2, t.Size);
    //
    //             if (GetBroadphase(origin, e, vel) &&
    //                 IntersectsTarget(origin, vel, e, out hit.normal, out hit.rayTime))
    //             {
    //                 hit.collider = t.Parent;
    //                 hit.point = origin + vel * hit.rayTime;
    //                 return true;
    //             }
    //         }
    //         return false;
    //     }
    //
    //     public static bool Intersects(Vector2 origin, Vector2 dir, float length, out RaycastHit hit, ulong layer,
    //                                   params Entity[] ignore)
    //     {
    //         return IntersectsVel(origin, dir * length, out hit, layer, ignore);
    //     }
    //
    //     public static bool Intersects(Vector2 from, Vector2 to, out RaycastHit hit, ulong layer, params Entity[] ignore)
    //     {
    //         return IntersectsVel(from, to - from, out hit, layer, ignore);
    //     }
    //
    //     internal static bool GetBroadphase(Vector2 pos, Rect rect, Vector2 vel)
    //     {
    //         Vector2 size = rect.Size;
    //         var r = new Rect
    //         {
    //             x = vel.x > 0 ? pos.x : pos.x + vel.x,
    //             y = vel.y > 0 ? pos.y : pos.y + vel.y,
    //             w = vel.x > 0 ? vel.x + size.x : size.x - vel.x,
    //             h = vel.y > 0 ? vel.y + size.y : size.y - vel.y
    //         };
    //         return r.Intersects(rect);
    //     }
    // }
}
