using System;
using System.Reflection;

namespace Crimson.Scenes
{
    public abstract class SceneGenerator
    {
        public Scene Scene { get; set; }
        public static Entity CurrentEntity { get; private set; }

        public abstract void Start();

        protected Entity Spawn(Action ent, Vector2 position, string name = "")
        {
            Entity e = new()
            {
                Position = position,
                Name = name
            };
            Entity prev = CurrentEntity;
            CurrentEntity = e;
            ent();
            CurrentEntity = prev; // makes nested Spawns work (you'll keep editing the wrong entity after exiting ent).
            Scene.AddObject(e);
            return e;
        }

        protected Entity Spawn(Action ent, string name = "") => Spawn(ent, Vector2.Zero, name);
        protected Entity Spawn(Action ent, float x, float y, string name = "") => Spawn(ent, new(x, y), name);

        protected internal static T Comp<T>(Action<T> awake = null) where T : Component, new()
        {
            T t = CurrentEntity.AddComponent<T>();
            awake?.Invoke(t);
            return t;
        }
    }
}
