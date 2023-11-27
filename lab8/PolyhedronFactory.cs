using AngouriMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Shapes;

namespace lab8
{
    class PolyhedronFactory
    {
        public static Polyhedron Hexahedron(double size)
        {
            var vertices = new List<Point>
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

            var indices = new List<List<int>>
            {
                new List<int> { 1, 5,3 },
                new List<int> { 0, 1,3 },
                new List<int> { 6, 3,0 },
                new List<int> { 2, 6,0 },
                new List<int> { 1, 0,2 },
                new List<int> { 4, 1,2 },
                new List<int> { 5, 3,6 },
                new List<int> { 7, 5,6 },
                new List<int> { 4, 7,6 },
                new List<int> { 2, 4,6 },
                new List<int> { 1, 5,7 },
                new List<int> { 4, 1,7 }
            };

            return new Polyhedron(vertices, indices);
        }

        public static Polyhedron Tetrahedron(double size)
        {
            double h = Math.Sqrt(2.0 / 3.0) * size;
            var vertices = new List<Point>
            {
                new Point(-size / 2, 0, h / 3),
                new Point(0, 0, -h * 2 / 3),
                new Point(size / 2, 0, h / 3),
                new Point(0, h, 0)
            };
            var indices = new List<List<int>>
            {
                new List<int> { 0, 1, 2 },
                new List<int> { 1, 3, 0 },
                new List<int> { 0, 3, 2 },
                new List<int> { 2, 3, 1 }
            };

            return new Polyhedron(vertices, indices);
        }

        public static Polyhedron Icosahedron(double size)
        {
            var vertices = new Point[12];
            var indices = new List<int>[20];
            double R = (size * Math.Sqrt(2.0 * (5.0 + Math.Sqrt(5.0)))) / 4;
            double r = (size * Math.Sqrt(3.0) * (3.0 + Math.Sqrt(5.0))) / 12;
            for (int i = 0; i < 5; ++i)
            {
                vertices[2 * i] = new Point(
                    r * Math.Cos(2 * Math.PI / 5 * i),
                    R / 2,
                    r * Math.Sin(2 * Math.PI / 5 * i));
                vertices[2 * i + 1] = new Point(
                    r * Math.Cos(2 * Math.PI / 5 * i + 2 * Math.PI / 10),
                    -R / 2,
                    r * Math.Sin(2 * Math.PI / 5 * i + 2 * Math.PI / 10));
            }
            vertices[10] = new Point(0, R, 0);
            vertices[11] = new Point(0, -R, 0);
            for (int i = 0; i < 10; ++i)
                indices[i] = new List<int> { i, (i + 1) % 10, (i + 2) % 10 };
            for (int i = 0; i < 5; ++i)
            {
                indices[10 + 2 * i] = new List<int> { 2 * i, 10, (2 * (i + 1)) % 10 };
                indices[10 + 2 * i + 1] = new List<int> { 2 * i + 1, 11, (2 * (i + 1) + 1) % 10 };
            }
            return new Polyhedron(vertices.ToList(), indices.ToList());
        }

        /*public static Polyhedron RotationFigure(List<Point> basePoints, int axis, int patritions)
        {
            var points = new List<Point>(basePoints);
            List<Point> rotatedPoints = new List<Point>();
            Func<double, Matrix> rotation;

            switch (axis)
            {
                case 0:
                    rotation = Transformations.RotateX;
                    break;
                case 1:
                    rotation = Transformations.RotateY;
                    break;
                case 2:
                    rotation = Transformations.RotateZ;
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
        }*/

        public static Polyhedron Function(double x0, double x1, double y0, double y1, double xStep, double yStep, string function)
        {
            if (function == null || function == "")
                return new Polyhedron(new List<Point>(), new List<List<int>>());

            var points = new List<Point>();
            var polygons = new List<List<int>>();

            var prevPolygonPoints = new List<int>();
            int k = 0;
            for (double y = y0; y <= y1 + 0.001; y += yStep)
            {
                double z = FunctionValue(x0, y, function);
                var point = new Point(x0, y, z);
                prevPolygonPoints.Add(k++);
                points.Add(point);
            }

            for (double x = x0 + xStep; x <= x1 + 0.001; x += xStep)
            {
                var polygonPoints = new List<int>();
                for (double y = y0; y <= y1 + 0.001; y += yStep)
                {
                    double z = FunctionValue(x, y, function);
                    var point = new Point(x, y, z);
                    points.Add(point);
                    polygonPoints.Add(k++);
                }

                for (int i = 1; i < polygonPoints.Count; i++)
                {
                    var pol1 = new List<int>
                    {
                        prevPolygonPoints[i],
                        polygonPoints[i],
                        polygonPoints[i-1],
                    };
                    polygons.Add(pol1);
                    var pol2 = new List<int>
                    {
                        prevPolygonPoints[i],
                        polygonPoints[i-1],
                        prevPolygonPoints[i-1],
                    };
                    polygons.Add(pol2);
                }
                prevPolygonPoints = new List<int>(polygonPoints);
            }

            return new Polyhedron(points, polygons);
        }

        private static double FunctionValue(double x, double y, string function)
        {
            Entity expr = function.Replace("x", x.ToString()).Replace("y", y.ToString()).Replace(",", ".");
            return (double)expr.EvalNumerical();
        }
    }
}
