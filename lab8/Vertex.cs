using System.Drawing;

namespace lab8
{
    public class Vertex
    {
        public Point Coordinate { get; set; }
        public Point Normal { get; set; }
        public Color Color { get; set; }

        public Vertex() { }

        public Vertex(Point coordinate, Point normal, Color color)
        {
            Coordinate = coordinate;
            Normal = normal;
            Color = color;
        }
    }
}
