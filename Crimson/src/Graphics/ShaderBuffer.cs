using OpenGL;

namespace Crimson;

/// <summary>
/// A shader storage buffer object (SSBO)
/// </summary>
public unsafe class ShaderBuffer<T> : IDisposable where T : unmanaged
{
    private int length;
    private uint index;

    private uint id;

    public ShaderBuffer(uint index)
    {
        id = Gl.CreateBuffer();
        this.index = index;
    }

    public ShaderBuffer(int index) : this((uint)index) { }

    public ShaderBuffer(Shader shader, string name)
        : this(Gl.GetProgramResourceIndex(shader.program, ProgramInterface.ShaderStorageBlock, name)) { }

    public void SetData(T[] arr)
    {
        Bind();
        length = arr.Length;
        Gl.BufferData(BufferTargetARB.ShaderStorageBuffer, arr, BufferUsageARB.DynamicCopy);
    }

    public void GetData(ref T[] output)
    {
        if (output.Length != length) output = new T[length];
        Gl.BindBuffer(BufferTargetARB.ShaderStorageBuffer, id);
        IntPtr p = new(Gl.MapBuffer(BufferTargetARB.ShaderStorageBuffer, BufferAccessARB.WriteOnly));
        T* data = (T*)p;
        for (int i = 0; i < length; i++)
            output[i] = data![i];

        Gl.UnmapBuffer(BufferTargetARB.ShaderStorageBuffer);
    }

    public void Bind()
    {
        Gl.BindBuffer(BufferTargetARB.ShaderStorageBuffer, id);
        Gl.BindBufferBase(BufferTargetARB.ShaderStorageBuffer, index, id);
    }

    private void ReleaseUnmanagedResources() =>
        Gl.DeleteBuffers(1, [id]);

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    // no finalizer since it sometimes causes a crash due to an OpenGL.Net bug.
    // ~ShaderBuffer() => ReleaseUnmanagedResources();
}
