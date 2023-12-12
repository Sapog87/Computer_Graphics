using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuroLightning
{
    public class Polygon
    {
        public List<int> Vertices { get; set; }
        public List<int> Normals { get; set; }
        public List<int> UVCoordinates { get; set; }
        public Polygon()
        {
            Vertices = new List<int>();
            Normals = new List<int>();
            UVCoordinates = new List<int>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Vertices.Count; i++)
            {
                sb.Append(Vertices[i] + 1);
                sb.Append('/');
                sb.Append(UVCoordinates[i] + 1);
                sb.Append('/');
                sb.Append(Normals[i] + 1);
                if (i != Vertices.Count - 1) sb.Append(' ');
            }

            return sb.ToString();
        }
    }
}
