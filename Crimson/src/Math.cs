/*
 * Most of this file was taken (and heavily modified) from https://github.com/dwmkerr/glmnet.
 */

using System.Diagnostics.Contracts;
using static System.MathF;

// weird warnings about overloading Equals() and GetHashCode()
#pragma warning disable 659,660,661

namespace Crimson;

public struct Vector2
{
    public float x, y;

    public Vector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.x + b.x, a.y + b.y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.x - b.x, a.y - b.y);
    public static Vector2 operator *(Vector2 a, Vector2 b) => new(a.x * b.x, a.y * b.y);
    public static Vector2 operator /(Vector2 a, Vector2 b) => new(a.x / b.x, a.y / b.y);
    public static Vector2 operator *(Vector2 a, float b) => new(a.x * b, a.y * b);
    public static Vector2 operator /(Vector2 a, float b) => new(a.x / b, a.y / b);
    public static Vector2 operator *(float a, Vector2 b) => new(a * b.x, a * b.y);
    public static Vector2 operator /(float a, Vector2 b) => new(a / b.x, a / b.y);

    public static bool operator ==(Vector2 a, Vector2 b) => a.x == b.x && a.y == b.y;
    public static bool operator !=(Vector2 a, Vector2 b) => !(a == b);

    public static Vector2 operator -(Vector2 v) => v * -1;

    /// <summary> The vector's magnitude squared. Faster than <see cref="Length"/></summary>
    public float SquareLength() => x * x + y * y;
    /// <summary> The vector's magnitude. <see cref="SquareLength"/> is faster. </summary>
    public float Length() => Sqrt(SquareLength());

    /// <summary>
    /// Returns this vector normalized without modifying it.
    /// If the vector's length is 0, it will return a (0, 0) vector.
    /// </summary>
    public Vector2 Normalized()
    {
        float l = Length();
        return l == 0 ? new(0, 0) : new(x / l, y / l);
    }

    /// <summary>
    /// normalizes this vector.
    /// If the vector's length is 0, it will return a (0, 0) vector.
    /// </summary>
    public void Normalize() => this = Normalized();

    /// <summary> Returns the direction to another vector. </summary>
    public Vector2 DirectionTo(Vector2 v) => (v - this).Normalized();
    public float SquareDistanceTo(Vector2 v) => Pow(x - v.x, 2) + Pow(y - v.y, 2);
    public float DistanceTo(Vector2 v) => Sqrt(SquareDistanceTo(v));

    public float Dot(Vector2 v) => x * v.x + y * v.y;

    public Vector2 Round() => new(MathF.Round(x), MathF.Round(y));
    public Vector2 Floor() => new(MathF.Floor(x), MathF.Floor(y));
    public Vector2 Ceil() => new(Ceiling(x), Ceiling(y));

    public Vector2 Abs() => new(MathF.Abs(x), MathF.Abs(y));

    public override string ToString() => $"({x}, {y})";

    public static Vector2 Zero => new();
    public static Vector2 One => new(1, 1);
    public static Vector2 Up => new(0, -1);
    public static Vector2 Down => new(0, 1);
    public static Vector2 Left => new(-1, 0);
    public static Vector2 Right => new(1, 0);

    public float this[int i]
    {
        get => i switch {0 => x, 1 => y, _ => throw new ArgumentOutOfRangeException(nameof(i), i, null)};
        set
        {
            switch (i)
            {
                case 0:
                    x = value;
                    break;
                case 1:
                    y = value;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(i), i, null);
            }
        }
    }

    /// <summary>
    /// Takes an "(x,y)" string and turns it into a Vector2.
    /// </summary>
    public static Vector2 Parse(string str)
    {
        if (!TryParse(str, out Vector2 v))
            throw new FormatException($"input {str} was in an incorrect format.");
        return v;
    }

    /// <summary>
    /// Takes an "(x,y)" string and turns it into a Vector2.
    /// </summary>
    public static bool TryParse(string str, out Vector2 result)
    {
        if (!str.Contains('(') || !str.Contains(')'))
        {
            result = Zero;
            return false;
        }

        string[] split = str.Replace(" ", "").Trim('(').Trim(')').Split(',');
        if (split.Length != 2)
        {
            result = Zero;
            return false;
        }

        result = new();
        return float.TryParse(split[0], out result.x) && float.TryParse(split[1], out result.y);
    }
    public Vector2 Rotated(float theta)
    {
        float cs = Cos(theta);
        float sn = Sin(theta);
        return new(x * cs - y * sn, x * sn + y * cs);
    }

    public Vector2 Cross()
    {
        Vector3 d = Vector3.Forward.Cross(new(this, 0));
        return new(d.x, d.y);
    }

    public float Cross(Vector2 with) => x * with.y - y * with.x;

    public float Angle() => Mathf.Atan2(y, x);
    public Vector2 Sign() => new(Mathf.Sign(x), Mathf.Sign(y));

    public static implicit operator System.Numerics.Vector2(Vector2 v) => new(v.x, v.y);
    public static implicit operator Vector2(System.Numerics.Vector2 v) => new(v.X, v.Y);
}

public struct Rect
{
    public float x, y, w, h;

    public Vector2 Size
    {
        get => new(w, h);
        set
        {
            w = value.x;
            h = value.y;
        }
    }

    public Vector2 Position
    {
        get => new(x, y);
        set
        {
            x = value.x;
            y = value.y;
        }
    }

    public Rect(float x, float y, float w, float h)
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
    }

    public Rect(Vector2 position, Vector2 size) : this(position.x, position.y, size.x, size.y) { }

    public bool Intersects(Rect r) =>
        x < r.x + r.w && x + w > r.x &&
        y < r.y + r.h && y + h > r.y;

    public override string ToString() => $"(Position: {Position}, Size: {Size})";

    /// <summary>
    /// Takes a "[x, y, w, h]" string and turns it into a Rect.
    /// </summary>
    public static Rect Parse(string str)
    {
        if (!TryParse(str, out Rect r))
            throw new FormatException($"input {str} was in an incorrect format.");
        return r;
    }

    /// <summary>
    /// Takes a "[x, y, w, h]" string and turns it into a Rect.
    /// </summary>
    public static bool TryParse(string str, out Rect result)
    {
        if (!str.Contains('[') || !str.Contains(']'))
        {
            result = new Rect();
            return false;
        }

        string[] split = str.Replace(" ", "").Trim('[').Trim(']').Split(',');
        if (split.Length != 4)
        {
            result = new Rect();
            return false;
        }

        result = new Rect();
        return float.TryParse(split[0], out result.x) && float.TryParse(split[1], out result.y) &&
               float.TryParse(split[2], out result.w) && float.TryParse(split[3], out result.h);
    }
}

