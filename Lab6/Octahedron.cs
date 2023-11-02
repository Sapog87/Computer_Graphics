using System.Collections.Generic;

namespace Lab6
{
    class Octahedron : Primitive
    {
        public Octahedron(double size)
        {
            Points = new List<Point>
            {
                new Point(-size / 2, 0, 0),
                new Point(0, -size / 2, 0),
                new Point(0, 0, -size / 2),
                new Point(size / 2, 0, 0),
                new Point(0, size / 2, 0),
                new Point(0, 0, size / 2)
            };

            Polygons = new List<Polygon>
            {
                new Polygon(new List<Point> { Points[0], Points[2], Points[4] }),
                new Polygon(new List<Point> { Points[2], Points[4], Points[3] }),
                new Polygon(new List<Point> { Points[4], Points[5], Points[3] }),
                new Polygon(new List<Point> { Points[0], Points[5], Points[4] }),
                new Polygon(new List<Point> { Points[0], Points[5], Points[1] }),
                new Polygon(new List<Point> { Points[5], Points[3], Points[1] }),
                new Polygon(new List<Point> { Points[0], Points[2], Points[1] }),
                new Polygon(new List<Point> { Points[2], Points[1], Points[3] })
            };
        }

        public override bool Condition()
        {
            return Points.Count != 6;
        }
    }
}
