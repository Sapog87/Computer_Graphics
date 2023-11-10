namespace Lab6
{
    class Polyhedron
    {
        public List<Point> Points { get; set; }
        public List<Polygon> Polygons { get; set; }

        public Polyhedron(List<Point> points, List<Polygon> polygons)
        {
            Points = points;
            Polygons = polygons;
        }

        public virtual void Draw(Graphics g, Transformation projection, int width, int height)
        {
            foreach (var polygon in Polygons)
                polygon.Draw(g, projection, width, height);
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

        public override string? ToString()
        {
            return GetType().Name;
        }
    }
}
