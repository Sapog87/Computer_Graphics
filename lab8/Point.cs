using System;

namespace lab8
{
    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }

        public Point(double x, double y, double z, double w = 1)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static Point operator *(double x, Point v)
        {
            for (int i = 0; i < 3; ++i) v[i] *= x;
            return v;
        }

        public static Point operator -(Point v)
        {
            return -1 * v;
        }

        public static Point CrossProduct(Point u, Point v)
        {
            return new Point(
                u[1] * v[2] - u[2] * v[1],
                u[2] * v[0] - u[0] * v[2],
                u[0] * v[1] - u[1] * v[0]);
        }

        public static Point operator *(Point v, Matrix m)
        {
            var result = v;
            for (int i = 0; i < 4; ++i)
            {
                result[i] = 0;
                for (int j = 0; j < 4; ++j)
                    result[i] += v[j] * m[j, i];
            }
            return result;
        }

        public double this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return X;
                    case 1: return Y;
                    case 2: return Z;
                    case 3: return W;
                    default: throw new IndexOutOfRangeException("Vertex has only 4 coordinates");
                }
            }
            set
            {
                switch (i)
                {
                    case 0: X = value; break;
                    case 1: Y = value; break;
                    case 2: Z = value; break;
                    case 3: W = value; break;
                    default: throw new IndexOutOfRangeException("Vertex has only 4 coordinates");
                }
            }
        }
    }
}