public static class Mathf
{
    public static float Lerp(float a, float b, float t) => (1 - t) * a + t * b;
    public static Vector2 Lerp(Vector2 a, Vector2 b, float t) => (1 - t) * a + t * b;

    public static void Swap<T>(ref T a, ref T b)
    {
        T c = a;
        a = b;
        b = c;
    }
    public const float Pi = PI;
    public const float Tau = MathF.Tau;

    public static float Pow(float x, float y) => MathF.Pow(x, y);
    public static float Abs(float f) => f < 0 ? -f : f;
    public static Vector2 Abs(Vector2 v) => new(Abs(v.x), Abs(v.y));
    public static float Cos(float alpha) => MathF.Cos(alpha);
    public static float Sin(float alpha) => MathF.Sin(alpha);
    public static float Tan(float alpha) => MathF.Tan(alpha);
    public static float Sin2(float alpha) => Pow(Sin(alpha), 2);
    public static float Cos2(float alpha) => Pow(Cos(alpha), 2);
    public static float Tan2(float alpha) => Pow(Tan(alpha), 2);
    public static float Atan2(float y, float x) => MathF.Atan2(y, x);
    public static float Sqrt(float x) => MathF.Sqrt(x);

    public static float Max(float a, float b) => a > b ? a : b;
    public static float Min(float a, float b) => a < b ? a : b;
    public static int Max(int a, int b) => a > b ? a : b;
    public static int Min(int a, int b) => a < b ? a : b;
    public static int Sign(float n) => n == 0 ? 0 : n > 0 ? 1 : -1;
    public static float Clamp(float value, float min, float max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
    public static int Clamp(int value, int min, int max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
    public static float LerpAngle(float a, float b, float t)
    {
        float diff = (b - a) % Tau;
        float dist = 2 * diff % Tau - diff;
        return a + dist * t;
    }

    public static float Degrees(float rads) => rads * 180f / Pi;
    public static float Radians(float degs) => degs * Pi / 180f;
}

/// <summary>
/// Represents a 4x4 matrix.
/// </summary>
public struct Matrix
{
    /// <summary>
    /// The columns of the matrix.
    /// </summary>
    private Vector4[] cols;

    #region Construction

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix"/> struct.
    /// This matrix is the identity matrix scaled by <paramref name="scale"/>.
    /// </summary>
    /// <param name="scale">The scale.</param>
    public Matrix(float scale) =>
        cols = new[]
        {
            new Vector4(scale, 0.0f, 0.0f, 0.0f),
            new Vector4(0.0f, scale, 0.0f, 0.0f),
            new Vector4(0.0f, 0.0f, scale, 0.0f),
            new Vector4(0.0f, 0.0f, 0.0f, scale),
        };

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix"/> struct.
    /// The matrix is initialised with the <paramref name="cols"/>.
    /// </summary>
    /// <param name="cols">The columns of the matrix.</param>
    public Matrix(Vector4[] cols) =>
        this.cols = cols;

    public Matrix(Vector4 a, Vector4 b, Vector4 c, Vector4 d) =>
        cols = new [] {a, b, c, d};

    /// <summary>
    /// Creates an identity matrix.
    /// </summary>
    /// <returns>A new identity matrix.</returns>
    public static Matrix Identity => new(1);

    #endregion

    #region Index Access

    /// <summary>
    /// Gets or sets the <see cref="Vector4"/> column at the specified index.
    /// </summary>
    /// <value>
    /// The <see cref="Vector4"/> column.
    /// </value>
    /// <param name="column">The column index.</param>
    /// <returns>The column at index <paramref name="column"/>.</returns>
    public Vector4 this[int column] { get => cols[column]; set => cols[column] = value; }

    /// <summary>
    /// Gets or sets the element at <paramref name="column"/> and <paramref name="row"/>.
    /// </summary>
    /// <value>
    /// The element at <paramref name="column"/> and <paramref name="row"/>.
    /// </value>
    /// <param name="column">The column index.</param>
    /// <param name="row">The row index.</param>
    /// <returns>
    /// The element at <paramref name="column"/> and <paramref name="row"/>.
    /// </returns>
    public float this[int column, int row] { get => cols[column][row]; set => cols[column][row] = value; }

    #endregion

    #region Conversion

    /// <summary>
    /// Returns the matrix as a flat array of elements, column major.
    /// </summary>
    /// <returns></returns>
    [Pure]
    public float[] ToArray() =>
        new[]
        {
            cols[0].x, cols[0].y, cols[0].z, cols[0].w,
            cols[1].x, cols[1].y, cols[1].z, cols[1].w,
            cols[2].x, cols[2].y, cols[2].z, cols[2].w,
            cols[3].x, cols[3].y, cols[3].z, cols[3].w,
        };

    /// <summary>
    /// Returns the <see cref="Matrix3"/> portion of this matrix.
    /// </summary>
    /// <returns>The <see cref="Matrix3"/> portion of this matrix.</returns>
    public Matrix3 ToMatrix3() =>
        new(new[]
        {
            new Vector3(cols[0][0], cols[0][1], cols[0][2]),
            new Vector3(cols[1][0], cols[1][1], cols[1][2]),
            new Vector3(cols[2][0], cols[2][1], cols[2][2])
        });

    #endregion

    #region Multiplication

    /// <summary>
    /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> vector.
    /// </summary>
    /// <param name="lhs">The left hand side matrix.</param>
    /// <param name="rhs">The right hand side vector.</param>
    /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
    public static Vector4 operator *(Matrix lhs, Vector4 rhs) =>
        new(
            lhs[0, 0] * rhs[0] + lhs[1, 0] * rhs[1] + lhs[2, 0] * rhs[2] + lhs[3, 0] * rhs[3],
            lhs[0, 1] * rhs[0] + lhs[1, 1] * rhs[1] + lhs[2, 1] * rhs[2] + lhs[3, 1] * rhs[3],
            lhs[0, 2] * rhs[0] + lhs[1, 2] * rhs[1] + lhs[2, 2] * rhs[2] + lhs[3, 2] * rhs[3],
            lhs[0, 3] * rhs[0] + lhs[1, 3] * rhs[1] + lhs[2, 3] * rhs[2] + lhs[3, 3] * rhs[3]
        );

    /// <summary>
    /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> matrix.
    /// </summary>
    /// <param name="lhs">The left hand side matrix.</param>
    /// <param name="rhs">The right hand side matrix.</param>
    /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
    public static Matrix operator *(Matrix lhs, Matrix rhs) =>
        new(new[]
        {
            rhs[0][0] * lhs[0] + rhs[0][1] * lhs[1] + rhs[0][2] * lhs[2] + rhs[0][3] * lhs[3],
            rhs[1][0] * lhs[0] + rhs[1][1] * lhs[1] + rhs[1][2] * lhs[2] + rhs[1][3] * lhs[3],
            rhs[2][0] * lhs[0] + rhs[2][1] * lhs[1] + rhs[2][2] * lhs[2] + rhs[2][3] * lhs[3],
            rhs[3][0] * lhs[0] + rhs[3][1] * lhs[1] + rhs[3][2] * lhs[2] + rhs[3][3] * lhs[3]
        });

    public static Matrix operator *(Matrix lhs, float s) =>
        new(new[]
        {
            lhs[0] * s,
            lhs[1] * s,
            lhs[2] * s,
            lhs[3] * s
        });

    #endregion

    public override string ToString() =>
        $"[{this[0, 0]}, {this[1, 0]}, {this[2, 0]}, {this[3, 0]}\n" +
        $" {this[0, 1]}, {this[1, 1]}, {this[2, 1]}, {this[3, 1]}\n" +
        $" {this[0, 2]}, {this[1, 2]}, {this[2, 2]}, {this[3, 2]}\n" +
        $" {this[0, 3]}, {this[1, 3]}, {this[2, 3]}, {this[3, 3]}]";

    #region Comparision

    /// <summary>
    /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
    /// The Difference is detected by the different values
    /// </summary>
    /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object obj) =>
        obj is Matrix mat && mat[0] == this[0] && mat[1] == this[1] && mat[2] == this[2] && mat[3] == this[3];

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="m1">The first Matrix.</param>
    /// <param name="m2">The second Matrix.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Matrix m1, Matrix m2) =>
        m1.Equals(m2);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="m1">The first Matrix.</param>
    /// <param name="m2">The second Matrix.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Matrix m1, Matrix m2) =>
        !m1.Equals(m2);

    #endregion

    #region Generators

    /// <summary>
    /// Creates a frustum projection matrix.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <param name="bottom">The bottom.</param>
    /// <param name="top">The top.</param>
    /// <param name="nearVal">The near val.</param>
    /// <param name="farVal">The far val.</param>
    public static Matrix Frustum(float left, float right, float bottom, float top, float nearVal, float farVal)
    {
        var result = Identity;
        result[0, 0] = 2.0f * nearVal / (right - left);
        result[1, 1] = 2.0f * nearVal / (top - bottom);
        result[2, 0] = (right + left) / (right - left);
        result[2, 1] = (top + bottom) / (top - bottom);
        result[2, 2] = -(farVal + nearVal) / (farVal - nearVal);
        result[2, 3] = -1.0f;
        result[3, 2] = -(2.0f * farVal * nearVal) / (farVal - nearVal);
        return result;
    }

    /// <summary>
    /// Creates a matrix for a symmetric perspective-view frustum with far plane at infinite.
    /// </summary>
    /// <param name="fovy">The fovy.</param>
    /// <param name="aspect">The aspect.</param>
    /// <param name="zNear">The z near.</param>
    /// <returns></returns>
    public static Matrix InfinitePerspective(float fovy, float aspect, float zNear)
    {
        float range = Tan(fovy / 2f) * zNear;

        float left = -range * aspect;
        float right = range * aspect;
        float bottom = -range;
        float top = range;

        var result = new Matrix(0)
        {
            [0, 0] = 2f * zNear / (right - left),
            [1, 1] = 2f * zNear / (top - bottom),
            [2, 2] = -1f,
            [2, 3] = -1f,
            [3, 2] = -2f * zNear
        };
        return result;
    }

    /// <summary>
    /// Build a look at view matrix.
    /// </summary>
    /// <param name="eye">The eye.</param>
    /// <param name="center">The center.</param>
    /// <param name="up">Up.</param>
    /// <returns></returns>
    public static Matrix LookAt(Vector3 eye, Vector3 center, Vector3 up)
    {
        Vector3 f = (center - eye).Normalized();
        Vector3 s = f.Cross(up).Normalized();
        Vector3 u = s.Cross(f);

        Matrix result = new Matrix(1)
        {
            [0, 0] = s.x,
            [1, 0] = s.y,
            [2, 0] = s.z,
            [0, 1] = u.x,
            [1, 1] = u.y,
            [2, 1] = u.z,
            [0, 2] = -f.x,
            [1, 2] = -f.y,
            [2, 2] = -f.z,
            [3, 0] = -s.Dot(eye),
            [3, 1] = -u.Dot(eye),
            [3, 2] = f.Dot(eye)
        };
        return result;
    }

    /// <summary>
    /// Creates a matrix for an orthographic parallel viewing volume.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <param name="bottom">The bottom.</param>
    /// <param name="top">The top.</param>
    /// <param name="zNear">The z near.</param>
    /// <param name="zFar">The z far.</param>
    /// <returns></returns>
    public static Matrix Ortho(float left, float right, float bottom, float top, float zNear, float zFar)
    {
        var result = Identity;
        result[0, 0] = 2f / (right - left);
        result[1, 1] = 2f / (top - bottom);
        result[2, 2] = -2f / (zFar - zNear);
        result[3, 0] = -(right + left) / (right - left);
        result[3, 1] = -(top + bottom) / (top - bottom);
        result[3, 2] = -(zFar + zNear) / (zFar - zNear);
        return result;
    }

    /// <summary>
    /// Creates a matrix for projecting two-dimensional coordinates onto the screen.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <param name="bottom">The bottom.</param>
    /// <param name="top">The top.</param>
    /// <returns></returns>
    public static Matrix Ortho(float left, float right, float bottom, float top)
    {
        var result = Identity;
        result[0, 0] = 2f / (right - left);
        result[1, 1] = 2f / (top - bottom);
        result[2, 2] = -1f;
        result[3, 0] = -(right + left) / (right - left);
        result[3, 1] = -(top + bottom) / (top - bottom);
        return result;
    }

    /// <summary>
    /// Creates a perspective transformation matrix.
    /// </summary>
    /// <param name="fovy">The field of view angle, in radians.</param>
    /// <param name="aspect">The aspect ratio.</param>
    /// <param name="zNear">The near depth clipping plane.</param>
    /// <param name="zFar">The far depth clipping plane.</param>
    /// <returns>A <see cref="Matrix"/> that contains the projection matrix for the perspective transformation.</returns>
    public static Matrix Perspective(float fovy, float aspect, float zNear, float zFar)
    {
        var tanHalfFovy = (float)Math.Tan(fovy / 2.0f);

        var result = Identity;
        result[0, 0] = 1.0f / (aspect * tanHalfFovy);
        result[1, 1] = 1.0f / tanHalfFovy;
        result[2, 2] = -(zFar + zNear) / (zFar - zNear);
        result[2, 3] = -1.0f;
        result[3, 2] = -(2.0f * zFar * zNear) / (zFar - zNear);
        result[3, 3] = 0.0f;
        return result;
    }

    /// <summary>
    /// Builds a perspective projection matrix based on a field of view.
    /// </summary>
    /// <param name="fov">The fov (in radians).</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="zNear">The z near.</param>
    /// <param name="zFar">The z far.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static Matrix PerspectiveFov(float fov, float width, float height, float zNear, float zFar)
    {
        if (width <= 0)
            throw new ArgumentOutOfRangeException(nameof(width), width, "Must be larger than 0");

        if (height <= 0)
            throw new ArgumentOutOfRangeException(nameof(height), height, "Must be larger than 0");

        if (fov <= 0)
            throw new ArgumentOutOfRangeException(nameof(fov), fov, "Must be larger than 0");

        var rad = fov;

        var h = Cos(0.5f * rad) / Sin(0.5f * rad);
        var w = h * height / width;

        var result = new Matrix(0)
        {
            [0, 0] = w,
            [1, 1] = h,
            [2, 2] = -(zFar + zNear) / (zFar - zNear),
            [2, 3] = -1f,
            [3, 2] = -(2f * zFar * zNear) / (zFar - zNear)
        };
        return result;
    }

    /// <summary>
    /// Set a picking region.
    /// </summary>
    /// <param name="center">The center.</param>
    /// <param name="delta">The delta.</param>
    /// <param name="viewport">The viewport.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static Matrix PickMatrix(Vector2 center, Vector2 delta, Vector4 viewport)
    {
        if (delta.x <= 0)
            throw new ArgumentOutOfRangeException(nameof(delta), delta, "x must be larger than 0.");
        if (delta.y <= 0)
            throw new ArgumentOutOfRangeException(nameof(delta), delta, "y must be larger than 0.");

        var result = new Matrix(1.0f);

        if (!(delta.x > 0f && delta.y > 0f))
            return result; // Error

        Vector3 temp = new Vector3(
            (viewport[2] - 2f * (center.x - viewport[0])) / delta.x,
            (viewport[3] - 2f * (center.y - viewport[1])) / delta.y,
            0f);

        // Translate and scale the picked region to the entire window
        result = result.Translate(temp);
        return result.Scale(new Vector3(viewport[2] / delta.x, viewport[3] / delta.y, 1));
    }

    /// <summary>
    /// Creates a matrix for a symmetric perspective-view frustum with far plane
    /// at infinite for graphics hardware that doesn't support depth clamping.
    /// </summary>
    /// <param name="fovy">The fovy.</param>
    /// <param name="aspect">The aspect.</param>
    /// <param name="zNear">The z near.</param>
    /// <returns></returns>
    public static Matrix TweakedInfinitePerspective(float fovy, float aspect, float zNear)
    {
        float range = Tan(fovy / 2) * zNear;
        float left = -range * aspect;
        float right = range * aspect;
        float bottom = -range;

        Matrix result = new Matrix(0f)
        {
            [0, 0] = 2 * zNear / (right - left),
            [1, 1] = 2 * zNear / (range - bottom),
            [2, 2] = 0.0001f - 1f,
            [2, 3] = -1,
            [3, 2] = -(0.0001f - 2) * zNear
        };
        return result;
    }

    #endregion

    /// <summary>
    /// Builds a rotation 4 * 4 matrix created from an axis vector and an angle.
    /// </summary>
    /// <param name="m">The m.</param>
    /// <param name="angle">The angle.</param>
    /// <param name="v">The v.</param>
    /// <returns></returns>
    public Matrix Rotate(float angle, Vector3 v)
    {
        float c = Cos(angle);
        float s = Sin(angle);

        Vector3 axis = v.Normalized();
        Vector3 temp = (1.0f - c) * axis;

        Matrix rotate = Identity;
        rotate[0, 0] = c + temp[0] * axis[0];
        rotate[0, 1] = 0 + temp[0] * axis[1] + s * axis[2];
        rotate[0, 2] = 0 + temp[0] * axis[2] - s * axis[1];

        rotate[1, 0] = 0 + temp[1] * axis[0] - s * axis[2];
        rotate[1, 1] = c + temp[1] * axis[1];
        rotate[1, 2] = 0 + temp[1] * axis[2] + s * axis[0];

        rotate[2, 0] = 0 + temp[2] * axis[0] + s * axis[1];
        rotate[2, 1] = 0 + temp[2] * axis[1] - s * axis[0];
        rotate[2, 2] = c + temp[2] * axis[2];

        Matrix result = Identity;
        result[0] = this[0] * rotate[0][0] + this[1] * rotate[0][1] + this[2] * rotate[0][2];
        result[1] = this[0] * rotate[1][0] + this[1] * rotate[1][1] + this[2] * rotate[1][2];
        result[2] = this[0] * rotate[2][0] + this[1] * rotate[2][1] + this[2] * rotate[2][2];
        result[3] = this[3];
        return result;
    }

    /// <summary>
    /// Builds a rotation 4 * 4 matrix created from an axis vector and an angle.
    /// </summary>
    /// <param name="m">The m.</param>
    /// <param name="angle">The angle.</param>
    /// <param name="v">The v.</param>
    /// <returns></returns>
    public static Matrix Rotation(float angle, Vector3 v) => Identity.Rotate(angle, v);

    /// <summary>
    /// Applies a scale transformation to matrix <paramref name="m"/> by vector <paramref name="v"/>.
    /// </summary>
    /// <param name="m">The matrix to transform.</param>
    /// <param name="v">The vector to scale by.</param>
    /// <returns><paramref name="m"/> scaled by <paramref name="v"/>.</returns>
    public Matrix Scale(Vector3 v)
    {
        Matrix result = this;
        result[0] = this[0] * v[0];
        result[1] = this[1] * v[1];
        result[2] = this[2] * v[2];
        result[3] = this[3];
        return result;
    }

    /// <summary>
    /// Applies a scale transformation to matrix <paramref name="m"/> by vector <paramref name="v"/>.
    /// </summary>
    /// <param name="m">The matrix to transform.</param>
    /// <param name="v">The vector to scale by.</param>
    /// <returns><paramref name="m"/> scaled by <paramref name="v"/>.</returns>
    public static Matrix Scaling(Vector3 v) => Identity.Scale(v);

    /// <summary>
    /// Applies a translation transformation to matrix <paramref name="m"/> by vector <paramref name="v"/>.
    /// </summary>
    /// <param name="m">The matrix to transform.</param>
    /// <param name="v">The vector to translate by.</param>
    /// <returns><paramref name="m"/> translated by <paramref name="v"/>.</returns>
    public Matrix Translate(Vector3 v)
    {
        Matrix result = this;
        result[3] = this[0] * v[0] + this[1] * v[1] + this[2] * v[2] + this[3];
        return result;
    }

    /// <summary>
    /// Applies a translation transformation to matrix <paramref name="m"/> by vector <paramref name="v"/>.
    /// </summary>
    /// <param name="m">The matrix to transform.</param>
    /// <param name="v">The vector to translate by.</param>
    /// <returns><paramref name="m"/> translated by <paramref name="v"/>.</returns>
    public static Matrix Translation(Vector3 v) => Identity.Translate(v);

    public Matrix Inverse()
    {
        float coef00 = this[2][2] * this[3][3] - this[3][2] * this[2][3];
        float coef02 = this[1][2] * this[3][3] - this[3][2] * this[1][3];
        float coef03 = this[1][2] * this[2][3] - this[2][2] * this[1][3];

        float coef04 = this[2][1] * this[3][3] - this[3][1] * this[2][3];
        float coef06 = this[1][1] * this[3][3] - this[3][1] * this[1][3];
        float coef07 = this[1][1] * this[2][3] - this[2][1] * this[1][3];

        float coef08 = this[2][1] * this[3][2] - this[3][1] * this[2][2];
        float coef10 = this[1][1] * this[3][2] - this[3][1] * this[1][2];
        float coef11 = this[1][1] * this[2][2] - this[2][1] * this[1][2];

        float coef12 = this[2][0] * this[3][3] - this[3][0] * this[2][3];
        float coef14 = this[1][0] * this[3][3] - this[3][0] * this[1][3];
        float coef15 = this[1][0] * this[2][3] - this[2][0] * this[1][3];

        float coef16 = this[2][0] * this[3][2] - this[3][0] * this[2][2];
        float coef18 = this[1][0] * this[3][2] - this[3][0] * this[1][2];
        float coef19 = this[1][0] * this[2][2] - this[2][0] * this[1][2];

        float coef20 = this[2][0] * this[3][1] - this[3][0] * this[2][1];
        float coef22 = this[1][0] * this[3][1] - this[3][0] * this[1][1];
        float coef23 = this[1][0] * this[2][1] - this[2][0] * this[1][1];

        Vector4 fac0 = new Vector4(coef00, coef00, coef02, coef03);
        Vector4 fac1 = new Vector4(coef04, coef04, coef06, coef07);
        Vector4 fac2 = new Vector4(coef08, coef08, coef10, coef11);
        Vector4 fac3 = new Vector4(coef12, coef12, coef14, coef15);
        Vector4 fac4 = new Vector4(coef16, coef16, coef18, coef19);
        Vector4 fac5 = new Vector4(coef20, coef20, coef22, coef23);

        Vector4 vec0 = new Vector4(this[1][0], this[0][0], this[0][0], this[0][0]);
        Vector4 vec1 = new Vector4(this[1][1], this[0][1], this[0][1], this[0][1]);
        Vector4 vec2 = new Vector4(this[1][2], this[0][2], this[0][2], this[0][2]);
        Vector4 vec3 = new Vector4(this[1][3], this[0][3], this[0][3], this[0][3]);

        Vector4 inv0 = vec1 * fac0 - vec2 * fac1 + vec3 * fac2;
        Vector4 inv1 = vec0 * fac0 - vec2 * fac3 + vec3 * fac4;
        Vector4 inv2 = vec0 * fac1 - vec1 * fac3 + vec3 * fac5;
        Vector4 inv3 = vec0 * fac2 - vec1 * fac4 + vec2 * fac5;

        Vector4 signA = new Vector4(+1, -1, +1, -1);
        Vector4 signB = new Vector4(-1, +1, -1, +1);
        Matrix inverse = new Matrix(inv0 * signA, inv1 * signB, inv2 * signA, inv3 * signB);

        Vector4 row0 = new Vector4(inverse[0][0], inverse[1][0], inverse[2][0], inverse[3][0]);

        Vector4 dot0 = this[0] * row0;
        float dot1 = dot0.x + dot0.y + (dot0.z + dot0.w);

        float oneOverDeterminant = 1f / dot1;

        return inverse * oneOverDeterminant;
    }

    /// <summary>
    /// Creates a 2D transformation matrix.
    /// </summary>
    /// <param name="position">The translation</param>
    /// <param name="rotation">The angle (in radians)</param>
    /// <param name="scale">The scale</param>
    /// <returns></returns>
    public static Matrix Transformation(Vector2 position, float rotation, Vector2 scale) =>
        Translation(new(position, 0)).Rotate(rotation, new(0, 0, 1)).Scale(new(scale, 0));
}

/// <summary>
/// Represents a 2x2 matrix.
/// </summary>
public struct Matrix2
{
    /// <summary>
    /// The columns of the matrix.
    /// </summary>
    private Vector2[] cols;

