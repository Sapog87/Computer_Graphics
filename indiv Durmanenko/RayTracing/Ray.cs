using System;

namespace RayTracing
{
    public class Ray
    {
        public Point3D start;
        public Point3D direction;

        public Ray(Point3D start, Point3D end)
        {
            this.start = new Point3D(start);
            direction = Point3D.Norm(end - start);
        }

        private Ray() { }

        // отражение
        public Ray Reflect(Point3D hitPoint, Point3D normal)
        {
            Point3D direcion = direction - 2 * normal * Point3D.Scalar(direction, normal);
            return new Ray(hitPoint, hitPoint + direcion);
        }

        // преломление
        public Ray Refract(Point3D hitPoint, Point3D normal, float eta)
        {
            float scalar = Point3D.Scalar(normal, direction);
            float k = 1 - eta * eta * (1 - scalar * scalar);

            if (k < 0)
                return null;
            float cos = (float)Math.Sqrt(k);
            Ray ray = new Ray
            {
                start = new Point3D(hitPoint),
                direction = Point3D.Norm(eta * direction - (cos + eta * scalar) * normal)
            };
            return ray;
        }
    }
}
