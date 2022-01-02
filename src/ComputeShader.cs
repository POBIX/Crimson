using OpenGL;
using GLType = OpenGL.ShaderType;

namespace Crimson;

[Flags]
public enum MemoryBarriers : uint
{
    All = MemoryBarrierMask.AllBarrierBits,
    AtomicCounter = MemoryBarrierMask.AtomicCounterBarrierBit,
    BufferUpdate = MemoryBarrierMask.BufferUpdateBarrierBit,
    ClientMappedBuffer = MemoryBarrierMask.ClientMappedBufferBarrierBit,
    CommandBarrier = MemoryBarrierMask.CommandBarrierBit,
    ElementArray = MemoryBarrierMask.ElementArrayBarrierBit,
    Framebuffer = MemoryBarrierMask.FramebufferBarrierBit,
    PixelBuffer = MemoryBarrierMask.PixelBufferBarrierBit,
    QueryBuffer = MemoryBarrierMask.QueryBufferBarrierBit,
    ShaderGlobalAccess = MemoryBarrierMask.ShaderGlobalAccessBarrierBitNv,
    ShaderImageAccess = MemoryBarrierMask.ShaderImageAccessBarrierBit,
    ShaderStorage = MemoryBarrierMask.ShaderStorageBarrierBit,
    TextureFetch = MemoryBarrierMask.TextureFetchBarrierBit,
    TextureUpdate = MemoryBarrierMask.TextureUpdateBarrierBit,
    TransformFeedback = MemoryBarrierMask.TransformFeedbackBarrierBit,
    Uniform = MemoryBarrierMask.UniformBarrierBit,
    VertexAttribArray = MemoryBarrierMask.VertexAttribArrayBarrierBit,
    None = 0
}

public class ComputeShader : Shader, IDisposable
{
    public ComputeShader() => program = Gl.CreateProgram();

    public void Attach(string path) => AttachText(File.ReadAllText(path));

    public void AttachText(string text)
    {
        uint shader = Gl.CreateShader(GLType.ComputeShader);
        Gl.ShaderSource(shader, new[] { text });
        Gl.CompileShader(shader);
        PrintShaderLog(shader);

        Gl.AttachShader(program, shader);
        Gl.LinkProgram(program);
        PrintProgramLog(program);

        Gl.DeleteShader(shader);
    }

    public void Dispatch(int groupsX, int groupsY, int groupsZ, MemoryBarriers barriers)
    {
        Gl.UseProgram(program);
        Gl.DispatchCompute((uint)groupsX, (uint)groupsY, (uint)groupsZ);
        if (barriers != MemoryBarriers.None) Gl.MemoryBarrier((MemoryBarrierMask)barriers);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
            Material?.Dispose();
    }

    ~ComputeShader() => Dispose(false);
}
