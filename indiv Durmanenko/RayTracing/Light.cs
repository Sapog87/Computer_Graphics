using System;

namespace RayTracing
{
    public class Light
    {
        public Point3D lightLocation;
        public Point3D color;

        public Light(Point3D point, Point3D c)
        {
            lightLocation = new Point3D(point);
            color = new Point3D(c);
        }

        public Point3D Shade(Point3D hitPoint, Point3D normal, Point3D objColor, float diffuseCoef)
        {
            Point3D direction = lightLocation - hitPoint;
            // направление луча из источника света в точку удара
            direction = Point3D.Norm(direction);

            Point3D diff = diffuseCoef * color * Math.Max(Point3D.Scalar(normal, direction), 0);
            return new Point3D(diff.x * objColor.x, diff.y * objColor.y, diff.z * objColor.z);
        }
    }
}
