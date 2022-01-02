using OpenGL;
using GLTarget = OpenGL.FramebufferTarget;

namespace Crimson;

public class Framebuffer : IDisposable
{
    private uint id;

    public static Framebuffer Active { get; private set; }

    public Framebuffer() => id = Gl.GenFramebuffer();

    public void Bind()
    {
        Gl.BindFramebuffer(FramebufferTarget.Framebuffer, id);
        Active = this;
    }

    public static void BindDefault()
    {
        Gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        Active = null;
    }

    public void AttachTexture(Texture texture, int attachment)
    {
        Bind();
        Gl.FramebufferTexture2D(
            FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0 + attachment,
            TextureTarget.Texture2d, texture.id, 0
        );
    }

    public static Texture Draw(Vector2 size, Action action)
    {
        Framebuffer prev = Active;
        Texture texture = new(size);
        using Framebuffer fbo = new();
        fbo.AttachTexture(texture, 0);
        action();
        prev.Bind();
        return texture;
    }

    public static Texture Draw(int width, int height, Action action) => Draw(new(width, height), action);

    private void ReleaseUnmanagedResources() =>
        Gl.DeleteFramebuffers(id);

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Framebuffer() => ReleaseUnmanagedResources();
}
