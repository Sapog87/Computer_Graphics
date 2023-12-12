using GuroLightning.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GuroLightning
{
    public static class Models
    {
        public static MeshWithNormalsAndTexture Sphere(double diameter, int slices, int stacks)
        {
            var radius = diameter / 2;
            var vertices = new Vector[slices * stacks + slices];
            var normals = new Vector[slices * stacks + slices];
            var textureCoords = new Vector[slices * slices + slices];

            for (int stack = 0; stack < stacks; ++stack)
            {
                for (int slice = 0; slice < slices; ++slice)
                {
                    var theta = Math.PI * stack / stacks;
                    var phi = 2 * Math.PI * slice / slices;
                    vertices[stack * slices + slice] = new Vector(
                        radius * Math.Sin(theta) * Math.Cos(phi),
                        radius * Math.Sin(theta) * Math.Sin(phi),
                        radius * Math.Cos(theta));
                    normals[stack * slices + slice] = vertices[stack * slices + slice].Normalize();
                    textureCoords[stack * slices + slice] = new Vector(slice % 2, stack % 2);
                }
            }

            for (int slice = 0; slice < slices; ++slice)
            {
                vertices[stacks * slices + slice] = new Vector(
                    0,
                    0,
                    -radius);
                normals[stacks * slices + slice] = vertices[stacks * slices + slice].Normalize();
                textureCoords[stacks * slices + slice] = new Vector(slice % 2, stacks % 2);
            }

            var polygons = new List<Polygon>();
            for (int stack = 0; stack < stacks; ++stack)
            {
                for (int slice = 0; slice < slices; ++slice)
                {
                    var polygon = new Polygon();

                    polygon.Vertices = new List<int>
                    {
                        stack * slices + ((slice + 1) % slices),
                        (stack + 1) * slices + ((slice + 1) % slices),
                        (stack + 1) * slices + slice,
                        stack * slices + slice
                    };

                    polygon.Normals = new List<int>
                    {
                        stack * slices + ((slice + 1) % slices),
                        (stack + 1) * slices + ((slice + 1) % slices),
                        (stack + 1) * slices + slice,
                        stack * slices + slice
                    };

                    polygon.UVCoordinates = new List<int>
                    {
                        stack * slices + ((slice + 1) % slices),
                        (stack + 1) * slices + ((slice + 1) % slices),
                        (stack + 1) * slices + slice,
                        stack * slices + slice
                    };

                    polygons.Add(polygon);
                }
            }

            return new MeshWithNormalsAndTexture(vertices.ToList(), normals.ToList(), polygons, Resources.Texture2, textureCoords.ToList());
        }
    }
}
