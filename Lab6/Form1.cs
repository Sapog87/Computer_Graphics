using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Bitmap bmp;
        private Primitive currentPolyhedron;
        private Dictionary<string, Transformation> projections = new Dictionary<string, Transformation>
        {
            {"Perspective",     Transformation.PerspectiveProjection()   },
            {"Isometric",       Transformation.IsometricProjection()     },
            {"Orthographic XY", Transformation.OrthographicXYProjection()},
            {"Orthographic XZ", Transformation.OrthographicXZProjection()},
            {"Orthographic YZ", Transformation.OrthographicYZProjection()},
        };

        public Form1()
        {
            InitializeComponent();

            bmp = new Bitmap(Box.Width, Box.Height);
            g = Graphics.FromImage(bmp);
            Box.Image = bmp;

            PrimitiveComboBox.SelectedItem = PrimitiveComboBox.Items[1];
            ReflectionComboBox.SelectedItem = ReflectionComboBox.Items[0];
            PerspectiveComboBox.SelectedItem = PerspectiveComboBox.Items[1];

            GetPrimitive();
            DrawAxis(g, GetProjection(), Box.Width, Box.Height);
        }

        private Transformation GetProjection()
        {
            return projections[PerspectiveComboBox.SelectedItem.ToString()];
        }

        private void GetPrimitive()
        {
            switch (PrimitiveComboBox.SelectedItem.ToString())
            {
                case "Tetrahedron":
                    {
                        currentPolyhedron = PrimitiveFactory.Tetrahedron();
                        break;
                    }
                case "Hexahedron":
                    {
                        currentPolyhedron = PrimitiveFactory.Hexahedron();
                        break;
                    }
                case "Octahedron":
                    {
                        currentPolyhedron = PrimitiveFactory.Octahedron();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void DrawAxis(Graphics g, Transformation t, int width, int height)
        {
            List<Point> points = new List<Point> {
                new Point(0, 0, 0),
                new Point(1, 0, 0),
                new Point(0, 1, 0),
                new Point(0, 0, 1)
            };

            List<Primitive> primitives = new List<Primitive>
            {
                new Line(points[0], points[1]),
                new Line(points[0], points[2]),
                new Line(points[0], points[3]),
                currentPolyhedron
            };

            foreach (Primitive p in primitives)
                p.Draw(g, t, width, height);
        }

        private void Clear()
        {
            bmp = new Bitmap(Box.Width, Box.Height);
            g = Graphics.FromImage(bmp);
            Box.Image = bmp;
        }

        private void Translate()
        {
            currentPolyhedron.Apply(
                Transformation.Translate(
                    (double)Translate1.Value,
                    (double)Translate2.Value,
                    (double)Translate3.Value
                )
            );
        }

        private void Rotate()
        {
            currentPolyhedron.Apply(
                Transformation.RotateX((double)Rotate1.Value / 180.0 * Math.PI)
                * Transformation.RotateY((double)Rotate2.Value / 180.0 * Math.PI)
                * Transformation.RotateZ((double)Rotate3.Value / 180.0 * Math.PI));
        }

        private void Scale()
        {
            currentPolyhedron.Apply(
                Transformation.Scale(
                    (double)Scale1.Value,
                    (double)Scale2.Value,
                    (double)Scale3.Value
                )
            );
        }

        private void Reflect()
        {
            switch (ReflectionComboBox.SelectedItem.ToString())
            {
                case "X":
                    {
                        currentPolyhedron.Apply(Transformation.ReflectX());
                        break;
                    }
                case "Y":
                    {
                        currentPolyhedron.Apply(Transformation.ReflectY());
                        break;
                    }
                case "Z":
                    {
                        currentPolyhedron.Apply(Transformation.ReflectZ());
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void RotateAroundLine()
        {
            Line line = new Line(
                new Point(
                    (double)Point1X.Value,
                    (double)Point1Y.Value,
                    (double)Point1Z.Value
                ),
                new Point(
                    (double)Point2X.Value,
                    (double)Point2Y.Value,
                    (double)Point2Z.Value
                )
            );
            double angle = (double)Angle.Value / 180.0 * Math.PI;
            currentPolyhedron.Apply(Transformation.RotateLine(line, angle));
        }

        private void ApplyPerspective_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(Box.Width, Box.Height);
            g = Graphics.FromImage(bmp);
            Box.Image = bmp;
            DrawAxis(g, GetProjection(), Box.Width, Box.Height);
        }

        private void ApplyPrimitive_Click(object sender, EventArgs e)
        {
            Clear();
            GetPrimitive();
            DrawAxis(g, GetProjection(), Box.Width, Box.Height);
        }

        private void ApplyAffin_Click(object sender, EventArgs e)
        {
            Clear();
            Translate();
            Rotate();
            Scale();
            DrawAxis(g, GetProjection(), Box.Width, Box.Height);
        }

        private void ApplyReflection_Click(object sender, EventArgs e)
        {
            Clear();
            Reflect();
            DrawAxis(g, GetProjection(), Box.Width, Box.Height);
        }

        private void ApplyLineRotation_Click(object sender, EventArgs e)
        {
            Clear();
            RotateAroundLine();
            DrawAxis(g, GetProjection(), Box.Width, Box.Height);
        }
    }
}
