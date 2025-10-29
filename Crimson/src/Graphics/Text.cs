using OpenGL;
using SharpFont;

namespace Crimson;

public class Font : IDisposable
{
    public struct Character(Texture texture, Vector2 size, Vector2 bearing, int advance)
    {
        public Texture texture = texture;
        public Vector2 size = size;
        public Vector2 bearing = bearing;
        public int advance = advance;
    }

    private Dictionary<char, Character> characters = new();

    // height is in 1/64 of a point. divide it by 64 (bitshift by 6) to get the real value.
    public int LineHeight => (lineHeight >> 6) + HeightMargin;
    private int lineHeight;
    public int HeightMargin { get; set; } = 4;

    /// <param name="path">The path to the font file/name of a registered font (see <seealso cref="Register"/>)</param>
    /// <param name="size">The font's size in points.</param>
    /// <param name="filter">controls antialiasing filter. Set to false on pixel art fonts.</param>
    public Font(string path, int size, bool filter = true)
    {
        using Library lib = new();
        using Face face = new(lib, GetPath(path));
        face.SetPixelSizes(0, (uint)size);
        lineHeight = face.Size.Metrics.Height.Value;

        Gl.PixelStorei(PixelStoreParameter.UnpackAlignment, 1);

        // load first 128 ASCII characters.
        for (uint c = 0; c < 16384; c++)
        {
            face.LoadChar(c, LoadFlags.Render, LoadTarget.Normal);
            GlyphSlot glyph = face.Glyph;
            using FTBitmap bmp = glyph.Bitmap;
            Texture tex = new(bmp.Width, bmp.Rows, bmp.Buffer, true, filter, InternalFormat.R8, PixelFormat.Red);
            tex.SetClamp(TextureWrapMode.ClampToEdge);

            characters.Add((char)c, new Character(
                tex, new(bmp.Width, bmp.Rows), new(glyph.BitmapLeft, glyph.BitmapTop), glyph.Advance.X.Value
            ));
        }

        Gl.PixelStorei(PixelStoreParameter.UnpackAlignment, 4);
    }

    public Character this[char c] => characters[c];

    /// <summary>
    /// Returns the size, in pixels, of a specified string when drawn using this font.
    /// </summary>
    /// <param name="text">The text to measure</param>
    public Vector2 MeasureText(string text)
    {
        if (text.Length == 0) return Vector2.Zero;
        float maxW = -1;
        float w = 0;
        float h = 0;

        foreach (char c in text)
        {
            if (c == '\n')
            {
                h += LineHeight;
                maxW = Mathf.Max(maxW, w);
                w = 0;
                continue;
            }

            w += MeasureChar(c);
        }
        h += this[text[0]].size.y * 2; // when we're done, add the height of the final line, without any room for a line break.

        maxW = Mathf.Max(maxW, w);
        return new(maxW, h);
    }

    public static int MeasureChar(Character c)
    {
        // units are 1/64 of a point. divide it by 64 (bitshift by 6) to get the value in pixels.
        return c.advance >> 6;
    }
    public int MeasureChar(char c) => MeasureChar(this[c]);

    private static Dictionary<string, string> fontPaths = new();
    public static void Register(string name, string path) => fontPaths.Add(name, path);
    public static string GetPath(string name) => fontPaths.GetValueOrDefault(name, name);

    private void ReleaseUnmanagedResources()
    {
        foreach (var (_, c) in characters)
            c.texture.Dispose();
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Font() => ReleaseUnmanagedResources();
}

public enum HAlignment
{
    Left,
    Center,
    Right
}
public enum VAlignment
{
    Top,
    Center,
    Bottom
}

public struct WrapSettings(IList<char> wrapOn, IList<char> breakOn, IList<char> ignore)
{
    /// <summary> Only wrap after these characters </summary>
    public IList<char> WrapOn { get; set; } = wrapOn;
    /// <summary> Force a line break on these characters </summary>
    public IList<char> BreakOn { get; set; } = breakOn;
    /// <summary> Ignore these characters </summary>
    public IList<char> Ignore { get; set; } = ignore;
}

public class Label : DrawableObject, IDisposable
{
    public override Material Material { get; set; } = new();
    public Scene Scene { get; private set; }

    public Font Font { get; set; }
    public string Text { get; set; } = "";

    public float Rotation { get; set; }
    public Color Color { get; set; } = Color.White;
    public Vector2 Scale { get; set; } = Vector2.One;
    public Vector2 Size { get; set; }
    public HAlignment HAlignment { get; set; } = HAlignment.Left;
    public VAlignment VAlignment { get; set; } = VAlignment.Top;

