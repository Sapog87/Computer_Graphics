using System;
using System.Collections.Generic;
using System.Windows.Shapes;

namespace lab8
{
    public class Hexahedron : Polyhedron
    {
        public Hexahedron(double size)
            : base(Construct(size))
        {
        }

        private static Tuple<Point[], int[][]> Construct(double size)
        {
            var vertices = new Point[8]
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
            var indices = new int[][]
            {
                new int[] { 1, 5,3 },
                new int[] { 0, 1,3 },
                new int[] { 6, 3,0 },
                new int[] { 2, 6,0 },
                new int[] { 1, 0,2 },
                new int[] { 4, 1,2 },
                new int[] { 5, 3,6 },
                new int[] { 7, 5,6 },
                new int[] { 4, 7,6 },
                new int[] { 2, 4,6 },
                new int[] { 1, 5,7 },
                new int[] { 4, 1,7 }
            };

            return new Tuple<Point[], int[][]>(vertices, indices);
        }
    }
}