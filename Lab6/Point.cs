using System.Drawing;

namespace Lab6
{
    class Point
    {
        private double[] coords = new double[] { 0, 0, 0, 1 };

        public double X { get { return coords[0]; } set { coords[0] = value; } }
        public double Y { get { return coords[1]; } set { coords[1] = value; } }
        public double Z { get { return coords[2]; } set { coords[2] = value; } }

        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Apply(Transformation t)
        {
            double[] newCoords = new double[4];
            for (int i = 0; i < 4; ++i)
            {
                newCoords[i] = 0;
                for (int j = 0; j < 4; ++j)
                    newCoords[i] += coords[j] * t.Matrix[j, i];
            }
            coords = newCoords;
        }

        public Point Transform(Transformation t)
        {
            var p = new Point(X, Y, Z);
            p.Apply(t);
            return p;
        }

        public Point FixDisplay(int width, int height)
        {
            var x = (X / coords[3] + 1.0) / 2.0 * width;
            var y = (-Y / coords[3] + 1.0) / 2.0 * height;
            return new Point(x, y, Z);
        }
    }
}
