using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lab6
{
    abstract class Primitive
    {
        public List<Point> Points { get; set; }
        public List<Polygon> Polygons { get; set; }

        public virtual void Draw(Graphics g, Transformation projection, int width, int height)
        {
            if (Condition())
                return;

            foreach (var Verge in Polygons)
                Verge.Draw(g, projection, width, height);
        }

        public virtual void Apply(Transformation t)
        {
            foreach (var point in Points)
                point.Apply(t);
        }

        public virtual bool Condition()
        {
            return false;
        }
    }
}
