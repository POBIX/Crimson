using System.Drawing;
using System.Drawing.Imaging;
using OpenGL;
using DrawingFormat = System.Drawing.Imaging.PixelFormat;
using PixelFormat = OpenGL.PixelFormat;

namespace Crimson;

public enum BufferAccess
{
    Read = BufferAccessARB.ReadOnly,
    Write = BufferAccessARB.WriteOnly,
    ReadWrite = BufferAccessARB.ReadWrite
}

public class Texture : IDisposable
{
    public uint GLId { get; }
    public Vector2 Size { get; }
    public int Width => (int)Size.x;
    public int Height => (int)Size.y;

    /// <summary>
    /// Loads raw data into the texture.
    /// </summary>
    /// <param name="data">The data pointer.</param>
    public void SetData(IntPtr data, InternalFormat internalFormat = InternalFormat.Rgba32f,
                        PixelFormat pixelFormat = PixelFormat.Bgra, PixelType pixelType = PixelType.UnsignedByte)
    {
        BindObject();
        Gl.TexImage2D(
            TextureTarget.Texture2d, 0, internalFormat, Width, Height,
            0, pixelFormat, pixelType, data
        );
    }

    /// <summary>
    /// Sets the texture filter. false - Nearest, true - Linear.
    /// </summary>
    public void SetFilter(bool filter)
    {
        BindObject();
        Gl.TexParameteri(
            TextureTarget.Texture2d, TextureParameterName.TextureMinFilter,
            filter ? (int)TextureMinFilter.Linear : (int)TextureMinFilter.Nearest
        );
        Gl.TexParameteri(
            TextureTarget.Texture2d, TextureParameterName.TextureMagFilter,
            filter ? (int)TextureMagFilter.Linear : (int)TextureMagFilter.Nearest
        );
    }

    public void SetClamp(TextureWrapMode mode)
    {
        Gl.TextureParameteri(GLId, TextureParameterName.TextureWrapS, (int)mode);
        Gl.TextureParameteri(GLId, TextureParameterName.TextureWrapT, (int)mode);
    }

    /// <summary>
    /// Loads a texture from an image.
    /// </summary>
    /// <param name="filePath">The path to the image.</param>
    public Texture(string filePath, bool filter = false)
    {
        GLId = Gl.CreateTexture(TextureTarget.Texture2d);

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
    /// Creates a texture from a raw data pointer and with a specified width and height.
    /// </summary>
    public Texture(int width, int height, IntPtr data, bool generateMipmaps = true, bool filter = false,
                   InternalFormat internalFormat = InternalFormat.Rgba32f,
                   PixelFormat pixelFormat = PixelFormat.Bgra, PixelType pixelType = PixelType.UnsignedByte)
    {
        GLId = Gl.CreateTexture(TextureTarget.Texture2d);
        Size = new(width, height);

        SetData(data, internalFormat, pixelFormat, pixelType);
        SetFilter(filter);

        if (generateMipmaps) Gl.GenerateMipmap(TextureTarget.Texture2d);
    }

    /// <summary>
    /// Creates a texture from a raw data pointer and with a specified size.
    /// </summary>
    public Texture(Vector2 size, IntPtr data, bool generateMipmaps = true, bool filter = false,
                   InternalFormat internalFormat = InternalFormat.Rgba32f,
                   PixelFormat pixelFormat = PixelFormat.Bgra, PixelType pixelType = PixelType.UnsignedByte)
        : this((int)size.x, (int)size.y, data, generateMipmaps, filter, internalFormat, pixelFormat, pixelType) { }

    /// <summary>
    /// Creates an empty texture with a specified size.
    /// </summary>
    public Texture(Vector2 size, bool generateMipmaps = true, bool filter = false)
        : this((int)size.x, (int)size.y, IntPtr.Zero, generateMipmaps, filter) { }

    /// <summary>
    /// Creates an empty texture with a specified width and height.
    /// </summary>
    public Texture(int width, int height, bool generateMipmaps = true, bool filter = false) :
        this(width, height, IntPtr.Zero, generateMipmaps, filter) { }

    public void BindImage(BufferAccess access, int unit) =>
        Gl.BindImageTexture((uint)unit, GLId, 0, false, 0, (BufferAccessARB)access, InternalFormat.Rgba32f);

    /// <summary>
    /// Binds the texture object without activating it.
    /// </summary>
    public void BindObject() => Gl.BindTexture(TextureTarget.Texture2d, GLId);

    /// <summary>
    /// Binds the texture object and activates it on <paramref name="unit"/>
    /// </summary>
    public void Bind(int unit)
    {
        Gl.ActiveTexture(TextureUnit.Texture0 + unit);
        BindObject();
    }

    public void Clear() => Gl.ClearTexImage(GLId, 0, PixelFormat.Bgra, PixelType.Float, IntPtr.Zero);

    public void Resize(Vector2 size, InternalFormat internalFormat = InternalFormat.Rgba32f,
                       PixelFormat pixelFormat = PixelFormat.Bgra, PixelType pixelType = PixelType.UnsignedByte)
    {
        BindObject();
        Gl.TexImage2D(
            TextureTarget.Texture2d, 0, internalFormat, (int)size.x, (int)size.y,
            0, pixelFormat, pixelType, IntPtr.Zero
        );
    }

    public void Resize(float w, float h) => Resize(new(w, h));

    public byte[] GetData(PixelFormat pixelFormat = PixelFormat.Bgra, PixelType pixelType = PixelType.UnsignedByte)
    {
        BindObject();

        var output = new byte[Width * Height * 4 * sizeof(float)]; // size * color channels * size of element
        Gl.GetTexImage(TextureTarget.Texture2d, 0, pixelFormat, pixelType, output);
        return output;
    }

    private void ReleaseUnmanagedResources() => Gl.DeleteTextures(1, [GLId]);

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Texture() => ReleaseUnmanagedResources();
}
