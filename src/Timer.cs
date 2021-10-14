using System;

namespace Crimson
{
    public class Timer : ISceneObject
    {
        public event Action Timeout;

        public float TimeLeft { get; private set; }
        public float Duration { get; set; }
        public bool Loop { get; set; }
        public bool Running { get; private set; }

        public Scene Scene { get; internal set; }

        public bool SyncToPhysics { get; set; }

        public Timer(float duration, bool loop = true, bool syncToPhysics = true)
        {
            Duration = duration;
            TimeLeft = Duration;
            Loop = loop;
            SyncToPhysics = syncToPhysics;
        }

        public void Begin()
        {
            if (Scene == null)
                throw new Exception("Timer was not added to the scene but Start() was called.");
            Running = true;
        }

        public void Stop()
        {
            Pause();
            TimeLeft = Duration;
        }

        public void Pause()
        {
            if (Scene == null)
                throw new Exception("Timer was not added to the scene but Stop() was called.");
            Running = false;
        }

        private void Progress(float delta)
        {
            if (!Running) return;

            TimeLeft -= delta;
            if (TimeLeft <= 0)
            {
                Timeout?.Invoke();
                if (!Loop) Stop();
                else TimeLeft = Duration;
            }
        }

        void ISceneObject.Update(float delta)
        {
            if (SyncToPhysics) Progress(delta);
        }

        void ISceneObject.Frame(float delta)
        {
            if (!SyncToPhysics) Progress(delta);
        }

        void ISceneObject.SetScene(Scene value) => Scene = value;
        void ISceneObject.Start() { }
    }
}
