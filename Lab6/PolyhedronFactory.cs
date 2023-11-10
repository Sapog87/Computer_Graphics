using AngouriMath;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Windows.Forms.VisualStyles;

namespace Lab6
{
    class PolyhedronFactory
    {
        public static Polyhedron Hexahedron(double size)
        {
            List<Point> points = new List<Point>
            {
                new Point(-size / 2, -size / 2, -size / 2),
                new Point(-size / 2, -size / 2, size / 2),
                new Point(-size / 2, size / 2, -size / 2),
                new Point(size / 2, -size / 2, -size / 2),
                new Point(-size / 2, size / 2, size / 2),
                new Point(size / 2, -size / 2, size / 2),
                new Point(size / 2, size / 2, -size / 2),
                new Point(size / 2, size / 2, size / 2)
            };

            List<Polygon> polygons = new List<Polygon>
            {
                new Polygon(new List<Point> { points[0], points[1], points[5], points[3] }),
                new Polygon(new List<Point> { points[2], points[6], points[3], points[0] }),
                new Polygon(new List<Point> { points[4], points[1], points[0], points[2] }),
                new Polygon(new List<Point> { points[7], points[5], points[3], points[6] }),
                new Polygon(new List<Point> { points[2], points[4], points[7], points[6] }),
                new Polygon(new List<Point> { points[4], points[1], points[5], points[7] })
            };

            return new Polyhedron(points, polygons);
        }

        public static Polyhedron Tetrahedron(double size)
        {
            double h = Math.Sqrt(2.0 / 3.0) * size;

            var points = new List<Point>
            {
                new Point(-size / 2.0, 0.0,        h / 3.0),
                new Point(        0.0, 0.0, -h * 2.0 / 3.0),
                new Point( size / 2.0, 0.0,        h / 3.0),
                new Point(        0.0,   h,            0.0)
            };

            var polygons = new List<Polygon>
            {
                new Polygon(new List<Point> { points[0], points[1], points[2] }),
                new Polygon(new List<Point> { points[1], points[3], points[0] }),
                new Polygon(new List<Point> { points[2], points[3], points[1] }),
                new Polygon(new List<Point> { points[0], points[3], points[2] })
            };

            return new Polyhedron(points, polygons);
        }

        public static Polyhedron Octahedron(double size)
        {
            var points = new List<Point>
            {
                new Point(-size / 2, 0, 0),
                new Point(0, -size / 2, 0),
                new Point(0, 0, -size / 2),
                new Point(size / 2, 0, 0),
                new Point(0, size / 2, 0),
                new Point(0, 0, size / 2)
            };

            var polygons = new List<Polygon>
            {
                new Polygon(new List<Point> { points[0], points[2], points[4] }),
                new Polygon(new List<Point> { points[2], points[4], points[3] }),
                new Polygon(new List<Point> { points[4], points[5], points[3] }),
                new Polygon(new List<Point> { points[0], points[5], points[4] }),
                new Polygon(new List<Point> { points[0], points[5], points[1] }),
                new Polygon(new List<Point> { points[5], points[3], points[1] }),
                new Polygon(new List<Point> { points[0], points[2], points[1] }),
                new Polygon(new List<Point> { points[2], points[1], points[3] })
            };

            return new Polyhedron(points, polygons);
        }

        public static Polyhedron RotationFigure(List<Point> basePoints, int axis, int patritions)
        {
            var points = new List<Point>(basePoints);
            List<Point> rotatedPoints = new List<Point>();
            Func<double, Transformation> rotation;

            switch (axis)
            {
                case 0:
                    rotation = Transformation.RotateX;
                    break;
                case 1:
                    rotation = Transformation.RotateY;
                    break;
                case 2:
                    rotation = Transformation.RotateZ;
                    break;
                default:
                    throw new ArgumentException("Invalid axis");
            }

            for (int i = 1; i < patritions; ++i)
            {
                double angle = 2 * Math.PI * i / (patritions);
                foreach (var point in basePoints)
                    rotatedPoints.Add(point.Transform(rotation(angle)));
                points.AddRange(rotatedPoints);
                rotatedPoints.Clear();
            }

            var n = basePoints.Count;
            var polygons = new List<Polygon>();
            for (int i = 0; i < patritions; ++i)
                for (int j = 0; j < n - 1; ++j)
                    polygons.Add(new Polygon(new List<Point> {
                        points[i * n + j], points[(i + 1) % (patritions) * n + j],
                        points[(i + 1) % (patritions) * n + j + 1], points[i * n + j + 1] }));

            return new Polyhedron(points, polygons);
        }

        public static Polyhedron Function(double x0, double x1, double y0, double y1, double xStep, double yStep, string function)
        {
            var points = new List<Point>();
            var polygons = new List<Polygon>();

            int xIter = (int)((x1 - x0) / xStep);
            int yIter = (int)((y1 - y0) / yStep);

            var prevPolygonPoints = new List<Point>();
            for (double y = y0; y <= y1 + 0.001; y += yStep)
            {
                double z = FunctionValue(x0, y, function);
                var point = new Point(x0, y, z);
                prevPolygonPoints.Add(point);
                points.Add(point);
            }
            prevPolygonPoints.Reverse();

            for (double x = x0 + xStep; x <= x1 + 0.001; x += xStep)
            {
                var polygonPoints = new List<Point>();
                for (double y = y0; y <= y1 + 0.001; y += yStep)
                {
                    double z = FunctionValue(x, y, function);
                    var point = new Point(x, y, z);
                    points.Add(point);
                    polygonPoints.Add(point);
                }
                var t = new List<Point>(polygonPoints);
                t.Reverse();
                polygonPoints.AddRange(prevPolygonPoints);
                polygons.Add(new Polygon(polygonPoints));
                prevPolygonPoints = t;
            }

            return new Polyhedron(points, polygons);
        }

        private static double FunctionValue(double x, double y, string function)
        {
            Entity expr = function.Replace("x", x.ToString()).Replace("y", y.ToString());
            return (double)expr.EvalNumerical();
        }
    }
}