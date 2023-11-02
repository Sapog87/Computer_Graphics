using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace Lab6
{
    class Tetrahedron : Primitive
    {
        public Tetrahedron(double size)
        {
            double h = Math.Sqrt(2.0 / 3.0) * size;
            Points = new List<Point>
            {
                new Point(-size / 2.0, 0.0,        h / 3.0),
                new Point(        0.0, 0.0, -h * 2.0 / 3.0),
                new Point( size / 2.0, 0.0,        h / 3.0),
                new Point(        0.0,   h,            0.0)
            };

            Polygons = new List<Polygon>
            {
                new Polygon(new List<Point> { Points[0], Points[1], Points[2] }),
                new Polygon(new List<Point> { Points[1], Points[3], Points[0] }),
                new Polygon(new List<Point> { Points[2], Points[3], Points[1] }),
                new Polygon(new List<Point> { Points[0], Points[3], Points[2] })
            };
        }
    }
}