    #region Construction

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix2"/> struct.
    /// This matrix is the identity matrix scaled by <paramref name="scale"/>.
    /// </summary>
    /// <param name="scale">The scale.</param>
    public Matrix2(float scale) =>
        cols = new[]
        {
            new Vector2(scale, 0.0f),
            new Vector2(0.0f, scale)
        };

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix2"/> struct.
    /// The matrix is initialised with the <paramref name="cols"/>.
    /// </summary>
    /// <param name="cols">The columns of the matrix.</param>
    public Matrix2(Vector2[] cols) =>
        this.cols = new[]
        {
            cols[0],
            cols[1]
        };

    public Matrix2(Vector2 a, Vector2 b) =>
        cols = new[]
        {
            a, b
        };

    public Matrix2(float a, float b, float c, float d) =>
        cols = new[]
        {
            new Vector2(a, b), new Vector2(c, d)
        };

    /// <summary>
    /// Creates an identity matrix.
    /// </summary>
    /// <returns>A new identity matrix.</returns>
    public static Matrix2 Identity() =>
        new()
        {
            cols = new[]
            {
                new Vector2(1, 0),
                new Vector2(0, 1)
            }
        };

    #endregion

    #region Index Access

    /// <summary>
    /// Gets or sets the <see cref="Vector2"/> column at the specified index.
    /// </summary>
    /// <value>
    /// The <see cref="Vector2"/> column.
    /// </value>
    /// <param name="column">The column index.</param>
    /// <returns>The column at index <paramref name="column"/>.</returns>
    public Vector2 this[int column] { get => cols[column]; set => cols[column] = value; }

