using System;
using OpenGL;
using GLTarget = OpenGL.FramebufferTarget;

namespace Crimson
{
    public class Framebuffer : IDisposable
    {
        private uint id;

        public Framebuffer() => id = Gl.GenFramebuffer();

        public void Bind() => Gl.BindFramebuffer(FramebufferTarget.Framebuffer, id);
        public void BindReadOnly() => Gl.BindFramebuffer(FramebufferTarget.ReadFramebuffer, id);
        public void BindWriteOnly() => Gl.BindFramebuffer(FramebufferTarget.DrawFramebuffer, id);
        public static void BindDefault() => Gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

        public void AttachTexture(Texture texture, int attachment)
        {
            Bind();
            Gl.FramebufferTexture2D(
                FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0 + attachment,
                TextureTarget.Texture2d, texture.id, 0
            );
        }

        private void ReleaseUnmanagedResources() =>
            Gl.DeleteFramebuffers(id);

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~Framebuffer() => ReleaseUnmanagedResources();
    }
}
