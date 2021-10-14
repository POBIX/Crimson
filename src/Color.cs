using System;
using System.Reflection;
using SColor = System.Drawing.Color;

// you can't override GetHashCode() in here, since there are no readonly fields.
#pragma warning disable 659,660,661

namespace Crimson
{
    public struct Color
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public Color(float r, float g, float b, float a = 1)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public SColor ToSystem() => SColor.FromArgb((int)(a * 255), (int)(r * 255), (int)(g * 255), (int)(b * 255));

        public static Color White => new(1, 1, 1);
        public static Color Black => new(0, 0, 0);
        public static Color Transparent => new(0, 0, 0, 0);
        public static Color None => new(0, 0, 0, 0);
        public static Color Blue => new(0, 0, 1);
        public static Color Green => new(0, 1, 0);
        public static Color Red => new(1, 0, 0);
        public static Color Yellow => new(1, 1, 0);

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

        public static Color Random()
        {
            Random rnd = new();
            return new(rnd.Next(0, 255) / 255f, rnd.Next(0, 255) / 255f, rnd.Next(0, 255) / 255f);
        }

        public static Color RandomAlpha()
        {
            Random rnd = new();
            return new(rnd.Next(0, 255) / 255f, rnd.Next(0, 255) / 255f, rnd.Next(0, 255) / 255f, rnd.Next(0, 255) / 255f);
        }

        public static Color operator +(Color a, Color b) => new(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
        public static Color operator -(Color a, Color b) => new(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
        public static Color operator *(Color a, Color b) => new(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
        public static Color operator /(Color a, Color b) => new(a.r / b.r, a.g / b.g, a.b / b.b, a.a / b.a);
        public static Color operator *(Color a, float b) => new(a.r * b, a.g * b, a.b * b, a.a * b);
        public static Color operator /(Color a, float b) => new(a.r / b, a.g / b, a.b / b, a.a / b);

    }
}
