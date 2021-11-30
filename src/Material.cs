using OpenGL;
using GLType = OpenGL.ShaderType;

namespace Crimson;

public enum ShaderType
{
    Fragment = GLType.FragmentShader,
    Vertex = GLType.VertexShader,
    Geometry = GLType.GeometryShader,
    TessControl = GLType.TessControlShader,
    TessEvaluation = GLType.TessEvaluationShader,
}

public class Material : ShaderBase, IDisposable
{
    internal uint vao;
    internal uint vbo;

    private Dictionary<ShaderType, uint> shaders = new();

    /// <summary> The material currently being used. </summary>
    public static Material Current { get; private set; }

    public Material()
    {
        program = Gl.CreateProgram();
        vao = Gl.CreateVertexArray();
        vbo = Gl.CreateBuffer();
    }

    /// <summary> Removes every shader from the material. </summary>
    public void Clear()
    {
        foreach (uint i in shaders.Values)
        {
            Gl.DetachShader(program, i);
            // Gl.DeleteShader(i);
        }
        shaders.Clear();

        Link();
    }

    /// <summary> Links the material. </summary>
    public void Link()
    {
        Gl.LinkProgram(program);
        PrintProgramLog(program);
        foreach (uint s in shaders.Values)
            Gl.DeleteShader(s);
    }

    private void Attach(uint shader)
    {
        Gl.AttachShader(program, shader);
        PrintShaderLog(shader);
    }

    /// <summary>
    /// Attaches the shader to the material.
    /// </summary>
    /// <param name="type">The shader's type.</param>
    /// <param name="text">The shader's source code (see <see cref="AttachShader"/> for loading a file)</param>
    public void AttachShaderText(ShaderType type, string text)
    {
        uint shader = Gl.CreateShader((GLType)type);
        Gl.ShaderSource(shader, new[] {text});
        Gl.CompileShader(shader);

        Attach(shader);

        shaders.Add(type, shader);
    }

    /// <summary>
    /// Attaches the shader to the material.
    /// </summary>
    /// <param name="type">The shader's type.</param>
    /// <param name="path">The path to the shader.</param>
    public void AttachShader(ShaderType type, string path) =>
        AttachShaderText(type, File.ReadAllText(path));

    /// <summary>
    /// Activates the shader.
    /// </summary>
    public void Use()
    {
        if (Current != this)
        {
            Gl.UseProgram(program);
            Current = this;
        }
    }

    public void EnableVertexAttribArray(string name, out uint location)
    {
        location = (uint)Gl.GetAttribLocation(program, name);
        Gl.EnableVertexAttribArray(location);
    }

    public void EnableVertexAttribArray(string name) => EnableVertexAttribArray(name, out _);

    public static void EnableVertexAttribArray(uint location) => Gl.EnableVertexAttribArray(location);

    /// <summary>
    /// Is there a shader of type <paramref name="type"/> attached to the material?
    /// </summary>
    public bool IsShaderSet(ShaderType type) => shaders.ContainsKey(type);

    public void BindVBO() => Gl.BindBuffer(BufferTarget.ArrayBuffer, vbo);

    /// <summary>
    /// Feeds the vertices into the VBO.
    /// </summary>
    internal void FeedBuffer(float[] vertices)
    {
        BindVBO();
        Gl.BufferData(
            BufferTarget.ArrayBuffer,
            (uint)vertices.Length * sizeof(float),
            vertices,
            BufferUsage.StaticDraw
        );
    }

    public void BindVAO() => Gl.BindVertexArray(vao);

    public uint GetAttribLocation(string name) => (uint)Gl.GetAttribLocation(program, name);

    public void VertexAttribPointer(string name, IntPtr ptr, int stride, out uint location)
    {
        location = GetAttribLocation(name);
        VertexAttribPointer(location, ptr, stride);
    }

    public void VertexAttribPointer(string name, IntPtr ptr, int stride) => VertexAttribPointer(name, ptr, stride, out _);

    public static void VertexAttribPointer(uint location, IntPtr ptr, int stride) =>
        Gl.VertexAttribPointer(location, 2, VertexAttribType.Float, false, stride * sizeof(float), ptr);

    /// <summary>
    /// Attaches <paramref name="fragPath"/> and <paramref name="vertPath"/> only if there is no shader already attached.
    /// </summary>
    /// <param name="fragPath">The path to the fragment shader.</param>
    /// <param name="vertPath">The path to the vertex shader.</param>
    public void InitShader(string vertPath, string fragPath) =>
        InitShaderText(File.ReadAllText(vertPath), File.ReadAllText(fragPath));

    /// <summary>
    /// Attaches <paramref name="frag"/> and <paramref name="vert"/> only if there is no shader already attached.
    /// </summary>
    /// <param name="frag">The fragment shader's source</param>
    /// <param name="vert">The vertex shader's source</param>
    public void InitShaderText(string vert, string frag)
    {
        bool changed = false;
        if (!IsShaderSet(ShaderType.Vertex))
        {
            AttachShaderText(ShaderType.Vertex, vert);
            changed = true;
        }
        if (!IsShaderSet(ShaderType.Fragment))
        {
            AttachShaderText(ShaderType.Fragment, frag);
            changed = true;
        }
        if (changed) Link();
    }

    private void ReleaseUnmanagedResources()
    {
        foreach (uint shader in shaders.Values)
            Gl.DeleteShader(shader);
        Gl.DeleteVertexArrays(vao);
        Gl.DeleteBuffers(vbo);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        ReleaseUnmanagedResources();
    }

    ~Material() => ReleaseUnmanagedResources();
}