    /// <summary>
    /// Gets or sets the element at <paramref name="column"/> and <paramref name="row"/>.
    /// </summary>
    /// <value>
    /// The element at <paramref name="column"/> and <paramref name="row"/>.
    /// </value>
    /// <param name="column">The column index.</param>
    /// <param name="row">The row index.</param>
    /// <returns>
    /// The element at <paramref name="column"/> and <paramref name="row"/>.
    /// </returns>
    public float this[int column, int row] { get => cols[column][row]; set => cols[column][row] = value; }

    #endregion

    #region Conversion

    /// <summary>
    /// Returns the matrix as a flat array of elements, column major.
    /// </summary>
    /// <returns></returns>
    [Pure]
    public float[] ToArray() =>
        new[]
        {
            cols[0].x, cols[0].y,
            cols[1].x, cols[1].y
        };

    #endregion

    #region Multiplication

    /// <summary>
    /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> vector.
    /// </summary>
    /// <param name="lhs">The left hand side matrix.</param>
    /// <param name="rhs">The right hand side vector.</param>
    /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
    public static Vector2 operator *(Matrix2 lhs, Vector2 rhs) =>
        new(
            lhs[0, 0] * rhs[0] + lhs[1, 0] * rhs[1],
            lhs[0, 1] * rhs[0] + lhs[1, 1] * rhs[1]
        );

