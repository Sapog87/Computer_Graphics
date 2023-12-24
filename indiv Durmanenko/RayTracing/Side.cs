using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RayTracing
{
    public class Side
    {
        public Figure host = null;
        public List<int> points = new List<int>();
        public Pen drawing_pen = new Pen(Color.Black);
        public Point3D normal;

        public Side(Figure h = null)
        {
            host = h;
        }

        public Side(Side s)
        {
            points = new List<int>(s.points);
            host = s.host;
            drawing_pen = s.drawing_pen.Clone() as Pen;
            normal = new Point3D(s.normal);
        }

        public Point3D GetPoint(int index)
        {
            return host?.points[points[index]];
        }

        public static Point3D Norm(Side S)
        {
            if (S.points.Count() < 3)
                return new Point3D(0, 0, 0);
            Point3D U = S.GetPoint(1) - S.GetPoint(0);
            Point3D V = S.GetPoint(S.points.Count - 1) - S.GetPoint(0);
            Point3D normal = U * V;
            return Point3D.Norm(normal);
        }
    }
}
