using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace GuroLightning
{
    public class Mesh
    {
        public List<Vector> Vertices { get; set; }
        public List<Polygon> Polygons { get; set; }

        public virtual Vector Center
        {
            get
            {
                Vector center = new Vector();
                foreach (var v in Vertices)
                {
                    center.X += v.X;
                    center.Y += v.Y;
                    center.Z += v.Z;
                }
                center.X /= Vertices.Count;
                center.Y /= Vertices.Count;
                center.Z /= Vertices.Count;
                return center;
            }
        }

        public Mesh(List<Vector> vertices, List<Polygon> polygons)
        {
            Vertices = vertices;
            Polygons = polygons;
        }

        public Mesh()
        {
            Vertices = new List<Vector>();
            Polygons = new List<Polygon>();
        }

        public virtual void Apply(Matrix transformation)
        {
            for (int i = 0; i < Vertices.Count; ++i)
                Vertices[i] *= transformation;
        }

        protected static Color NextColor(Random r)
        {
            return Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
        }

        public virtual void Draw(Graphics3D graphics)
        {
            throw new NotImplementedException();
        }

        public virtual void Save(string path)
        {
            throw new NotImplementedException();
        }
    }
}