    /// <summary>
    /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> matrix.
    /// </summary>
    /// <param name="lhs">The left hand side matrix.</param>
    /// <param name="rhs">The right hand side matrix.</param>
    /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
    public static Matrix2 operator *(Matrix2 lhs, Matrix2 rhs) =>
        new(new[]
        {
            lhs[0][0] * rhs[0] + lhs[1][0] * rhs[1],
            lhs[0][1] * rhs[0] + lhs[1][1] * rhs[1]
        });

    public static Matrix2 operator *(Matrix2 lhs, float s) =>
        new(new[]
        {
            lhs[0] * s,
            lhs[1] * s
        });

    #endregion

    public override string ToString() =>
        $"[{this[0, 0]}, {this[1, 0]}; {this[0, 1]}, {this[1, 1]}]";

    #region Comparison

    /// <summary>
    /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
    /// The Difference is detected by the different values
    /// </summary>
    /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object obj) =>
        obj is Matrix2 mat && mat[0] == this[0] && mat[1] == this[1];

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="m1">The first Matrix.</param>
    /// <param name="m2">The second Matrix.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Matrix2 m1, Matrix2 m2) =>
        m1.Equals(m2);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="m1">The first Matrix.</param>
    /// <param name="m2">The second Matrix.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Matrix2 m1, Matrix2 m2) =>
        !m1.Equals(m2);

