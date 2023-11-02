using System;

namespace Lab6
{
    class Transformation
    {
        public double[,] Matrix { get; set; }

        public Transformation(double[,] matrix)
        {
            Matrix = matrix;
        }

        public static Transformation RotateX(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            return new Transformation(
                new double[,]
                {
                    { 1.0, 0.0,  0.0, 0.0 },
                    { 0.0, cos, -sin, 0.0 },
                    { 0.0, sin,  cos, 0.0},
                    { 0.0, 0.0,  0.0, 1.0 }
                });
        }

        public static Transformation RotateY(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            return new Transformation(
                new double[,]
                {
                    {  cos, 0, sin, 0 },
                    {    0, 1,   0, 0 },
                    { -sin, 0, cos, 0 },
                    {    0, 0,   0, 1 }
                });
        }

        public static Transformation RotateZ(double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            return new Transformation(
                new double[,]
                {
                    { cos, -sin, 0.0, 0.0 },
                    { sin,  cos, 0.0, 0.0 },
                    { 0.0,  0.0, 1.0, 0.0 },
                    { 0.0,  0.0, 0.0, 1.0 }
                });
        }

        public static Transformation RotateLine(Line line, double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            double l = Math.Sign(line.Points[1].X - line.Points[0].X);
            double m = Math.Sign(line.Points[1].Y - line.Points[0].Y);
            double n = Math.Sign(line.Points[1].Z - line.Points[0].Z);
            double length = Math.Sqrt(l * l + m * m + n * n);
            l /= length; m /= length; n /= length;
            return new Transformation(
                new double[,]
                {
                   {   l * l + cos * (1 - l * l),   l * (1 - cos) * m + n * sin,   l * (1 - cos) * n - m * sin,   0.0 },
                   { l * (1 - cos) * m - n * sin,     m * m + cos * (1 - m * m),   m * (1 - cos) * n + l * sin,   0.0 },
                   { l * (1 - cos) * n + m * sin,   m * (1 - cos) * n - l * sin,     n * n + cos * (1 - n * n),   0.0 },
                   {                         0.0,                           0.0,                           0.0,   1.0 }
                });

        }

        public static Transformation Scale(double fx, double fy, double fz)
        {
            return new Transformation(
                new double[,] {
                    {  fx, 0.0, 0.0, 0.0 },
                    { 0.0,  fy, 0.0, 0.0 },
                    { 0.0, 0.0,  fz, 0.0 },
                    { 0.0, 0.0, 0.0, 1.0 }
                });
        }

        public static Transformation Translate(double dx, double dy, double dz)
        {
            return new Transformation(
                new double[,]
                {
                    { 1.0, 0.0, 0.0, 0.0 },
                    { 0.0, 1.0, 0.0, 0.0 },
                    { 0.0, 0.0, 1.0, 0.0 },
                    {  dx,  dy,  dz, 1.0 },
                });
        }

        public static Transformation Identity()
        {
            return new Transformation(
                new double[,] {
                    { 1.0, 0.0, 0.0, 0.0 },
                    { 0.0, 1.0, 0.0, 0.0 },
                    { 0.0, 0.0, 1.0, 0.0 },
                    { 0.0, 0.0, 0.0, 1.0 }
                });
        }


        public static Transformation ReflectX()
        {
            return Scale(1.0, -1.0, -1.0);
        }

        public static Transformation ReflectY()
        {
            return Scale(-1.0, 1.0, -1.0);
        }

        public static Transformation ReflectZ()
        {
            return Scale(-1.0, -1.0, 1.0);
        }

        public static Transformation OrthographicXYProjection()
        {
            return Identity();
        }

        public static Transformation OrthographicXZProjection()
        {
            return Identity() * RotateX(-Math.PI / 2.0);
        }

        public static Transformation OrthographicYZProjection()
        {
            return Identity() * RotateY(Math.PI / 2.0);
        }

        public static Transformation IsometricProjection()
        {
            return Identity() * RotateY(Math.PI / 3.0) * RotateX(-Math.PI / 12.0);
        }

        public static Transformation PerspectiveProjection()
        {
            return new Transformation(
                new double[,] {
                    { 1.0, 0.0, 0.0, 0.0 },
                    { 0.0, 1.0, 0.0, 0.0 },
                    { 0.0, 0.0, 0.0, 2.0 },
                    { 0.0, 0.0, 0.0, 1.0 }
                });
        }

        public static Transformation operator *(Transformation t1, Transformation t2)
        {
            double[,] matrix = new double[4, 4];
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    matrix[i, j] = 0;
                    for (int k = 0; k < 4; ++k)
                        matrix[i, j] += t1.Matrix[i, k] * t2.Matrix[k, j];
                }
            }
            return new Transformation(matrix);
        }
    }
}
