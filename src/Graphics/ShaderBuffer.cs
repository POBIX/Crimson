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
        id = Gl.GenBuffer();
        this.index = index;
    }

    public ShaderBuffer(int index) : this((uint)index) { }

    public ShaderBuffer(Shader shader, string name)
        : this(Gl.GetProgramResourceIndex(shader.program, ProgramInterface.ShaderStorageBlock, name)) { }

    public void SetData(T[] arr)
    {
        Bind();
        length = arr.Length;
        Gl.BufferData(BufferTarget.ShaderStorageBuffer, (uint)(length * sizeof(T)), arr, BufferUsage.DynamicCopy);
    }

    public void GetData(ref T[] output)
    {
        if (output.Length != length) output = new T[length];
        Gl.BindBuffer(BufferTarget.ShaderStorageBuffer, id);
        IntPtr p = Gl.MapBuffer(BufferTarget.ShaderStorageBuffer, OpenGL.BufferAccess.WriteOnly);
        T* data = (T*)p;
        for (int i = 0; i < length; i++)
            output[i] = data![i];

        Gl.UnmapBuffer(BufferTarget.ShaderStorageBuffer);
    }

    public void Bind()
    {
        Gl.BindBuffer(BufferTarget.ShaderStorageBuffer, id);
        Gl.BindBufferBase(BufferTarget.ShaderStorageBuffer, index, id);
    }

    private void ReleaseUnmanagedResources() =>
        Gl.DeleteBuffers(id);

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~ShaderBuffer() => ReleaseUnmanagedResources();
}
