using System.Reflection;
using SColor = System.Drawing.Color;

// you can't override GetHashCode() in here, since there are no readonly fields.
#pragma warning disable 659,660,661

namespace Crimson;

public struct Color
{
    public float r;
    public float g;
    public float b;
    public float a;

    private Color(float r, float g, float b, float a)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    /// <summary>
    /// Creates a color with floating point values (between 0 and 1).
    /// </summary>
    /// <param name="r">Red</param>
    /// <param name="g">Green</param>
    /// <param name="b">Blue</param>
    /// <param name="a">Alpha</param>
    public static Color Float(float r, float g, float b, float a = 1) =>
        new(r, g, b, a);

    /// <summary>
    /// Creates a color with byte values (between 0 and 255).
    /// </summary>
    /// <param name="r">Red</param>
    /// <param name="g">Green</param>
    /// <param name="b">Blue</param>
    /// <param name="a">Alpha</param>
    public static Color Byte(int r, int g, int b, int a = 255) =>
        new(r / 255f, g / 255f, b / 255f, a / 255f);

    /// <summary>
    /// Creates a color from a <code>hex code</code>.
    /// Example usage: <code>Color.Code(0xFFFFFF);</code> produces white.
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static Color Code(int code)
    {
        int r = (code >> 16) & 255;
        int g = (code >> 8) & 255;
        int b = code & 255;
        return Byte(r, g, b);
    }

    public SColor ToSystem() => SColor.FromArgb((int)(a * 255), (int)(r * 255), (int)(g * 255), (int)(b * 255));

    public static Color White => Float(1, 1, 1);
    public static Color Black => Float(0, 0, 0);
    public static Color Transparent => Float(0, 0, 0, 0);
    public static Color None => Float(0, 0, 0, 0);
    public static Color Blue => Float(0, 0, 1);
    public static Color Green => Float(0, 1, 0);
    public static Color Red => Float(1, 0, 0);
    public static Color Yellow => Float(1, 1, 0);

    public override bool Equals(object obj) => obj is Color c && Equals(c);

    public bool Equals(Color other) => r.Equals(other.r) && g.Equals(other.g) && b.Equals(other.b) && a.Equals(other.a);

    public override string ToString() => $"({r}, {g}, {b}, {a})";

    public static bool operator ==(Color a, Color b) => a.Equals(b);
    public static bool operator !=(Color a, Color b) => !(a == b);

    public static bool TryParse(string source, out Color result)
    {
        PropertyInfo p = typeof(Color).GetProperty(source, BindingFlags.Static | BindingFlags.Public);
        if (p == null)
        {
            if (Vector4.TryParse(source, out Vector4 v))
            {
                result = v.ToColor();
                return true;
            }
            result = None;
            return false;
        }
        result = (Color)p.GetValue(null)!;
        return true;
    }

    public static Color Parse(string str)
    {
        if (TryParse(str, out Color c))
            return c;
        throw new FormatException($"Color {str} in unexpected format");
    }

    private static Random rnd = new();
    public static Color Random() =>
        Byte(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));

    public static Color RandomAlpha() =>
        Byte(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));

    public static Color operator +(Color a, Color b) => new(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
    public static Color operator -(Color a, Color b) => new(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
    public static Color operator *(Color a, Color b) => new(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
    public static Color operator /(Color a, Color b) => new(a.r / b.r, a.g / b.g, a.b / b.b, a.a / b.a);
    public static Color operator *(Color a, float b) => new(a.r * b, a.g * b, a.b * b, a.a * b);
    public static Color operator /(Color a, float b) => new(a.r / b, a.g / b, a.b / b, a.a / b);

}