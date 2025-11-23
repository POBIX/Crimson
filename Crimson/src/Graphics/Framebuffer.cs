using OpenGL;
using GLTarget = OpenGL.FramebufferTarget;

namespace Crimson;

public class Framebuffer : IDisposable
{
    private uint id;

    public static Framebuffer Active { get; private set; }

    public Framebuffer() => id = Gl.CreateFramebuffer();

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
        Framebuffer prevFBO = Active;

        int[] prevViewport = new int[4];
        Gl.GetIntegerv(GetPName.Viewport, prevViewport);
        Camera.SetOrtho((int)size.x, (int)size.y);

        Texture texture = new(size);
        using Framebuffer fbo = new();

        fbo.AttachTexture(texture, 0);

        FramebufferStatus status = Gl.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
        if (status != FramebufferStatus.FramebufferComplete)
        {
            Console.WriteLine($"Framebuffer incomplete: {status}");
            return null;
        }

        Graphics.Clear();
        Gl.Viewport(0, 0, (int)size.x, (int)size.y);

        action();

        if (prevFBO != null) prevFBO.Bind();
        else BindDefault();

        Camera.DefaultOrtho();
        Gl.Viewport(prevViewport[0], prevViewport[1], prevViewport[2], prevViewport[3]);
        return texture;
    }

    public static Texture Draw(int width, int height, Action action) => Draw(new(width, height), action);

    private void ReleaseUnmanagedResources() =>
        Gl.DeleteFramebuffers(1, [id]);

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    // no finalizer since it sometimes causes a crash due to an OpenGL.Net bug.
    // ~Framebuffer() => ReleaseUnmanagedResources();
}
