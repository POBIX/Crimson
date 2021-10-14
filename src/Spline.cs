using System;
using System.Collections.Generic;
using System.Linq;

namespace Crimson
{
    /// <summary>
    /// A spline, based on a Bezier Curve with an arbitrary amount of control points.
    /// </summary>
    public class Spline
    {
        private class Point
        {
            public Vector2 point;
            public Func<float, float> bernstein;

            public int index;
            private static int indexCounter = 0;

            public Point(Vector2 p)
            {
                point = p;
                index = indexCounter++;
            }

            public static implicit operator Vector2(Point p) => p.point;

            private const double Division = 4;
            private static double Breakdown(int b, int s)
            {
                double product = 1 / Division;
                for (double i = 1; i <= s; i++)
                    product *= (b + i) / i;

                return product;
            }

            // these are some optimizations to the binomial coefficient calculation. (expanded formula, did algebra)
            private static double Choose(int n, int k)
            {
                bool b = k > n - k;
                int bigger = b ? k : n - k;
                int smaller = b ? n - k : k;

                return Breakdown(bigger, smaller);
            }

            public void UpdateBernstein()
            {
                int count = indexCounter - 1;
                double c = Choose(count, index);
                bernstein = t => (float)(c * MathF.Pow(t, index) * MathF.Pow(1 - t, count - index) * Division);
            }
        }

        private List<Point> points = new();

        public Vector2[] Path { get; private set; }

        public Spline(params Vector2[] points)
        {
            foreach (Vector2 v in points)
                AddPoint(v);
        }

        private void UpdatePoints()
        {

        }

        public void AddPoint(Vector2 v)
        {
            Point p = new(v);
            points.Add(p);

            foreach (Point n in points)
                n.UpdateBernstein();

            UpdatePoints();
        }

        public void ClearPoints()
        {
            points.Clear();
            UpdatePoints();
        }

        public void SetPoint(int index, Vector2 newValue)
        {
            // points[index] = newValue;
        }
    }

    // public static class C
    // {
    //     public static List<ControlPoint> ps = new();
    //
    //     public static Vector2 P(float t, List<ControlPoint> points)
    //     {
    //         Vector2 sum = Vector2.Zero;
    //         foreach (ControlPoint p in points)
    //             sum += p.point * p.bernstein(t);
    //         return sum;
    //     }
    //
    //     public static Vector2 Pt(float t, List<ControlPoint> points)
    //     {
    //         Vector2 sum = Vector2.Zero;
    //         foreach (ControlPoint p in points)
    //             sum += p.point * p.bernstDer(t);
    //         return sum;
    //     }
    // }

}
