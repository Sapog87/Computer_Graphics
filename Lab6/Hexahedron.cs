using System.Collections.Generic;

namespace Lab6
{
    class Hexahedron : Primitive
    {
        public Hexahedron(double size)
        {
            Points = new List<Point>
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

            Polygons = new List<Polygon>
            {
                new Polygon(new List<Point> { Points[0], Points[1], Points[5], Points[3] }),
                new Polygon(new List<Point> { Points[2], Points[6], Points[3], Points[0] }),
                new Polygon(new List<Point> { Points[4], Points[1], Points[0], Points[2] }),
                new Polygon(new List<Point> { Points[7], Points[5], Points[3], Points[6] }),
                new Polygon(new List<Point> { Points[2], Points[4], Points[7], Points[6] }),
                new Polygon(new List<Point> { Points[4], Points[1], Points[5], Points[7] })
            };

        }

        public override bool Condition()
        {
            return Points.Count != 8;
        }
    }
}
