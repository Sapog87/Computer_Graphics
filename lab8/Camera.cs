using System;
using System.Diagnostics;

namespace lab8
{
    class Camera
    {
        public Point Position { get; set; }
        public double Fi { get; set; }
        public double Theta { get; set; }
        public Matrix Projection { get; set; }
        public Point Forward { get { return new Point(-Math.Sin(Fi), Math.Sin(Theta), -Math.Cos(Fi)); } }
        public Point Left { get { return new Point(-Math.Sin(Fi + Math.PI / 2), 0, -Math.Cos(Fi + Math.PI / 2)); } }
        public Point Up { get { return Point.CrossProduct(Forward, Left); } }
        public Point Right { get { return -Left; } }
        public Point Backward { get { return -Forward; } }
        public Point Down { get { return -Up; } }

        public Matrix ViewProjection
        {
            get
            {
                return Transformations.Translate(-Position)
                    * Transformations.RotateY(-Fi)
                    * Transformations.RotateX(-Theta)
                    * Projection;
            }
        }

        public Camera(Point position, double angleY, double angleX, Matrix projection)
        {
            Position = position;
            Fi = angleY;
            Theta = angleX;
            Projection = projection;
        }
    }
}
