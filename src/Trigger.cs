using System;
using System.Collections.Generic;

namespace Crimson
{
    public class StaticTrigger : Component, ICollidable
    {
        private Dictionary<Entity, Controller> colliding = new();

        public Collider Collider { get; private set; }

        public CollisionLayer Layer { get; set; } = 1;

        public float CollisionTime { get; private set; }
        public Vector2 CollisionNormal { get; private set; }

        public event Action<Entity> Entered;
        public event Action<Entity> Exited;

        public override void Start()
        {
            base.Start();

            Collider = GetComponent<Collider>();
        }

        public void Collide(float time, Vector2 normal, ref Vector2 vel, Controller body)
        {
            CollisionTime = time;
            CollisionNormal = normal;
            if (!colliding.ContainsKey(body.Parent))
            {
                Entered?.Invoke(body.Parent);
                colliding.Add(body.Parent, body);
            }
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            UpdateCollisions(new(), c => Collider.Intersects(c.Collider));
        }

        protected void UpdateCollisions(Vector2 target, Func<Controller, bool> intersects)
        {
            var toRemove = new List<Entity>();

            foreach (var (ent, cont) in colliding)
            {
                if (!intersects(cont) &&
                    !cont.Intersects(Collider, cont.Velocity, target, out _, out _))
                {
                    Exited?.Invoke(ent);
                    toRemove.Add(ent);
                }
            }

            foreach (var ent in toRemove)
                colliding.Remove(ent);
        }
    }

    public class Trigger : StaticTrigger, IDynamicCollidable
    {
        public Vector2 Velocity { get; private set; }

        public void Move(Vector2 velocity)
        {
            Velocity = velocity;
            Position += Velocity * Engine.Delta;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            UpdateCollisions(Velocity, Intersects);
        }

        private bool Intersects(Controller c)
        {
            Vector2 vel = new Vector2
            {
                x = c.Velocity.x <= 0 ? Velocity.x - c.Velocity.x : 0,
                y = c.Velocity.y <= 0 ? Velocity.y - c.Velocity.y : 0
            };
            return Position.x < c.Position.x + c.Size.x && Position.x + Collider.Size.x + vel.x > c.Position.x &&
                   Position.y < c.Position.y + c.Size.y && Position.y + Collider.Size.y + vel.x > c.Position.y;
        }
    }
}