    #endregion

    public static Matrix2 Inverse(Matrix2 m)
    {
        float oneOverDeterminant = 1f / (
            +m[0][0] * m[1][1]
            - m[1][0] * m[0][1]);

        Matrix2 inverse = new Matrix2(
            +m[1][1] * oneOverDeterminant,
            -m[0][1] * oneOverDeterminant,
            -m[1][0] * oneOverDeterminant,
            +m[0][0] * oneOverDeterminant);

        return inverse;
    }
}

/// <summary>
/// Represents a 3x3 matrix.
/// </summary>
public struct Matrix3
{
    /// <summary>
    /// The columns of the matrix.
    /// </summary>
    private Vector3[] cols;

    #region Construction

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix3"/> struct.
    /// This matrix is the identity matrix scaled by <paramref name="scale"/>.
    /// </summary>
    /// <param name="scale">The scale.</param>
    public Matrix3(float scale) =>
        cols = new[]
        {
            new Vector3(scale, 0.0f, 0.0f),
            new Vector3(0.0f, scale, 0.0f),
            new Vector3(0.0f, 0.0f, scale)
        };

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix3"/> struct.
    /// The matrix is initialised with the <paramref name="cols"/>.
    /// </summary>
    /// <param name="cols">The colums of the matrix.</param>
    public Matrix3(Vector3[] cols) =>
        this.cols = new[]
        {
            cols[0],
            cols[1],
            cols[2]
        };

    public Matrix3(Vector3 a, Vector3 b, Vector3 c) =>
        cols = new[]
        {
            a, b, c
        };

    /// <summary>
    /// Creates an identity matrix.
    /// </summary>
    /// <returns>A new identity matrix.</returns>
    public static Matrix3 Identity()
    {
        return new()
        {
            cols = new[]
            {
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 1)
            }
        };
    }

    #endregion

    #region Index Access

    /// <summary>
    /// Gets or sets the <see cref="Vector3"/> column at the specified index.
    /// </summary>
    /// <value>
    /// The <see cref="Vector3"/> column.
    /// </value>
    /// <param name="column">The column index.</param>
    /// <returns>The column at index <paramref name="column"/>.</returns>
    public Vector3 this[int column] { get => cols[column]; set => cols[column] = value; }

    /// <summary>
    /// Gets or sets the element at <paramref name="column"/> and <paramref name="row"/>.
    /// </summary>
    /// <value>
    /// The element at <paramref name="column"/> and <paramref name="row"/>.
    /// </value>
    /// <param name="column">The column index.</param>
    /// <param name="row">The row index.</param>
    /// <returns>
    /// The element at <paramref name="column"/> and <paramref name="row"/>.
    /// </returns>
    public float this[int column, int row] { get => cols[column][row]; set => cols[column][row] = value; }

    #endregion

    #region Conversion

    /// <summary>
    /// Returns the matrix as a flat array of elements, column major.
    /// </summary>
    /// <returns></returns>
    [Pure]
    public float[] ToArray()
    {
        return new[]
        {
            cols[0].x, cols[0].y, cols[0].z,
            cols[1].x, cols[1].y, cols[1].z,
            cols[2].x, cols[2].y, cols[2].z
        };
    }

    /// <summary>
    /// Returns the <see cref="Matrix3"/> portion of this matrix.
    /// </summary>
    /// <returns>The <see cref="Matrix3"/> portion of this matrix.</returns>
    public Matrix2 ToMatrix2()
    {
        return new(new[]
        {
            new Vector2(cols[0][0], cols[0][1]),
            new Vector2(cols[1][0], cols[1][1])
        });
    }

    #endregion

    #region Multiplication

    /// <summary>
    /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> vector.
    /// </summary>
    /// <param name="lhs">The left hand side matrix.</param>
    /// <param name="rhs">The right hand side vector.</param>
    /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
    public static Vector3 operator *(Matrix3 lhs, Vector3 rhs)
    {
        return new(
            lhs[0, 0] * rhs[0] + lhs[1, 0] * rhs[1] + lhs[2, 0] * rhs[2],
            lhs[0, 1] * rhs[0] + lhs[1, 1] * rhs[1] + lhs[2, 1] * rhs[2],
            lhs[0, 2] * rhs[0] + lhs[1, 2] * rhs[1] + lhs[2, 2] * rhs[2]
        );
    }

    /// <summary>
    /// Multiplies the <paramref name="lhs"/> matrix by the <paramref name="rhs"/> matrix.
    /// </summary>
    /// <param name="lhs">The left hand side matrix.</param>
    /// <param name="rhs">The right hand side matrix.</param>
    /// <returns>The product of <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
    public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
    {
        return new(new[]
        {
            lhs[0][0] * rhs[0] + lhs[1][0] * rhs[1] + lhs[2][0] * rhs[2],
            lhs[0][1] * rhs[0] + lhs[1][1] * rhs[1] + lhs[2][1] * rhs[2],
            lhs[0][2] * rhs[0] + lhs[1][2] * rhs[1] + lhs[2][2] * rhs[2]
        });
    }

    public static Matrix3 operator *(Matrix3 lhs, float s)
    {
        return new(new[]
        {
            lhs[0] * s,
            lhs[1] * s,
            lhs[2] * s
        });
    }

    #endregion

    public override string ToString() =>
        $"[{this[0, 0]}, {this[1, 0]}, {this[2, 0]};\n" +
        $" {this[0, 1]}, {this[1, 1]}, {this[2, 1]};\n" +
        $" {this[0, 2]}, {this[1, 2]}, {this[2, 2]}]";

    #region Comparison

    /// <summary>
    /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
    /// The Difference is detected by the different values
    /// </summary>
    /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object obj) =>
        obj is Matrix3 mat && mat[0] == this[0] && mat[1] == this[1] && mat[2] == this[2];

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="m1">The first Matrix.</param>
    /// <param name="m2">The second Matrix.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Matrix3 m1, Matrix3 m2) =>
        m1.Equals(m2);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="m1">The first Matrix.</param>
    /// <param name="m2">The second Matrix.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Matrix3 m1, Matrix3 m2) =>
        !m1.Equals(m2);

    #endregion

    public static Matrix3 Inverse(Matrix3 m)
    {
        float oneOverDeterminant = 1f / (
            +m[0][0] * (m[1][1] * m[2][2] - m[2][1] * m[1][2])
            - m[1][0] * (m[0][1] * m[2][2] - m[2][1] * m[0][2])
            + m[2][0] * (m[0][1] * m[1][2] - m[1][1] * m[0][2]));

        Matrix3 inverse = new Matrix3(0)
        {
            [0, 0] = +(m[1][1] * m[2][2] - m[2][1] * m[1][2]) * oneOverDeterminant,
            [1, 0] = -(m[1][0] * m[2][2] - m[2][0] * m[1][2]) * oneOverDeterminant,
            [2, 0] = +(m[1][0] * m[2][1] - m[2][0] * m[1][1]) * oneOverDeterminant,
            [0, 1] = -(m[0][1] * m[2][2] - m[2][1] * m[0][2]) * oneOverDeterminant,
            [1, 1] = +(m[0][0] * m[2][2] - m[2][0] * m[0][2]) * oneOverDeterminant,
            [2, 1] = -(m[0][0] * m[2][1] - m[2][0] * m[0][1]) * oneOverDeterminant,
            [0, 2] = +(m[0][1] * m[1][2] - m[1][1] * m[0][2]) * oneOverDeterminant,
            [1, 2] = -(m[0][0] * m[1][2] - m[1][0] * m[0][2]) * oneOverDeterminant,
            [2, 2] = +(m[0][0] * m[1][1] - m[1][0] * m[0][1]) * oneOverDeterminant
        };

        return inverse;
    }
}

