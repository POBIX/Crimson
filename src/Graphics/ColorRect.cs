namespace Crimson;

public class ColorRect : Component
{
    public Vector2 Offset { get; set; }
    public Vector2 Size { get; set; }
    public Color Color { get; set; }

    public override void Draw()
    {
        base.Draw();
        Graphics.FillRect(Position + Offset, Size, Color);
    }
}
