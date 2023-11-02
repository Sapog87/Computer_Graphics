using System.Collections.Generic;
using System.Drawing;

namespace Lab6
{
    class Line : Primitive
    {
        public Line(Point a, Point b)
        {
            Points = new List<Point> { a, b };
        }

        public override void Draw(Graphics g, Transformation projection, int width, int height)
        {
            var c = Points[0].Transform(projection).FixDisplay(width, height);
            var d = Points[1].Transform(projection).FixDisplay(width, height);
            g.DrawLine(Pens.Black, (float)c.X, (float)c.Y, (float)d.X, (float)d.Y);
        }
    }
}
