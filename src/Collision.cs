using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Crimson
{
    // /// <summary>
    // /// Used internally to cast to <seealso cref="ICollider<T>"/>.
    // /// Use the generic variant.
    // /// </summary>
    // public interface ICollider
    // {
    //     Vector2 Move(Vector2 velocity);
    // }
    //
    // public interface ICollider<T> : ICollider
    // {
    //     bool IsColliding(Vector2 origin, Vector2 direction, out T info);
    //     Vector2 Respond(Controller body, Vector2 velocity, T info);
    // }
    //
    // public class Controller : Component
    // {
    //     public List<ICollider> Colliders { get; set; } = new();
    //
    //     public void Move(Vector2 velocity)
    //     {
    //         foreach (ICollider c in Colliders)
    //             velocity = c.Move(velocity);
    //     }
    // }
    //
    // public struct BoxCollisionInfo
    // {
    //     public Vector2 Normal { get; set; }
    //     public Rect Collider { get; set; }
    //     public Entity Body { get; set; }
    //     public float Time { get; set; }
    // }
    //
    // public class BoxCollider : ICollider<BoxCollisionInfo>
    // {
    //     public Rect Bounds { get; set; }
    //
    //     public bool IsColliding(Vector2 origin, Vector2 direction, out BoxCollisionInfo info)
    //     {
    //         info = new() { Collider = Bounds };
    //         Vector2 tNear = (Bounds.Position - origin) / direction;
    //         Vector2 tFar = (Bounds.Position + Bounds.Size - origin) / direction;
    //
    //         if (float.IsNaN(tFar.y) || float.IsNaN(tFar.x) || float.IsNaN(tNear.y) || float.IsNaN(tNear.x))
    //             return false;
    //
    //         if (tNear.x > tFar.x) Mathf.Swap(ref tNear.x, ref tFar.x);
    //         if (tNear.y > tFar.y) Mathf.Swap(ref tNear.y, ref tFar.y);
    //
    //         if (tNear.x > tFar.y || tNear.y > tFar.x) return false;
    //
    //         info.Time = MathF.Max(tNear.x, tNear.y);
    //         float timeFar = MathF.Min(tFar.x, tFar.y);
    //
    //         if (tNear.x > tNear.y)
    //             info.Normal = direction.x < 0 ? Vector2.Right : Vector2.Left;
    //         else if (tNear.x < tNear.y)
    //             info.Normal = direction.y < 0 ? Vector2.Down : Vector2.Up;
    //         else
    //             info.Normal = Vector2.Zero;
    //
    //         return timeFar >= 0 && info.Time >= 0 && info.Time < 1;
    //     }
    //
    //     public Vector2 Respond(Controller body, Vector2 velocity, BoxCollisionInfo info)
    //     {
    //         body.Position += velocity * info.Time;
    //         float rem = 1 - info.Time;
    //         float dot = (velocity.x * info.Normal.y + velocity.y * info.Normal.x) * rem;
    //         return new(
    //             dot * info.Normal.y,
    //             dot * info.Normal.x
    //         );
    //     }
    //
    //     Vector2 ICollider.Move(Vector2 velocity)
    //     {
    //
    //     }
    // }

    public class Controller : Component
    {
        private ICollide collider;

        public override void Start()
        {
            base.Start();
            collider = GetComponent<ICollide>();
        }

        public void Move(Vector2 velocity)
        {
            List<object> collisions = new();
            foreach (ICollide c in Scene.GetComponentsOfType<ICollide>())
            {
                if (c == collider) continue;
                if (collider.IsCollidingAny(collider, c, velocity, out object info))
                    collisions.Add(info);
            }
            collider.RespondAny(this, velocity, collisions);
        }
    }

    /// <summary> Used internally. You're probably looking for the generic version. </summary>
    public interface ICollide
    {
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

        bool IsColliding(T1 a, T2 b, Vector2 velocity, out TInfo info);
        void Respond(Controller body, Vector2 velocity, List<TInfo> collisions);
    }

    public struct BoxCollisionInfo
    {
        public Vector2 Normal { get; set; }
        public float Time { get; set; }
        public BoxCollider Target { get; set; }
    }

    public class BoxCollider : Component, ICollide<BoxCollider, BoxCollider, BoxCollisionInfo>
    {
        public Rect Bounds => new(Position, Size);
        public Vector2 Offset { get; set; }
        public Vector2 Size { get; set; }

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
            info = new() { Target = target };
            Rect a = source.Bounds;
            Rect b = new(target.Position - target.Size / 2 - a.Size / 2, target.Size + a.Size);
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
                // if multiple collisions were registered this frame, and one of them was going into a corner,
                // ignore it (or else you would get stuck on corners between blocks).
                // only one collision means you were going into a real corner, so we shouldn't let you pass.
                if (info.Normal == Vector2.Zero && collisions.Count != 1) continue;

                // stop body from actually moving
                body.Position += velocity * info.Time;
                float rem = 1 - info.Time;
                float dot = (velocity.x * info.Normal.y + velocity.y * info.Normal.x) * rem;
                velocity.x = dot * info.Normal.y;
                velocity.y = dot * info.Normal.x;
            }
            Position += velocity;
        }
    }
}
