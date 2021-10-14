namespace Crimson
{
    public class Body : Component, ICollidable
    {
        public Collider Collider { get; set; }

        public CollisionLayer Layer { get; set; } = 1;

        public override void Start()
        {
            base.Start();
            Collider = GetComponent<Collider>();
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
