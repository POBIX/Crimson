using System;
using System.Collections.Generic;
using System.Text;
using OpenGL;

namespace Crimson
{
    public abstract class ShaderBase : Component, IDisposable
    {
        private Dictionary<string, int> uniformLocs = new();

        protected internal uint program;

        protected ShaderBase() => program = Gl.CreateProgram();

        protected static void Print(string s)
        {
            if (s != "") Console.WriteLine(s);
        }

        protected const int LogLength = 2048;
        protected static string GetShaderLog(uint shader)
        {
            var sb = new StringBuilder(LogLength);
            Gl.GetShaderInfoLog(shader, LogLength, out _, sb);
            return sb.ToString();
        }

        protected static string GetProgramLog(uint program)
        {
            var sb = new StringBuilder(LogLength);
            Gl.GetProgramInfoLog(program, LogLength, out _, sb);
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

        public void SetUniform(string name, int value, out int location)
        {
            location = GetUniformLocation(name);
            Gl.ProgramUniform1(program, location, value);
        }

        public void SetUniform(string name, Vector2 value, out int location)
        {
            location = GetUniformLocation(name);
            Gl.ProgramUniform2(program, location, value.x, value.y);
        }

        public void SetUniform(string name, float value, out int location)
        {
            location = GetUniformLocation(name);
            Gl.ProgramUniform1(program, location, value);
        }

        public void SetUniform(string name, double value, out int location)
        {
            location = GetUniformLocation(name);
            Gl.ProgramUniform1(program, location, value);
        }

        public void SetUniform(string name, int value) => SetUniform(name, value, out _);
        public void SetUniform(string name, bool value) => SetUniform(name, value, out _);
        public void SetUniform(string name, Vector2 value) => SetUniform(name, value, out _);
        public void SetUniform(string name, Color value) => SetUniform(name, value, out _);
        public void SetUniform(string name, float value) => SetUniform(name, value, out _);
        public void SetUniform(string name, double value) => SetUniform(name, value, out _);
        
        public void SetUniform(string name, Matrix matrix, bool transpose, out int location)
        {
            location = GetUniformLocation(name);
            Gl.ProgramUniformMatrix4(program, location, transpose, matrix.ToArray());
        }

        public void SetUniform(string name, Matrix matrix, bool transpose) =>
            SetUniform(name, matrix, transpose, out _);

        public void SetUniform(string name, Color value, out int location)
        {
            location = GetUniformLocation(name);
            Gl.ProgramUniform4(program, location, value.r, value.g, value.b, value.a);
        }

        public void SetUniform(string name, bool value, out int location)
        {
            location = GetUniformLocation(name);
            Gl.ProgramUniform1(program, location, value ? 1 : 0);
        }

        private void ReleaseUnmanagedResources()
        {
            Gl.DeleteProgram(program);
        }

        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ShaderBase() => Dispose(false);
    }
}
