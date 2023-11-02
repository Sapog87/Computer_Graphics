using System.Collections.Generic;
using System.Drawing;

namespace Lab6
{
    class Polygon
    {
        public List<Point> Points { get; set; }

        public Polygon(List<Point> points)
        {
            Points = points;
        }

        public void Draw(Graphics g, Transformation projection, int width, int height)
        {
            for (int i = 0; i < Points.Count - 1; ++i)
            {
                new Line(Points[i], Points[i + 1]).Draw(g, projection, width, height);
            }
            new Line(Points[Points.Count - 1], Points[0]).Draw(g, projection, width, height);
        }
    }
}
