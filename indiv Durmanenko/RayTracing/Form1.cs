using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RayTracing
{
    public partial class Form1 : Form
    {
        public List<Figure> figures;
        public List<Light> lights;
        public Color[,] colorPixels;                    // цвета пикселей для отображения на pictureBox
        public Point3D[,] pixels;
        public Point3D focus;
        public Point3D upLeft, upRight, downLeft, downRight;
        public int w, h;

        public Form1()
        {
            InitializeComponent();

            figures = new List<Figure>();
            lights = new List<Light>();
            focus = new Point3D();
            upLeft = new Point3D();
            upRight = new Point3D();
            downLeft = new Point3D();
            downRight = new Point3D();

            w = pictureBox1.Width;
            h = pictureBox1.Height;
            pictureBox1.Image = new Bitmap(w, h);
        }

        public void BuildScene()
        {
            Hexahedron room = new Hexahedron(10);
            upLeft = room.sides[0].GetPoint(0);
            upRight = room.sides[0].GetPoint(1);
            downRight = room.sides[0].GetPoint(2);
            downLeft = room.sides[0].GetPoint(3);

            Point3D normal = Side.Norm(room.sides[0]);
            Point3D center = (upLeft + upRight + downLeft + downRight) / 4;
            focus = center + normal * 10;

            room.SetPen(new Pen(Color.White));

            room.isRoom = true;

            room.sides[0].drawing_pen = new Pen(Color.White);
            room.backWallMaterial = backWallSpecularCB.Checked ? Materials.WallSpecular : Materials.WallDefault;

            room.sides[1].drawing_pen = new Pen(Color.Yellow);
            room.frontWallMaterial = frontWallSpecularCB.Checked ? Materials.WallSpecular : Materials.WallDefault;

            room.sides[2].drawing_pen = new Pen(Color.Green);
            room.rightWallMaterial = rightWallSpecularCB.Checked ? Materials.WallSpecular : Materials.WallDefault;

            room.sides[3].drawing_pen = new Pen(Color.Blue);
            room.leftWallMaterial = leftWallSpecularCB.Checked ? Materials.WallSpecular : Materials.WallDefault;

            room.upWallMaterial = upWallSpecularCB.Checked ? Materials.WallSpecular : Materials.WallDefault;
            room.downWallMaterial = downWallSpecularCB.Checked ? Materials.WallSpecular : Materials.WallDefault;

            figures.Add(room);

            Light l1 = new Light(new Point3D(0f, 1f, 4.9f), new Point3D(1f, 1f, 1f));
            lights.Add(l1);
            if (twoLightsCB.Checked)
            {
                Light l2 = new Light(new Point3D(0f, 4f, 4.9f), new Point3D(1f, 1f, 1f));
                lights.Add(l2);
            }

            Hexahedron cube1 = new Hexahedron(1.7f);
            cube1.offset(-0.5f, 3.0f, -4.0f);
            cube1.rotate_around(60, "CZ");
            cube1.SetPen(new Pen(Color.Green));
            cube1.material = refractCubeCB.Checked ? Materials.FigureRefractable : Materials.FigureDefault;
            figures.Add(cube1);

            Hexahedron cube2 = new Hexahedron(2.6f);
            cube2.offset(-2.4f, 1.0f, -3.8f);
            cube2.rotate_around(45, "CZ");
            cube2.SetPen(new Pen(Color.Blue));
            cube2.material = cubeSpecularCB.Checked ? Materials.FigureSpecular : Materials.FigureDefault;
            figures.Add(cube2);

            Sphere sphere1 = new Sphere(new Point3D(2.5f, 2f, -3.4f), 2.0f);
            sphere1.SetPen(new Pen(Color.Red));
            sphere1.material = refractSphereCB.Checked ? Materials.FigureRefractable : Materials.FigureDefault;
            figures.Add(sphere1);

            Sphere sphere2 = new Sphere(new Point3D(-2.2f, 1.6f, -1.4f), 0.9f);
            sphere2.SetPen(new Pen(Color.Purple));
            sphere2.material = sphereSpecularCB.Checked ? Materials.FigureSpecular : Materials.FigureDefault;
            figures.Add(sphere2);
        }

        public void Clear()
        {
            figures.Clear();
            lights.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
            BuildScene();
            RunRayTrace();
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    (pictureBox1.Image as Bitmap).SetPixel(i, j, colorPixels[i, j]);

            pictureBox1.Invalidate();
        }

        public void RunRayTrace()
        {
            GetPixels();
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    Ray ray = new Ray(focus, pixels[i, j])
                    {
                        start = new Point3D(pixels[i, j])
                    };
                    Point3D clr = RayTrace(ray, 10, 1);
                    if (clr.x > 1.0f) clr.x = 1.0f;
                    if (clr.y > 1.0f) clr.y = 1.0f;
                    if (clr.z > 1.0f) clr.z = 1.0f;

                    colorPixels[i, j] = Color.FromArgb((int)(255 * clr.x), (int)(255 * clr.y), (int)(255 * clr.z));
                }
            }
        }

        // получение всех пикселей сцены
        public void GetPixels()
        {
            pixels = new Point3D[w, h];
            colorPixels = new Color[w, h];
            Point3D step_up = (upRight - upLeft) / (w - 1);
            Point3D step_down = (downRight - downLeft) / (w - 1);

            Point3D up = new Point3D(upLeft);
            Point3D down = new Point3D(downLeft);

            for (int i = 0; i < w; i++)
            {
                Point3D step_y = (up - down) / (h - 1);
                Point3D d = new Point3D(down);
                for (int j = 0; j < h; j++)
                {
                    pixels[i, j] = d;
                    d += step_y;
                }
                up += step_up;
                down += step_down;
            }
        }

        // видима ли точка пересечения луча с фигурой из источника света
        public bool IsPointVisible(Point3D lightPoint, Point3D hitPoint)
        {
            float max_t = (lightPoint - hitPoint).Length();     // позиция источника света на луче
            Ray r = new Ray(hitPoint, lightPoint);

            foreach (Figure fig in figures)
                if (fig.Intersects(r, out float t, out _))
                    if (t < max_t && t > Figure.EPS)
                        return false;
            return true;
        }

        public Point3D RayTrace(Ray ray, int iteration, float env)
        {
            if (iteration <= 0)
                return new Point3D(0, 0, 0);

            float t = 0;        // позиция точки пересечения луча с фигурой на луче
            Point3D normal = null;
            Material m = new Material(0f, 0f, 0f, 0f);
            Point3D resultColor = new Point3D(0, 0, 0);
            bool refrationOutOfRange = false; //  проверка, что луч преломления выходит из объекта

            foreach (Figure figure in figures)
            {
                if (figure.Intersects(ray, out float intersect, out Point3D n) && (intersect < t || t == 0))
                {
                    t = intersect;
                    normal = n;
                    m = new Material(figure.material);
                }
            }

            if (t == 0)
                return new Point3D(0, 0, 0);

            if (Point3D.Scalar(ray.direction, normal) > 0)
            {
                normal *= -1;
                refrationOutOfRange = true;
            }

            Point3D hit_point = ray.start + ray.direction * t;

            foreach (Light l in lights)
            {
                Point3D amb = l.color * m.ambient;
                amb.x *= m.color.x;
                amb.y *= m.color.y;
                amb.z *= m.color.z;
                resultColor += amb;

                // диффузное освещение
                if (IsPointVisible(l.lightLocation, hit_point))
                    resultColor += l.Shade(hit_point, normal, m.color, m.diffuse);
            }

            if (m.reflection > 0)
            {
                Ray reflected_ray = ray.Reflect(hit_point, normal);
                resultColor += m.reflection * RayTrace(reflected_ray, iteration - 1, env);
            }

            if (m.refraction > 0)
            {
                //коэффициент преломления
                float eta = refrationOutOfRange ? m.environment : 1 / m.environment;
                Ray refractedRay = ray.Refract(hit_point, normal, eta);
                if (refractedRay != null)
                    resultColor += m.refraction * RayTrace(refractedRay, iteration - 1, m.environment);
            }

            return resultColor;
        }
    }
}
