using System;
using System.Collections.Generic;

namespace Crimson
{
    public class Controller : Component, IDynamicCollidable
    {
        public Collider Collider { get; private set; }
        public CollisionLayer Layer { get; set; } = 1;

        public Vector2 Size => Collider.Size;

        public override void Start()
        {
            base.Start();
            Collider = GetComponent<Collider>();
        }

        internal bool Intersects(Collider target, Vector2 vel, Vector2 targetVel, out Vector2 normal,
                                 out float time)
        {
            normal = new Vector2();
            time = 0;
            vel -= targetVel;

            Rect expanded = new Rect(target.Position - target.Size / 2 - Collider.Size / 2, target.Size + Collider.Size);

            return Raycast.GetBroadphase(Collider.Position, expanded, vel) &&
                   Raycast.IntersectsTarget(Collider.Position, vel, expanded, out normal, out time);
        }

        public Vector2 Velocity { get; private set; }

        public void Move(Vector2 velocity)
        {
            Velocity = velocity;

            var collisions = new List<CollisionInfo>();
            foreach (var body in Scene.GetComponentsOfType<ICollidable>())
            {
                if (body == this || (body.Layer & Layer) == 0) continue;
                Vector2 target = body is IDynamicCollidable d ? d.Velocity : new();
                if (Intersects(body.Collider, velocity, target, out Vector2 normal, out float time))
                    collisions.Add(new CollisionInfo(time, normal, body));
            }

            collisions.Sort((a, b) => a.time.CompareTo(b.time));

            foreach (CollisionInfo c in collisions)
            {
                // if multiple collisions were registered this frame, and one of them was going into a corner,
                // ignore it (or else you would get stuck on corners between blocks).
                // only one collision means you were going into a real corner, so we shouldn't let you pass.
                if (c.normal == Vector2.Zero && collisions.Count != 1) continue;
                c.body.Collide(c.time, c.normal, ref velocity, this);
            }

            Position += velocity;
        }

        public void Collide(float time, Vector2 normal, ref Vector2 vel, Controller body)
        {
            body.Position += vel * time;
            float rem = 1 - time;
            float dot = (vel.x * normal.y + vel.y * normal.x) * rem;
            vel.x = dot * normal.y;
            vel.y = dot * normal.x;
        }
    }
}