/// <summary>
/// Represents a three dimensional vector.
/// </summary>
public struct Vector3
{
    public float x;
    public float y;
    public float z;

    public float this[int index]
    {
        get
        {
            return index switch
            {
                0 => x,
                1 => y,
                2 => z,
                _ => throw new Exception("Out of range.")
            };
        }
        set
        {
            switch (index)
            {
                case 0:
                    x = value;
                    break;
                case 1:
                    y = value;
                    break;
                case 2:
                    z = value;
                    break;
                default:
                    throw new Exception("Out of range.");
            }
        }
    }

    public Vector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3(Vector2 xy, float z)
    {
        x = xy.x;
        y = xy.y;
        this.z = z;
    }

    public static Vector3 operator +(Vector3 lhs, Vector3 rhs) =>
        new(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);

    public static Vector3 operator +(Vector3 lhs, float rhs) =>
        new(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs);

    public static Vector3 operator -(Vector3 lhs, Vector3 rhs) =>
        new(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);

    public static Vector3 operator -(Vector3 lhs, float rhs) =>
        new(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);

    public static Vector3 operator *(Vector3 self, float s) =>
        new(self.x * s, self.y * s, self.z * s);

    public static Vector3 operator *(float lhs, Vector3 rhs) =>
        new(rhs.x * lhs, rhs.y * lhs, rhs.z * lhs);

    public static Vector3 operator /(Vector3 lhs, float rhs) =>
        new(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);

    public static Vector3 operator *(Vector3 lhs, Vector3 rhs) =>
        new(rhs.x * lhs.x, rhs.y * lhs.y, rhs.z * lhs.z);

    public float[] ToArray() => new[] {x, y, z};

    #region Comparison

