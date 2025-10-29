using System.Runtime.InteropServices;

namespace OpenGL;

public static partial class Gl
{
    public static void ShaderSource(uint shader, string source) => ShaderSource(shader, 1, [source], null);

    public static unsafe void BufferData<T>(BufferTargetARB target, T[] data, BufferUsageARB usage)
        where T : unmanaged
    {
        nint size = data.Length * sizeof(T);
        fixed (void* p = &data[0])
            BufferData(target, size, p, usage);
    }

    public static unsafe void NamedBufferData<T>(uint buffer, T[] data, VertexBufferObjectUsage usage)
        where T : unmanaged
    {
        nint size = data.Length * sizeof(T);
        fixed (void* p = &data[0])
            NamedBufferData(buffer, size, p, usage);
    }

    public static uint CreateVertexArray()
    {
        uint[] arr = new uint[1];
        CreateVertexArrays(1, arr);
        return arr[0];
    }

    public static uint CreateBuffer()
    {
        uint[] arr = new uint[1];
        CreateBuffers(1, arr);
        return arr[0];
    }

    public static uint CreateTexture(TextureTarget target)
    {
        uint[] arr = new uint[1];
        CreateTextures(target, 1, arr);
        return arr[0];
    }

    public static uint CreateFramebuffer()
    {
        uint[] arr = new uint[1];
        CreateFramebuffers(1, arr);
        return arr[0];
    }

    public static unsafe void GetTexImage<T>(TextureTarget target, int level, PixelFormat format, PixelType type,
                                             T[] pixels) where T : unmanaged
    {
        fixed (void* rawPixels = pixels)
            GetTexImage(target, level, format, type, rawPixels);
    }
}
