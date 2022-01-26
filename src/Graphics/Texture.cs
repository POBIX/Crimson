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

    private string file;

    /// <summary>
    /// Loads raw data into the texture.
    /// </summary>
    /// <param name="data">The data pointer.</param>
    public void SetData(IntPtr data)
    {
        BindObject();
        Gl.TexImage2D(
            TextureTarget.Texture2d, 0, InternalFormat.Rgba32f, Width, Height,
            0, PixelFormat.Bgra, PixelType.UnsignedByte, data
        );
    }

    /// <summary>
    /// Sets the texture filter. false - Nearest, true - Linear.
    /// </summary>
    /// <param name="filter"></param>
    public void SetFilter(bool filter)
    {
        BindObject();
        Gl.TexParameter(
            TextureTarget.Texture2d, TextureParameterName.TextureMinFilter,
            filter ? (int)TextureMinFilter.Linear : (int)TextureMinFilter.Nearest
        );
        Gl.TexParameter(
            TextureTarget.Texture2d, TextureParameterName.TextureMagFilter,
            filter ? (int)TextureMagFilter.Linear : (int)TextureMagFilter.Nearest
        );
    }

    /// <summary>
    /// Loads a texture from an image.
    /// </summary>
    /// <param name="filePath">The path to the image.</param>
    public Texture(string filePath, bool filter = false)
    {
        file = filePath;
        id = Gl.GenTexture();

        using Bitmap bmp = new(filePath);

        BitmapData data = bmp.LockBits(
            new(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, DrawingFormat.Format32bppArgb
        );

        Size = new Vector2(bmp.Width, bmp.Height);

        SetFilter(filter);
        SetData(data.Scan0);

        Gl.GenerateMipmap(TextureTarget.Texture2d);

        bmp.UnlockBits(data);
    }


    /// <summary>
    /// Creates an empty texture with a specified width and height.
    /// </summary>
    public Texture(int width, int height, bool generateMipmaps = true, bool filter = false)
    {
        id = Gl.GenTexture();
        Size = new(width, height);

        SetData(IntPtr.Zero);
        SetFilter(filter);

        if (generateMipmaps) Gl.GenerateMipmap(TextureTarget.Texture2d);
    }

    /// <summary>
    /// Creates an empty texture with a specified size.
    /// </summary>
    public Texture(Vector2 size, bool generateMipmaps = true, bool filter = false)
        : this((int)size.x, (int)size.y, generateMipmaps, filter) { }

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

    private void ReleaseUnmanagedResources()
    {
        uint[] arr = { id }; // crash without explicitly creating array.
        Gl.DeleteTextures(arr);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    // no finalizer since it sometimes causes a crash due to an OpenGL.Net bug.
    // ~Texture() => ReleaseUnmanagedResources();
}
