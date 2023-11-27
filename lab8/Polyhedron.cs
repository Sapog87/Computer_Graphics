using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;


namespace lab8
{
    public class Polyhedron
    {
        public List<Point> Points { get; set; }
        public List<List<int>> Polygons { get; set; }
        public Point Center
        {
            get
            {
                Point center = new Point();
                foreach (var v in Points)
                {
                    center.X += v.X;
                    center.Y += v.Y;
                    center.Z += v.Z;
                }
                center.X /= Points.Count;
                center.Y /= Points.Count;
                center.Z /= Points.Count;
                return center;
            }
        }

        public Polyhedron(Tuple<List<Point>, List<List<int>>> data) : this(data.Item1, data.Item2)
        { }

        public Polyhedron(List<Point> vertices, List<List<int>> verges)
        {
            Points = vertices;
            Polygons = verges;
        }

        public Polyhedron(string path)
        {
            var vertices = new List<Point>();
            var verges = new List<List<int>>();
            var info = File.ReadAllLines(path);
            int index = 0;
            while (info[index].Equals("") || !info[index][0].Equals('v'))
                index++;
            while (info[index].Equals("") || info[index][0].Equals('v'))
            {
                var infoPoint = info[index].Split(' ');
                double x = double.Parse(infoPoint[1]);
                double y = double.Parse(infoPoint[2]);
                double z = double.Parse(infoPoint[3]);
                vertices.Add(new Point(x, y, z));
                index++;
            }
            while (info[index].Equals("") || !info[index][0].Equals('f'))
                index++;
            int indexPointSeq = 0;
            while (info[index].Equals("") || info[index][0].Equals('f'))
            {
                var infoPointSeq = info[index].Split(' ');
                var listPoints = new List<int>();
                for (int i = 1; i < infoPointSeq.Length; ++i)
                {
                    int elem;
                    if (int.TryParse(infoPointSeq[i], out elem))
                        listPoints.Add(elem - 1);
                }
                verges.Add(listPoints);
                index++;
                indexPointSeq++;
            }
            Points = vertices;
            Polygons = verges;
        }

        public void Apply(Matrix transformation)
        {
            for (int i = 0; i < Points.Count; ++i)
                Points[i] *= transformation;
        }

        public virtual void Draw(Graphics3D graphics)
        {
            Random r = new Random(256);

            foreach (var polygon in Polygons)
            {
                int k = r.Next(0, 256);
                int k2 = r.Next(0, 256);
                int k3 = r.Next(0, 256);

                for (int i = 1; i < polygon.Count - 1; ++i)
                {
                    var a = new Vertex(Points[polygon[0]], new Point(), Color.FromArgb(k2, k, k3));
                    var b = new Vertex(Points[polygon[i]], new Point(), Color.FromArgb(k2, k, k3));
                    var c = new Vertex(Points[polygon[i + 1]], new Point(), Color.FromArgb(k2, k, k3));
                    graphics.DrawPolygon(a, b, c);
                }
            }
        }

        public virtual void DrawNoColor(Graphics3D graphics)
        {
            foreach (var verge in Polygons)
            {
                Point p1 = Points[verge[0]];
                Point p2 = Points[verge[1]];
                Point p3 = Points[verge[2]];

                double[,] matrix = new double[2, 3];
                matrix[0, 0] = p2.X - p1.X;
                matrix[0, 1] = p2.Y - p1.Y;
                matrix[0, 2] = p2.Z - p1.Z;
                matrix[1, 0] = p3.X - p1.X;
                matrix[1, 1] = p3.Y - p1.Y;
                matrix[1, 2] = p3.Z - p1.Z;

                double ni = matrix[0, 1] * matrix[1, 2] - matrix[0, 2] * matrix[1, 1];
                double nj = matrix[0, 2] * matrix[1, 0] - matrix[0, 0] * matrix[1, 2];
                double nk = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
                double d = -(ni * p1.X + nj * p1.Y + nk * p1.Z);

                Point pp = new Point(p1.X + ni, p1.Y + nj, p1.Z + nk);
                double val1 = ni * pp.X + nj * pp.Y + nk * pp.Z + d;
                double val2 = ni * Center.X + nj * Center.Y + nk * Center.Z + d;

                if (val1 * val2 > 0)
                {
                    ni = -ni;
                    nj = -nj;
                    nk = -nk;
                }

                if (ni * (-graphics.CamPosition.X) + nj * (-graphics.CamPosition.Y) + nk * (-graphics.CamPosition.Z) + ni * p1.X + nj * p1.Y + nk * p1.Z < 0)
                {
                    graphics.DrawPoint(Points[verge[0]], Color.Black);
                    for (int i = 1; i < verge.Count; ++i)
                    {
                        graphics.DrawPoint(Points[verge[i]], Color.Black);
                        graphics.DrawLine(Points[verge[i - 1]], Points[verge[i]]);
                    }
                    graphics.DrawLine(Points[verge[verge.Count - 1]], Points[verge[0]]);
                }
            }
        }

        public void Save(string path)
        {
            string info = "# File Created: " + DateTime.Now.ToString() + "\r\n";
            foreach (var v in Points)
                info += "v " + v.X + " " + v.Y + " " + v.Z + "\r\n";
            info += "# " + Points.Count + " vertices\r\n";
            foreach (var verge in Polygons)
            {
                info += "f ";
                for (int i = 0; i < verge.Count; ++i)
                    info += (verge[i] + 1) + " ";
                info += "\r\n";
            }
            info += "# " + Polygons.Count + " polygons\r\n";
            File.WriteAllText(path, info);
        }
    }
}
