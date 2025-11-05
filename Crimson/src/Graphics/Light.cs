namespace Crimson;

public class Light : SceneObject
{
    /// <summary> The light's radius. </summary>
    public float Radius { get; set; }

    /// <summary> The intensity of the light's emission. </summary>
    public float Strength { get; set; } = 1;

    /// <summary> The color of the light's emission. </summary>
    public Color Color { get; set; } = Color.White;

    private Scene scene;
    public override Scene Scene
    {
        get => scene;
        internal set
        {
            scene = value;
            scene?.lights.Add(this);
        }
    }

    /// <summary>
    /// The amount of ambient light in the scene.
    /// 1 is completely bright (lights won't work) and 0 is completely dark (you won't see anything without lights).
    /// </summary>
    public static float Ambience { get; set; } = 1;

    public override void Start() { }
    public override void Update(float delta) { }
    public override void Frame(float delta) { }
    public override void OnDestroy() { }
}

public sealed partial class Scene
{
    internal List<Light> lights = new();

    internal void DrawLights(Material mat)
    {
        mat.SetUniform("LIGHTS", lights.Count);
        for (int i = 0; i < lights.Count; i++)
        {
            mat.SetUniform($"lights[{i}].pos", lights[i].Position - Camera.CurrentOrigin);
            mat.SetUniform($"lights[{i}].radius", lights[i].Radius);
            mat.SetUniform($"lights[{i}].strength", lights[i].Strength);
            mat.SetUniform($"lights[{i}].color", lights[i].Color);
        }
    }

    public Light CreateLight(Vector2 position, float radius, float strength, Color color)
    {
        Light l = new() { Position = position, Radius = radius, Strength = strength, Color = color };
        AddObject(l);
        return l;
    }

    public Light CreateLight(SceneObject parent, float radius, float strength, Color color, Vector2 offset = new())
    {
        Light l = new() { Parent = parent, LocalPosition = offset, Radius = radius, Strength = strength, Color = color };
        AddObject(l);
        return l;
    }
}
