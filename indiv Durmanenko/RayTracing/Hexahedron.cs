using System;
using System.Drawing;

namespace RayTracing
{
    public class Hexahedron : Figure
    {
        public bool isRoom = false;

        public Material frontWallMaterial;
        public Material backWallMaterial;
        public Material leftWallMaterial;
        public Material rightWallMaterial;
        public Material upWallMaterial;
        public Material downWallMaterial;

        public Hexahedron(float size)
        {
            points.Add(new Point3D(size / 2, size / 2, size / 2));
            points.Add(new Point3D(-size / 2, size / 2, size / 2));
            points.Add(new Point3D(-size / 2, size / 2, -size / 2));
            points.Add(new Point3D(size / 2, size / 2, -size / 2));

            points.Add(new Point3D(size / 2, -size / 2, size / 2));
            points.Add(new Point3D(-size / 2, -size / 2, size / 2));
            points.Add(new Point3D(-size / 2, -size / 2, -size / 2));
            points.Add(new Point3D(size / 2, -size / 2, -size / 2));

            Side side = new Side(this);
            side.points.AddRange(new int[] { 3, 2, 1, 0 });
            sides.Add(side);

            side = new Side(this);
            side.points.AddRange(new int[] { 4, 5, 6, 7 });
            sides.Add(side);

            side = new Side(this);
            side.points.AddRange(new int[] { 2, 6, 5, 1 });
            sides.Add(side);

            side = new Side(this);
            side.points.AddRange(new int[] { 0, 4, 7, 3 });
            sides.Add(side);

            side = new Side(this);
            side.points.AddRange(new int[] { 1, 5, 4, 0 });
            sides.Add(side);

            side = new Side(this);
            side.points.AddRange(new int[] { 2, 3, 7, 6 });
            sides.Add(side);
        }

        public override void SetPen(Pen dw)
        {
            foreach (Side s in sides)
                s.drawing_pen = dw;
        }
        private bool RayIntersects(Ray ray, Point3D p0, Point3D p1, Point3D p2, out float intersection)
        {
            intersection = -1;

            Point3D edge1 = p1 - p0;
            Point3D edge2 = p2 - p0;
            Point3D h = ray.direction * edge2;
            float a = Point3D.Scalar(edge1, h);

            // параллельность луча
            if (a > -EPS && a < EPS)
                return false;

            float c = 1.0f / a;
            Point3D s = ray.start - p0;
            float u = c * Point3D.Scalar(s, h);

            if (u < 0 || u > 1)
                return false;

            Point3D q = s * edge1;
            float v = c * Point3D.Scalar(ray.direction, q);

            if (v < 0 || u + v > 1)
                return false;

            // Находим точку пересечения
            float t = c * Point3D.Scalar(edge2, q);
            if (t > EPS)
            {
                intersection = t;
                return true;
            }
            // Не лучевое пересечение
            else
            {
                return false;
            }
        }

        public override bool Intersects(Ray r, out float intersection, out Point3D normal)
        {
            intersection = 0;
            Side side = null;
            int sideNumber = -1;

            for (int i = 0; i < sides.Count; i++)
            {
                if ((RayIntersects(r, sides[i].GetPoint(1), sides[i].GetPoint(2), sides[i].GetPoint(3), out float t) && (intersection == 0 || t < intersection))
                    || (RayIntersects(r, sides[i].GetPoint(0), sides[i].GetPoint(1), sides[i].GetPoint(3), out t) && (intersection == 0 || t < intersection)))
                {
                    sideNumber = i;
                    intersection = t;
                    side = sides[i];
                }
            }

            normal = null;
            if (intersection == 0)
                return false;

            normal = Side.Norm(side);

            // рисуем комнату
            if (isRoom)
            {
                switch (sideNumber)
                {
                    case 0:
                        material = new Material(backWallMaterial);
                        break;
                    case 1:
                        material = new Material(frontWallMaterial);
                        break;
                    case 2:
                        material = new Material(rightWallMaterial);
                        break;
                    case 3:
                        material = new Material(leftWallMaterial);
                        break;
                    case 4:
                        material = new Material(upWallMaterial);
                        break;
                    case 5:
                        material = new Material(downWallMaterial);
                        break;
                    default:
                        throw new Exception("Неизвестный случай");
                }
            }
            material.color = new Point3D(side.drawing_pen.Color.R / 255f, side.drawing_pen.Color.G / 255f, side.drawing_pen.Color.B / 255f);
            return true;
        }
    }
}
