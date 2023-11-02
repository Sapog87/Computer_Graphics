using System;

namespace Lab6
{
    class PrimitiveFactory
    {
        public static Primitive Hexahedron()
        {
            return new Hexahedron(0.5);
        }

        public static Primitive Tetrahedron()
        {
            return new Tetrahedron(0.5);
        }

        public static Primitive Octahedron()
        {
            return new Octahedron(0.5);
        }
    }
}