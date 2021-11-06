using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Crimson.Scenes;
using OpenGL;
using GLType = OpenGL.ShaderType;

namespace Crimson
{
    public class ComputeShader : ShaderBase, IDisposable
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

        public void Dispatch(int groupsX, int groupsY, int groupsZ)
        {
            Gl.UseProgram(program);
            Gl.DispatchCompute((uint)groupsX, (uint)groupsY, (uint)groupsZ);
            Gl.MemoryBarrier(MemoryBarrierMask.ShaderImageAccessBarrierBit);
        }

        public void SetUniformImage(string name, Texture tex, BufferAccess access, int unit)
        {
            tex.BindImage(access, unit);
            SetUniform(name, unit);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
                Material?.Dispose();
        }

        ~ComputeShader() => Dispose(false);
    }

    /// <summary>
    /// A shader storage buffer object (SSBO)
    /// </summary>
    public unsafe class ShaderBuffer<T> : IDisposable where T : unmanaged
    {
        private int length;

        private uint id;

        public ShaderBuffer(ShaderBase shader, string name, int index)
        {
            id = Gl.GenBuffer();

            Gl.BindBufferBase(BufferTarget.ShaderStorageBuffer, (uint)index, id);
        }

        public void SetData(T[] arr)
        {
            length = arr.Length;
            Gl.BufferData(BufferTarget.ShaderStorageBuffer, (uint)(length * sizeof(T)), arr, BufferUsage.DynamicCopy);
        }

        public void GetData(out T[] output)
        {
            output = new T[length];

            Gl.BindBuffer(BufferTarget.ShaderStorageBuffer, id);
            IntPtr p = Gl.MapBuffer(BufferTarget.ShaderStorageBuffer, OpenGL.BufferAccess.WriteOnly);
            T* data = (T*)p;
            for (int i = 0; i < length; i++)
                output[i] = data![i];

            Gl.UnmapBuffer(BufferTarget.ShaderStorageBuffer);
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
}