    /// <summary>
    /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
    /// The Difference is detected by the different values
    /// </summary>
    /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object obj) => obj is Vector3 vec && x == vec.x && y == vec.y && z == vec.z;

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="v1">The first Vector.</param>
    /// <param name="v2">The second Vector.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Vector3 v1, Vector3 v2) =>
        v1.Equals(v2);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="v1">The first Vector.</param>
    /// <param name="v2">The second Vector.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Vector3 v1, Vector3 v2) =>
        !v1.Equals(v2);

    #endregion

    public override string ToString() =>
        $"({x}, {y}, {z})";

    public Vector3 Cross(Vector3 rhs) => new(y * rhs.z - rhs.y * z, z * rhs.x - rhs.z * x, x * rhs.y - rhs.x * y);

    public float Dot(Vector3 v) => x * v.x + y * v.y + z * v.z;

    /// <summary>
    /// Map the specified object coordinates (obj.x, obj.y, obj.z) into window coordinates.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <param name="model">The model.</param>
    /// <param name="proj">The proj.</param>
    /// <param name="viewport">The viewport.</param>
    /// <returns></returns>
    public Vector3 Project(Matrix model, Matrix proj, Vector4 viewport)
    {
        Vector4 tmp = new Vector4(this, 1f);
        tmp = model * tmp;
        tmp = proj * tmp;

        tmp /= tmp.w;
        tmp = tmp * 0.5f + 0.5f;
        tmp[0] = tmp[0] * viewport[2] + viewport[0];
        tmp[1] = tmp[1] * viewport[3] + viewport[1];

        return new Vector3(tmp.x, tmp.y, tmp.z);
    }

    /// <summary>
    /// Map the specified window coordinates (win.x, win.y, win.z) into object coordinates.
    /// </summary>
    /// <param name="win">The win.</param>
    /// <param name="model">The model.</param>
    /// <param name="proj">The proj.</param>
    /// <param name="viewport">The viewport.</param>
    /// <returns></returns>
    public Vector3 UnProject(Matrix model, Matrix proj, Vector4 viewport)
    {
        Matrix inverse = (proj * model).Inverse();

        Vector4 tmp = new Vector4(this, 1f);
        tmp.x = (tmp.x - viewport[0]) / viewport[2];
        tmp.y = (tmp.y - viewport[1]) / viewport[3];
        tmp = tmp * 2f - 1f;

        Vector4 obj = inverse * tmp;
        obj /= obj.w;

        return new Vector3(obj.x, obj.y, obj.z);
    }

    public float SquareLength() => x * x + y * y + z * z;
    public float Length() => Sqrt(SquareLength());

    public Vector3 Normalized()
    {
        float l = Length();
        return l != 0 ? this / l : new();
    }

    public void Normalize() => this = Normalized();

    /// <summary>
    /// Takes an "(x,y,z)" string and turns it into a Vector3.
    /// </summary>
    public static Vector3 Parse(string str)
    {
        if (!TryParse(str, out Vector3 v))
            throw new FormatException($"input {str} was in an incorrect format.");
        return v;
    }

    /// <summary>
    /// Takes an "(x,y,z)" string and turns it into a Vector3.
    /// </summary>
    public static bool TryParse(string str, out Vector3 result)
    {
        if (!str.Contains('(') || !str.Contains(')'))
        {
            result = Zero;
            return false;
        }

        string[] split = str.Replace(" ", "").Trim('(').Trim(')').Split(',');
        if (split.Length != 3)
        {
            result = Zero;
            return false;
        }

        result = new();
        return float.TryParse(split[0], out result.x) && float.TryParse(split[1], out result.y) &&
               float.TryParse(split[2], out result.z);
    }

    public static Vector3 Zero => new(0, 0, 0);
    public static Vector3 One => new(1, 1, 1);
    public static Vector3 Up => new(0, 1, 0);
    public static Vector3 Down => new(0, -1, 0);
    public static Vector3 Right => new(1, 0, 0);
    public static Vector3 Left => new(-1, 0, 0);
    public static Vector3 Forward => new(0, 0, 1);
    public static Vector3 Backward => new(0, 0, -1);
}

/// <summary>
/// Represents a four dimensional vector.
/// </summary>
public struct Vector4
{
    public float x;
    public float y;
    public float z;
    public float w;

    public float this[int index]
    {
        get
        {
            return index switch
            {
                0 => x,
                1 => y,
                2 => z,
                3 => w,
                _ => throw new Exception("Out of range.")
            };
        }
        set
        {
            switch (index)
            {
                case 0:
                    x = value;
                    break;
                case 1:
                    y = value;
                    break;
                case 2:
                    z = value;
                    break;
                case 3:
                    w = value;
                    break;
                default:
                    throw new Exception("Out of range.");
            }
        }
    }

    public Vector4(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public Vector4(Vector3 xyz, float w)
    {
        x = xyz.x;
        y = xyz.y;
        z = xyz.z;
        this.w = w;
    }

    public static Vector4 operator +(Vector4 lhs, Vector4 rhs) =>
        new(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);

    public static Vector4 operator +(Vector4 lhs, float rhs) =>
        new(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs, lhs.w + rhs);

    public static Vector4 operator -(Vector4 lhs, float rhs) =>
        new(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs, lhs.w - rhs);

    public static Vector4 operator -(Vector4 lhs, Vector4 rhs) =>
        new(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);

    public static Vector4 operator *(Vector4 self, float s) =>
        new(self.x * s, self.y * s, self.z * s, self.w * s);

    public static Vector4 operator *(float lhs, Vector4 rhs) =>
        new(rhs.x * lhs, rhs.y * lhs, rhs.z * lhs, rhs.w * lhs);

    public static Vector4 operator *(Vector4 lhs, Vector4 rhs) =>
        new(rhs.x * lhs.x, rhs.y * lhs.y, rhs.z * lhs.z, rhs.w * lhs.w);

    public static Vector4 operator /(Vector4 lhs, float rhs) =>
        new(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);

    public float[] ToArray() =>
        new[] {x, y, z, w};

    #region Comparison

    /// <summary>
    /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
    /// The Difference is detected by the different values
    /// </summary>
    /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object obj) =>
        obj is Vector4 vec && x == vec.x && y == vec.y && z == vec.z && w == vec.w;

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="v1">The first Vector.</param>
    /// <param name="v2">The second Vector.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Vector4 v1, Vector4 v2) =>
        v1.Equals(v2);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="v1">The first Vector.</param>
    /// <param name="v2">The second Vector.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Vector4 v1, Vector4 v2) =>
        !v1.Equals(v2);

    #endregion

    public override string ToString() =>
        $"[{x}, {y}, {z}, {w}]";

    public float SquareLength() => x * x + y * y + z * z + w * w;
    public float Length() => Sqrt(SquareLength());

    public float Dot(Vector4 v) => x * v.x + y * v.y + z * v.z + w * v.w;

    public Vector4 Normalized()
    {
        float l = Length();
        return l != 0 ? this / l : new();
    }

    public void Normalize() => this = Normalized();

    /// <summary>
    /// Takes an "(x,y,z)" string and turns it into a Vector3.
    /// </summary>
    public static Vector4 Parse(string str)
    {
        if (!TryParse(str, out Vector4 v))
            throw new FormatException($"input {str} was in an incorrect format.");
        return v;
    }

    /// <summary>
    /// Takes an "(x,y,z)" string and turns it into a Vector3.
    /// </summary>
    public static bool TryParse(string str, out Vector4 result)
    {
        if (!str.Contains('(') || !str.Contains(')'))
        {
            result = Zero;
            return false;
        }

        string[] split = str.Replace(" ", "").Trim('(').Trim(')').Split(',');
        if (split.Length != 4)
        {
            result = Zero;
            return false;
        }

        result = new();
        return float.TryParse(split[0], out result.x) && float.TryParse(split[1], out result.y) &&
               float.TryParse(split[2], out result.z) && float.TryParse(split[3], out result.w);
    }

    public static Vector4 Zero => new(0, 0, 0, 0);
    public static Vector4 One => new(1, 1, 1, 1);
    public static Vector4 Up => new(0, 1, 0, 0);
    public static Vector4 Down => new(0, -1, 0, 0);
    public static Vector4 Right => new(1, 0, 0, 0);
    public static Vector4 Left => new(-1, 0, 0, 0);
    public static Vector4 Forward => new(0, 0, 1, 0);
    public static Vector4 Backward => new(0, 0, -1, 0);

    /// <summary>
    /// Returns a <seealso cref="Color"/>.
    /// x, y, z, w values treated as rgba values between 0 and 1.
    /// </summary>
    /// <returns></returns>
    public Color ToColor() => Color.Float(x, y, z, w);
}
