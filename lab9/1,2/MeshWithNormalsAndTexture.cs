using GuroLightning.Properties;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace GuroLightning
{
    public class MeshWithNormalsAndTexture : Mesh
    {
        public List<Vector> Normals { get; set; }
        private List<Vector> uvCoordinates;
        private Bitmap texture = Resources.Texture2;

        public MeshWithNormalsAndTexture(List<Vector> vertices, List<Vector> normals, List<Polygon> poligons, Bitmap texture, List<Vector> textureCoordinates) : base(vertices, poligons)
        {
            this.texture = texture;
            Normals = normals;
            uvCoordinates = textureCoordinates;
        }

        public MeshWithNormalsAndTexture(string path)
        {
            Normals = new List<Vector>();
            uvCoordinates = new List<Vector>();
            var info = File.ReadAllLines(path);

            foreach (var line in info)
            {
                if (line.Length > 2)
                {
                    if (line.Substring(0, 2) == "v ")
                    {
                        var infoV = line.Split(' ').Where(t => t != "").ToArray();
                        double x = double.Parse(infoV[1]);
                        double y = double.Parse(infoV[2]);
                        double z = double.Parse(infoV[3]);
                        Vertices.Add(new Vector(x, y, z));
                    }
                    else if (line.Substring(0, 2) == "vt")
                    {
                        var infoVT = line.Split(' ').Where(t => t != "").ToArray(); ;
                        double x = double.Parse(infoVT[1]);
                        double y = double.Parse(infoVT[2]);
                        double z = double.Parse(infoVT[3]);
                        uvCoordinates.Add(new Vector(x, y, z));
                    }
                    else if (line.Substring(0, 2) == "vn")
                    {
                        var infoVN = line.Split(' ').Where(t => t != "").ToArray(); ;
                        double x = double.Parse(infoVN[1]);
                        double y = double.Parse(infoVN[2]);
                        double z = double.Parse(infoVN[3]);
                        Normals.Add(new Vector(x, y, z));
                    }
                    else if (line.Substring(0, 2) == "f ")
                    {
                        var infoF = line.Split(' ').Where(t => t != "").Skip(1).ToArray();
                        var polygon = new Polygon();
                        foreach (var infoVF in infoF)
                        {
                            var data = infoVF.Split('/');

                            int v = int.Parse(data[0]);
                            int t = int.Parse(data[1]);
                            int n = int.Parse(data[2]);

                            polygon.Vertices.Add(v - 1);
                            polygon.UVCoordinates.Add(t - 1);
                            polygon.Normals.Add(n - 1);
                        }
                        Polygons.Add(polygon);
                    }
                }
            }

            Debug.WriteLine($"polygons: {Polygons.Count}");
        }

        public override void Apply(Matrix transformation)
        {
            var normalTransformation = transformation.Inverse().Transpose();
            for (int i = 0; i < Vertices.Count; ++i)
            {
                Vertices[i] *= transformation;
                Normals[i] = (Normals[i] * normalTransformation).Normalize();
            }
        }

        public override void Draw(Graphics3D graphics)
        {
            graphics.ActiveTexture = texture;

            foreach (var polygon in Polygons)
            {
                if (polygon.Vertices.Count == 3)
                {
                    var a = new Vertex(Vertices[polygon.Vertices[0]], Color.White, Normals[polygon.Normals[0]], uvCoordinates[polygon.UVCoordinates[0]]);
                    var b = new Vertex(Vertices[polygon.Vertices[1]], Color.White, Normals[polygon.Normals[1]], uvCoordinates[polygon.UVCoordinates[1]]);
                    var c = new Vertex(Vertices[polygon.Vertices[2]], Color.White, Normals[polygon.Normals[2]], uvCoordinates[polygon.UVCoordinates[2]]);

                    graphics.DrawTriangle(a, b, c);
                }
                else if (polygon.Vertices.Count == 4)
                {
                    var a = new Vertex(Vertices[polygon.Vertices[0]], Color.White, Normals[polygon.Normals[0]], uvCoordinates[polygon.UVCoordinates[0]]);
                    var b = new Vertex(Vertices[polygon.Vertices[1]], Color.White, Normals[polygon.Normals[1]], uvCoordinates[polygon.UVCoordinates[1]]);
                    var c = new Vertex(Vertices[polygon.Vertices[3]], Color.White, Normals[polygon.Normals[3]], uvCoordinates[polygon.UVCoordinates[3]]);

                    graphics.DrawTriangle(a, b, c);

                    b = new Vertex(Vertices[polygon.Vertices[1]], Color.White, Normals[polygon.Normals[1]], uvCoordinates[polygon.UVCoordinates[1]]);
                    c = new Vertex(Vertices[polygon.Vertices[2]], Color.White, Normals[polygon.Normals[2]], uvCoordinates[polygon.UVCoordinates[2]]);
                    a = new Vertex(Vertices[polygon.Vertices[3]], Color.White, Normals[polygon.Normals[3]], uvCoordinates[polygon.UVCoordinates[3]]);

                    graphics.DrawTriangle(a, b, c);
                }
            }
        }

        public override void Save(string path)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var vertex in Vertices)
            {
                stringBuilder.Append("v ");
                stringBuilder.AppendLine(vertex.ToString());
            }

            foreach (var normal in Normals)
            {
                stringBuilder.Append("vn ");
                stringBuilder.AppendLine(normal.ToString());
            }

            foreach (var uv in uvCoordinates)
            {
                stringBuilder.Append("vt ");
                stringBuilder.AppendLine(uv.ToString());
            }

            foreach (var polygon in Polygons)
            {
                stringBuilder.Append("f ");
                stringBuilder.AppendLine(polygon.ToString());
            }

            File.WriteAllText(path, stringBuilder.ToString());
        }
    }
}
