using System;
using System.Drawing;

namespace RayTracing
{
    public class Sphere : Figure
    {
        private readonly float radius;
        public Pen drawingPen = new Pen(Color.Black);

        public Sphere(Point3D point, float radius)
        {
            this.radius = radius;
            points.Add(point);
        }
        public override void SetPen(Pen dw)
        {
            drawingPen = dw;
        }

        private bool RayIntersects(Ray ray, Point3D pos, float radius, out float t)
        {
            Point3D k = ray.start - pos;
            float b = Point3D.Scalar(k, ray.direction);
            float c = Point3D.Scalar(k, k) - radius * radius;
            float D = b * b - c;
            t = 0;

            if (D < 0)
                return false;

            float sqrtd = (float)Math.Sqrt(D);
            float t1 = -b + sqrtd;
            float t2 = -b - sqrtd;

            float min = Math.Min(t1, t2);
            float max = Math.Max(t1, t2);

            t = (min > EPS) ? min : max;
            return t > EPS;
        }


        public override bool Intersects(Ray r, out float t, out Point3D normal)
        {
            normal = null;

            if (!RayIntersects(r, points[0], radius, out t) || t <= EPS)
                return false;

            normal = Point3D.Norm((r.start + r.direction * t) - points[0]);
            material.color = new Point3D(drawingPen.Color.R / 255f, drawingPen.Color.G / 255f, drawingPen.Color.B / 255f);
            return true;
        }
    }
}
