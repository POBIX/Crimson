namespace Crimson;

public abstract record Animation
{
    protected internal abstract void Play(AnimationPlayer player);
    protected internal abstract void Stop(AnimationPlayer player);
    protected internal abstract void Pause(AnimationPlayer player);
    protected internal abstract void Resume(AnimationPlayer player);
}

public record SimpleAnimation : Animation
{
    public Rect Clip { get; init; }
    public Vector2 Frames { get; init; }
    public float Interval { get; init; }
    public bool Loop { get; init; }
    public bool SyncToPhysics { get; init; } = false;

    public event Action Finished;

    private Timer timer;

    private void Advance(AnimationPlayer player)
    {
        if (++player.Sprite.FrameH >= player.Sprite.FramesH)
        {
            player.Sprite.FrameH = 0;
            if (++player.Sprite.FrameV >= player.Sprite.FramesV)
            {
                player.Sprite.FrameV = 0;
                if (!Loop)
                {
                    Finished?.Invoke();
                    timer.Scene.Destroy(timer);
                }
            }
        }
    }

    protected internal override void Play(AnimationPlayer player)
    {
        player.Sprite.Clip = Clip;
        player.Sprite.Frames = Frames;
        timer = player.Scene.CreateTimer(Interval, () => Advance(player), true, true, player.Entity, SyncToPhysics);
    }

    protected internal override void Stop(AnimationPlayer player)
    {
        timer?.Stop();
        player.Scene.Destroy(timer);
    }

    protected internal override void Pause(AnimationPlayer player) => timer?.Pause();
    protected internal override void Resume(AnimationPlayer player) => timer?.Run();
}

/// <summary>
/// Define and play animations on sprites
/// The entity must have a <see cref="Sprite"/>!
/// </summary>
public class AnimationPlayer : Component
{
    public Sprite Sprite { get; set; }
    private Dictionary<string, Animation> animations = new();

    public Animation Current { get; private set; }

    public override void Start()
    {
        base.Start();
        Sprite = GetComponent<Sprite>();
    }

    public void Add(string name, Animation animation)
    {
        if (!animations.TryAdd(name, animation))
            throw new ArgumentException($"Animation player already has animation named: {name}!");
    }

    public void Play(string name)
    {
        if (!animations.TryGetValue(name, out Animation animation))
            throw new ArgumentException($"Animation player has no animation named: {name}!");
        animation.Play(this);
        Current = animation;
    }

    public void Stop()
    {
        Current?.Stop(this);
        Current = null;
    }

    public void Pause() => Current?.Pause(this);
    public void Resume() => Current?.Resume(this);

    public bool HasAnimation(string name) => animations.ContainsKey(name);
}
