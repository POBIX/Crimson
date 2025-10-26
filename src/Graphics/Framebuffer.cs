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
            TextureTarget.Texture2d, texture.GLId, 0
        );
    }

    /// <summary>
    /// Draws anything drawn inside of <paramref name="action"/>() to a texture.
    /// NOTE: This creates a new framebuffer each time it is called. Do NOT call it every frame, create a framebuffer instead.
    /// </summary>
    public static Texture Draw(Vector2 size, Action action)
    {
        Framebuffer prev = Active;
        Texture texture = new(size);
        using Framebuffer fbo = new();

        fbo.AttachTexture(texture, 0);
        Graphics.Clear();
        Gl.Viewport(0, 0, (int)size.x, (int)size.y);

        action();

        if (prev != null) prev.Bind();
        else BindDefault();

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

    // no finalizer since it sometimes causes a crash due to an OpenGL.Net bug.
    // ~Framebuffer() => ReleaseUnmanagedResources();
}