    public bool Wrap { get; set; } = true;
    public WrapSettings WrapSettings { get; set; } = new()
    {
        WrapOn = " ,.:;])<>\\|/`~!@#$%^&*-_+=?".ToCharArray(),
        BreakOn = ['\n'],
        Ignore = ['\r']
    };

    private static float[] vertices =
    {
        0, -1, 0, 0,
        0, 0, 0, 1,
        1, 0, 1, 1,
        0, -1, 0, 0,
        1, 0, 1, 1,
        1, -1, 1, 0
    };

    public override void Start()
    {
        Material ??= new();
        Material.InitShaderText(Resources.Read("shaders/text.vert"), Resources.Read("shaders/text.frag"));

        Material.FeedBuffer(vertices);
        Material.BindVAO();

        Material.VertexAttribPointer("VERTEX", IntPtr.Zero, 4, out uint uvLoc);
        Material.EnableVertexAttribArray(uvLoc);

        Material.EnableVertexAttribArray("TEX_COORDS", out uint tcLoc);
        Material.VertexAttribPointer(tcLoc, new(2 * sizeof(float)), 4);
    }

    private float CalcStartX(string line)
    {
        if (HAlignment == HAlignment.Left) return 0;
        float lineWidth = Font.MeasureText(line).x * Scale.x;
        return HAlignment switch
        {
            HAlignment.Center => (Size.x - lineWidth) / 2,
            HAlignment.Right => Size.x - lineWidth,
            _ => throw new Exception()
        };
    }

    private IEnumerable<string> WrapText(string text)
    {
        static IEnumerable<string> Split(string s, IList<char> split, IList<char> keep, IList<char> ignore)
        {
            string word = "";
            foreach (char c in s)
            {
                if (split.Contains(c))
                {
                    yield return word + c;
                    word = "";
                }
                else if (keep.Contains(c))
                {
                    yield return word;
                    word = "";
                    yield return c.ToString();
                }
                else if (!ignore.Contains(c))
                    word += c;
            }
            yield return word;
        }

        float spaceWidth = Font.MeasureChar(' ');

        float x = 0;
        string line = "";
        foreach (string word in Split(text, WrapSettings.WrapOn, WrapSettings.BreakOn, WrapSettings.Ignore))
        {
            if (word.Length == 0) continue;

            if (WrapSettings.BreakOn.Contains(word[0])) // if we should force break on this line
            {
                x = 0;
                continue;
            }

            // if there's whitespace at the end of the word and it causes the line to overflow (whitespace has a width),
            // we should ignore it as you won't be able to actually see anything wrong.
            int spaces;
            // count the number of whitespaces on the right
            for (spaces = 0;
                 spaces < word.Length && char.IsWhiteSpace(word[word.Length - 1 - spaces]);
                 spaces++)
            { }

            float width = Font.MeasureText(word).x - spaces * spaceWidth;

            if (x + width > Size.x && line != string.Empty)
            {
                yield return line;
                x = 0;
                line = "";
            }
            x += width + spaces * spaceWidth; // correct for the removed whitespace
            line += word;
        }
        yield return line;
    }

    public override void Draw()
    {
        Matrix gMat = Camera.GetTransform(Position, Rotation, Vector2.One);

        float textHeight = Font.MeasureText(Text).y * Scale.y;

        float y = VAlignment switch
        {
            VAlignment.Top => Font.HeightMargin,
            VAlignment.Center => (Size.y - textHeight) / 2,
            VAlignment.Bottom => Size.y - textHeight,
            _ => throw new ArgumentOutOfRangeException(nameof(VAlignment))
        };

        foreach (string line in Wrap ? WrapText(Text) : Text.Split('\n'))
        {
            float x = CalcStartX(line.Trim());

            foreach (char c in line)
            {
                Font.Character ch = Font[c];
                Vector2 s = ch.size * Scale;
                Vector2 p = new(
                    x + ch.bearing.x * Scale.x,
                    y + (ch.size.y - ch.bearing.y) * Scale.y
                );

                x += Font.MeasureChar(ch) * Scale.x;

                Matrix t = gMat *
                           Matrix.Translation(new(p, 0)) *
                           Matrix.Scaling(new(s, 1));

                Material.BindVAO();
                Material.SetUniform("TRANSFORM", t, false);
                Material.SetUniform("TEXTURE", ch.texture, 0);
                Material.SetUniform("COLOR", Color);

                Graphics.DrawArrays(PrimitiveType.Triangles, 0, 6);
            }

            y += Font.LineHeight * Scale.y;
        }
    }

    public override void Update(float delta) { }
    public override void Frame(float delta) { }
    public override void SetScene(Scene value) => Scene = value;
    public override void OnDestroy() { }

    public Rect GetBoundingBox()
    {
        Vector2 s = Size * Scale;
        return new(Position + s / 2, s);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Material?.Dispose();
            Font?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
