using System.Text;
using OpenGL;

namespace Crimson;

public abstract class Shader : Component, IDisposable
{
    private Dictionary<string, int> uniformLocs = new();

    public delegate void UniformSetter<in T>(uint program, int location, T value);

    private static Dictionary<Type, UniformSetter<object>> uniformSetters = new();

    /// <summary> The time elapsed since the Material was created. </summary>
    public float Time { get; private set; }

    protected internal uint program;

    static Shader()
    {
        RegisterUniformSetter<int>(Gl.ProgramUniform1);
        RegisterUniformSetter<float>(Gl.ProgramUniform1);
        RegisterUniformSetter<uint>(Gl.ProgramUniform1);
        RegisterUniformSetter<double>(Gl.ProgramUniform1);
        RegisterUniformSetter<bool>((p, l, v) => Gl.ProgramUniform1(p, l, v ? 1 : 0));
        RegisterUniformSetter<Vector2>((p, l, v) => Gl.ProgramUniform2(p, l, v.x, v.y));
        RegisterUniformSetter<Vector3>((p, l, v) => Gl.ProgramUniform3(p, l, v.x, v.y, v.z));
        RegisterUniformSetter<Vector4>((p, l, v) => Gl.ProgramUniform4(p, l, v.x, v.y, v.z, v.w));
        RegisterUniformSetter<Color>((p, l, v) => Gl.ProgramUniform4(p, l, v.r, v.g, v.b, v.a));
    }

    protected Shader() => program = Gl.CreateProgram();

    protected static void Print(string s)
    {
        if (s != "") Console.WriteLine(s);
    }

    protected const int MaxLogLength = 2048;
    protected static string GetShaderLog(uint shader)
    {
        var sb = new StringBuilder(MaxLogLength);
        Gl.GetShaderInfoLog(shader, MaxLogLength, out _, sb);
        return sb.ToString();
    }

    protected static string GetProgramLog(uint program)
    {
        var sb = new StringBuilder(MaxLogLength);
        Gl.GetProgramInfoLog(program, MaxLogLength, out _, sb);
        return sb.ToString();
    }

    protected static void PrintProgramLog(uint program) => Print(GetProgramLog(program));
    protected static void PrintShaderLog(uint shader) => Print(GetShaderLog(shader));

    public int GetUniformLocation(string name)
    {
        if (uniformLocs.ContainsKey(name))
            return uniformLocs[name];
        int location = Gl.GetUniformLocation(program, name);
        uniformLocs.Add(name, location);
        return location;
    }

    public void SetUniform<T>(string name, T value)
    {
        if (!uniformSetters.ContainsKey(typeof(T)))
            throw new KeyNotFoundException($"No setter registered for type {typeof(T)}. Use Shader.RegisterUniformSetter");
        uniformSetters[typeof(T)](program, GetUniformLocation(name), value);
    }

    public void SetUniform(string name, Matrix2 value, bool transpose) =>
        Gl.ProgramUniformMatrix2(program, GetUniformLocation(name), transpose, value.ToArray());
    public void SetUniform(string name, Matrix3 value, bool transpose) =>
        Gl.ProgramUniformMatrix3(program, GetUniformLocation(name), transpose, value.ToArray());
    public void SetUniform(string name, Matrix value, bool transpose) =>
        Gl.ProgramUniformMatrix4(program, GetUniformLocation(name), transpose, value.ToArray());

    /// <summary>
    /// Binds as texture (sampler2D)
    /// </summary>
    public void SetUniform(string name, Texture value, int unit)
    {
        value.Bind(unit);
        SetUniform(name, unit);
    }

    /// <summary>
    /// Binds as image (image2D)
    /// </summary>
    public void SetUniform(string name, Texture value, BufferAccess access, int unit)
    {
        value.BindImage(access, unit);
        SetUniform(name, unit);
    }

    public static void RegisterUniformSetter<T>(UniformSetter<T> setter) =>
        uniformSetters.Add(typeof(T), (p, l, v) => setter(p, l, (T)v));

    /// <summary>
    /// Sets and updates TIME, CAM_SIZE and SCREEN_SIZE. Automatically called if attached to an entity.
    /// </summary>
    public void UpdateUniforms()
    {
        Time += Engine.FrameTime;
        SetUniform("TIME", Time);
        SetUniform("CAM_SIZE", Camera.CurrentResolution);
        SetUniform("SCREEN_SIZE", Engine.Size);
    }

    public override void Draw()
    {
        base.Draw();
        UpdateUniforms();
    }

    private void ReleaseUnmanagedResources() =>
        Gl.DeleteProgram(program);

    protected virtual void Dispose(bool disposing) =>
        ReleaseUnmanagedResources();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~Shader() => Dispose(false);
}
