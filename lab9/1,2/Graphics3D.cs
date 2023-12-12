using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace GuroLightning
{
    public class Graphics3D
    {
        public Bitmap ActiveTexture { get; set; }

        public enum Face { Clockwise, Counterclockwise, None }

        public Face CullFace { get; set; } = Face.Clockwise;

        public IList<LightSource> LightSources { get; set; } =
            new LightSource[]
            {
                new LightSource(new Vector(0, 1, 0), Color.White)
            };

        private Bitmap colorBuffer;

        private BitmapData bitmapData;

        private SceneView sceneView;

        private Matrix viewProjection;

        private double Width { get { return sceneView.Width; } }
        private double Height { get { return sceneView.Height; } }

        public Graphics3D(SceneView sceneView)
        {
            this.sceneView = sceneView;
            Resize();
        }

        public void Resize()
        {
            colorBuffer = new Bitmap((int)Width + 1, (int)Height + 1, PixelFormat.Format24bppRgb);
        }

        public void StartDrawing()
        {
            using (var g = Graphics.FromImage(colorBuffer))
                g.FillRectangle(Brushes.Gray, 0, 0, (int)Width, (int)Height);

            bitmapData = colorBuffer.LockBits(
                new Rectangle(0, 0, colorBuffer.Width, colorBuffer.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            viewProjection = sceneView.Camera.ViewProjection;
        }

        public Bitmap FinishDrawing()
        {
            colorBuffer.UnlockBits(bitmapData);
            bitmapData = null;
            return colorBuffer;
        }

        private unsafe void SetPixel(int x, int y, double z, Color color)
        {
            var pointer = (byte*)bitmapData.Scan0.ToPointer();
            pointer[y * bitmapData.Stride + 3 * x + 0] = color.B;
            pointer[y * bitmapData.Stride + 3 * x + 1] = color.G;
            pointer[y * bitmapData.Stride + 3 * x + 2] = color.R;
        }

        private Vector SpaceToClip(Vector v)
        {
            return v * viewProjection;
        }

        private Vector ClipToScreen(Vector v)
        {
            return NormalizedToScreen(Normalize(v));
        }

        private Vector Normalize(Vector v)
        {
            return new Vector(v.X / v.W, v.Y / v.W, v.Z / v.W);
        }

        private Vector NormalizedToScreen(Vector v)
        {
            return new Vector(
                (v.X + 1) / 2 * Width,
                (-v.Y + 1) / 2 * Height,
                v.Z);
        }

        private static double Interpolate(double x0, double x1, double f)
        {
            return x0 + (x1 - x0) * f;
        }

        private static long Interpolate(long x0, long x1, double f)
        {
            return x0 + (long)((x1 - x0) * f);
        }

        private static Color Interpolate(Color a, Color b, double f)
        {
            var R = Interpolate(a.R, b.R, f);
            var G = Interpolate(a.G, b.G, f);
            var B = Interpolate(a.B, b.B, f);
            return Color.FromArgb((byte)R, (byte)G, (byte)B);
        }

        private static Vector Interpolate(Vector a, Vector b, double f)
        {
            return new Vector(
                Interpolate(a.X, b.X, f),
                Interpolate(a.Y, b.Y, f),
                Interpolate(a.Z, b.Z, f),
                Interpolate(a.W, b.W, f));
        }

        private static Vertex Interpolate(Vertex a, Vertex b, double f)
        {
            return new Vertex(
                Interpolate(a.Coordinate, b.Coordinate, f),
                Interpolate(a.Color, b.Color, f),
                Interpolate(a.Normal, b.Normal, f),
                Interpolate(a.UVCoordinate, b.UVCoordinate, f));
        }

        private double NormalizedAdd(double x, double y)
        {
            return x + y - x * y;
        }

        private Color NormalizedAdd(Color a, Color b)
        {
            return Color.FromArgb(
                (int)(255 * NormalizedAdd(a.R / 255.0, b.R / 255.0)),
                (int)(255 * NormalizedAdd(a.G / 255.0, b.G / 255.0)),
                (int)(255 * NormalizedAdd(a.B / 255.0, b.B / 255.0)));
        }

        private Color CalculateLight(Vertex a, LightSource light)
        {
            var i = Math.Max(0, Vector.DotProduct(a.Normal, (light.Coordinate - a.Coordinate).Normalize()));
            return Color.FromArgb(
                (byte)(a.Color.R * light.Color.R / 255.0 * i),
                (byte)(a.Color.G * light.Color.G / 255.0 * i),
                (byte)(a.Color.B * light.Color.B / 255.0 * i));
        }

        public void DrawTriangle(Vertex a, Vertex b, Vertex c)
        {
            foreach (var lightSource in LightSources)
            {
                a.Color = CalculateLight(a, lightSource);
                b.Color = CalculateLight(b, lightSource);
                c.Color = CalculateLight(c, lightSource);
            }

            a.Coordinate = SpaceToClip(a.Coordinate);
            b.Coordinate = SpaceToClip(b.Coordinate);
            c.Coordinate = SpaceToClip(c.Coordinate);

            a.Coordinate = ClipToScreen(a.Coordinate);
            b.Coordinate = ClipToScreen(b.Coordinate);
            c.Coordinate = ClipToScreen(c.Coordinate);


            DrawTriangleInternal(a, b, c);
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }

        private void DrawTriangleInternal(Vertex a, Vertex b, Vertex c)
        {
            if (Face.None != CullFace)
            {
                var u = b.Coordinate - a.Coordinate;
                var v = c.Coordinate - a.Coordinate;

                if (Face.Counterclockwise == CullFace)
                    Swap(ref u, ref v);

                if (Vector.AngleBet(new Vector(0, 0, 1), Vector.CrossProduct(u, v)) > Math.PI / 2)
                    return;
            }

            if (a.Coordinate.Y > b.Coordinate.Y)
                Swap(ref a, ref b);

            if (a.Coordinate.Y > c.Coordinate.Y)
                Swap(ref a, ref c);

            if (b.Coordinate.Y > c.Coordinate.Y)
                Swap(ref b, ref c);

            for (double y = Math.Ceiling(a.Coordinate.Y); y < c.Coordinate.Y; ++y)
            {
                bool topHalf = y < b.Coordinate.Y;

                var a0 = a;
                var a1 = c;
                var left = Interpolate(a0, a1, (y - a0.Coordinate.Y) / (a1.Coordinate.Y - a0.Coordinate.Y));

                var b0 = topHalf ? a : b;
                var b1 = topHalf ? b : c;
                var right = Interpolate(b0, b1, (y - b0.Coordinate.Y) / (b1.Coordinate.Y - b0.Coordinate.Y));

                if (left.Coordinate.X > right.Coordinate.X)
                    Swap(ref left, ref right);

                for (double x = Math.Ceiling(left.Coordinate.X); x < right.Coordinate.X; ++x)
                {
                    if (x > 0 && y > 0 && x < Width && y < Height)
                    {
                        var point = Interpolate(left, right, (x - left.Coordinate.X) / (right.Coordinate.X - left.Coordinate.X));
                        int x1 = (int)(point.UVCoordinate.X * (ActiveTexture.Width - 1));
                        int y1 = (int)(point.UVCoordinate.Y * (ActiveTexture.Height - 1));

                        Color textureColor = ActiveTexture.GetPixel(x1, y1);

                        Color color = Color.FromArgb(
                            point.Color.A,
                            Math.Min(point.Color.R,
                            textureColor.R),
                            Math.Min(point.Color.G, textureColor.G),
                            Math.Min(point.Color.B, textureColor.B)
                        );

                        SetPixel(
                            (int)x,
                            (int)y,
                            point.Coordinate.Z,
                            color
                        );
                    }
                }
            }
        }
    }
}
