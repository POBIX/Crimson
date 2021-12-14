using System.Drawing;
using System.Drawing.Imaging;
using OpenGL;
using DrawingFormat = System.Drawing.Imaging.PixelFormat;
using PixelFormat = OpenGL.PixelFormat;
using GLAccess = OpenGL.BufferAccess;

namespace Crimson;

public enum BufferAccess
{
    Read = GLAccess.ReadOnly,
    Write = GLAccess.WriteOnly,
    ReadWrite = GLAccess.ReadWrite
}

public class Texture : IDisposable
{
    internal uint id;
    public Vector2 Size { get; }
    public int Width => (int)Size.x;
    public int Height => (int)Size.y;

    public string FilePath { get; }

    /// <summary>
    /// Loads a texture from an image.
    /// </summary>
    /// <param name="filePath">The path to the image.</param>
    public Texture(string filePath)
    {
        FilePath = filePath;
        using Bitmap bmp = new(filePath);
        id = Gl.GenTexture();

        BitmapData data = bmp.LockBits(
            new(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, DrawingFormat.Format32bppArgb
        );

        Gl.BindTexture(TextureTarget.Texture2d, id);
        Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        Gl.TexImage2D(
            TextureTarget.Texture2d, 0, InternalFormat.Rgba32f, bmp.Width, bmp.Height,
            0, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0
        );

        Gl.GenerateMipmap(TextureTarget.Texture2d);

        bmp.UnlockBits(data);

        Size = new Vector2(bmp.Width, bmp.Height);
    }

    /// <summary>
    /// Creates an empty texture with a specified width and height.
    /// </summary>
    public Texture(int width, int height, bool generateMipmaps = true)
    {
        id = Gl.GenTexture();
        Size = new(width, height);

        Gl.BindTexture(TextureTarget.Texture2d, id);
        Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        Gl.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        Gl.TexImage2D(
            TextureTarget.Texture2d, 0, InternalFormat.Rgba32f, width, height,
            0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero
        );

        if (generateMipmaps) Gl.GenerateMipmap(TextureTarget.Texture2d);
    }

    /// <summary>
    /// Creates an empty texture with a specified size.
    /// </summary>
    public Texture(Vector2 size, bool generateMipmaps = true) : this((int)size.x, (int)size.y, generateMipmaps) { }

    public void BindImage(BufferAccess access, int unit) =>
        Gl.BindImageTexture((uint)unit, id, 0, false, 0, (GLAccess)access, InternalFormat.Rgba32f);

    /// <summary>
    /// Binds the texture object without activating it.
    /// </summary>
    public void BindObject() => Gl.BindTexture(TextureTarget.Texture2d, id);

    /// <summary>
    /// Binds the texture object and activates it on <paramref name="unit"/>
    /// </summary>
    public void Bind(int unit)
    {
        Gl.ActiveTexture(TextureUnit.Texture0 + unit);
        BindObject();
    }

    public void Clear() => Gl.ClearTexImage(id, 0, PixelFormat.Bgra, PixelType.Float, IntPtr.Zero);

    public void Resize(Vector2 size)
    {
        BindObject();
        Gl.TexImage2D(
            TextureTarget.Texture2d, 0, InternalFormat.Rgba32f, (int)size.x, (int)size.y,
            0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero
        );
    }

    public void Resize(float w, float h) => Resize(new(w, h));

    public byte[] GetData()
    {
        BindObject();

        var output = new byte[Width * Height * 4 * sizeof(float)]; // size * color channels * size of element
        Gl.GetTexImage(TextureTarget.Texture2d, 0, PixelFormat.Bgra, PixelType.Float, output);
        return output;
    }

    private void ReleaseUnmanagedResources() =>
        Gl.DeleteTextures(id);

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Texture() => ReleaseUnmanagedResources();
}
