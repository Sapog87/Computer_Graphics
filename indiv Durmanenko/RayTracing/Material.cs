namespace RayTracing
{
    public class Material
    {
        public float reflection;    // коэффициент отражения
        public float refraction;    // коэффициент преломления
        public float environment;   // коэффициент преломления среды
        public float ambient;       // коэффициент принятия фонового освещения
        public float diffuse;       // коэффициент принятия диффузного освещения
        public Point3D color;       // цвет материала

        public Material(float reflection, float refraction, float ambient, float diffuse, float environment = 1.0f)
        {
            this.reflection = reflection;
            this.refraction = refraction;
            this.environment = environment;
            this.ambient = ambient;
            this.diffuse = diffuse;
        }

        public Material(Material m)
        {
            reflection = m.reflection;
            refraction = m.refraction;
            environment = m.environment;
            ambient = m.ambient;
            diffuse = m.diffuse;
            color = new Point3D(m.color);
        }
    }

    public static class Materials
    {
        public static Material WallDefault = new Material(0.0f, 0.0f, 0.1f, 0.8f);
        public static Material WallSpecular = new Material(0.8f, 0.0f, 0.0f, 0.0f);

        public static Material FigureDefault = new Material(0.0f, 0.0f, 0.1f, 0.8f);
        public static Material FigureRefractable = new Material(0.0f, 0.9f, 0.0f, 0.0f);
        public static Material FigureSpecular = new Material(0.9f, 0.0f, 0.0f, 0.0f);
    }
}
