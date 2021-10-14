namespace Crimson
{
    public interface ICollidable
    {
        Collider Collider { get; }
        CollisionLayer Layer { get; }

        void Collide(float time, Vector2 normal, ref Vector2 vel, Controller body);
    }

    public interface IDynamicCollidable : ICollidable
    {
        Vector2 Velocity { get; }

        void Move(Vector2 velocity);
    }

    public struct CollisionLayer
    {
        public ulong data;

        public CollisionLayer(ulong l) => data = l;

        public void SetBit(int n, bool val) =>
            data = val ? data | (uint)(1 << n) : data & (uint)~(1 << n);

        public bool GetBit(int n) => (data & (uint)(1 << n)) == 1;

        public static implicit operator ulong(CollisionLayer c) => c.data;
        public static implicit operator CollisionLayer(ulong l) => new(l);
    }

    public class Collider : Component
    {
        public Vector2 Size { get; set; }
        public Vector2 Offset { get; set; }
        public Color DrawColor { get; set; } = Color.None;

        public override Vector2 Position { get => base.Position + Offset; set => base.Position = value; }

        /// <summary>
        /// Simple AABB check. Only works for small velocities, but very fast.
        /// </summary>
        public bool Intersects(Collider c) =>
            Position.x < c.Position.x + c.Size.x && Position.x + Size.x > c.Position.x &&
            Position.y < c.Position.y + c.Size.y && Position.y + Size.y > c.Position.y;

        public override void Draw()
        {
            base.Draw();
            if (DrawColor != Color.None)
                Graphics.DrawRect(Position, Size, DrawColor);
        }
    }

    public struct CollisionInfo
    {
        public float time;
        public Vector2 normal;
        public ICollidable body;

        public CollisionInfo(float time, Vector2 normal, ICollidable body)
        {
            this.time = time;
            this.normal = normal;
            this.body = body;
        }
    }
}
