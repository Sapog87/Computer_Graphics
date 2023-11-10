using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters;
using System.Text.Json;
using System.Diagnostics;
using Microsoft.VisualBasic;
using AngouriMath;
using AngouriMath.Extensions;
using System.Reflection;

namespace Lab6
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Bitmap bmp;
        private Polyhedron currentPolyhedron;
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
            AxisComboBox.SelectedItem = AxisComboBox.Items[0];

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
                        currentPolyhedron = PolyhedronFactory.Tetrahedron(0.5);
                        break;
                    }
                case "Hexahedron":
                    {
                        currentPolyhedron = PolyhedronFactory.Hexahedron(0.5);
                        break;
                    }
                case "Octahedron":
                    {
                        currentPolyhedron = PolyhedronFactory.Octahedron(0.5);
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

            List<Line> lines = new List<Line>
            {
                new Line(points[0], points[1]),
                new Line(points[0], points[2]),
                new Line(points[0], points[3])
            };

            foreach (Line p in lines)
                p.Draw(g, t, width, height);

            currentPolyhedron.Draw(g, t, width, height);
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

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Object Files(*.obj)| *.obj | Text files(*.txt) | *.txt | All files(*.*) | *.* ";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string info = "";
                    info += currentPolyhedron.ToString() + "\n";
                    info += currentPolyhedron.Points.Count + "\n";
                    info += currentPolyhedron.Polygons.Count + "\n";

                    foreach (Point point in currentPolyhedron.Points)
                    {
                        info += point.X + " " + point.Y + " " + point.Z + "\n";
                    }

                    foreach (Polygon polygon in currentPolyhedron.Polygons)
                    {
                        foreach (Point point in polygon.Points)
                        {
                            int index = currentPolyhedron.Points.FindIndex(x => x.Equals(point));
                            info += index + " ";
                        }
                        info += "\n";
                    }

                    File.WriteAllText(saveDialog.FileName, info);
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно сохранить файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadDialog = new OpenFileDialog();
            loadDialog.Filter = "Object Files(*.obj)|*.obj|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (loadDialog.ShowDialog() == DialogResult.OK)
            {
                Debug.WriteLine(loadDialog.FileName);
                try
                {
                    Clear();
                    List<Point> points = new List<Point>();
                    List<Polygon> polygons = new List<Polygon>();

                    string[] info = File.ReadAllText(loadDialog.FileName).Split("\n");

                    string type_of_primitive = info[0];
                    int points_count = int.Parse(info[1]);
                    int polygons_count = int.Parse(info[2]);

                    for (int i = 3; i < 3 + points_count; i++)
                    {
                        double[] coordinates = Array.ConvertAll(info[i].Split(' '), s => double.Parse(s));
                        points.Add(new Point(coordinates[0], coordinates[1], coordinates[2]));
                    }

                    for (int i = 3 + points_count; i < 3 + points_count + polygons_count; i++)
                    {
                        string[] strings = info[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        int[] indices = Array.ConvertAll(strings, Int32.Parse);
                        List<Point> polygon_points = new List<Point>();
                        foreach (int index in indices)
                        {
                            polygon_points.Add(points[index]);
                        }
                        polygons.Add(new Polygon(polygon_points));
                    }

                    currentPolyhedron = new Polyhedron(points, polygons);

                    DrawAxis(g, GetProjection(), Box.Width, Box.Height);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void AddPointGeneratrix_Click(object sender, EventArgs e)
        {
            double x = (double)GeneratrixX.Value;
            double y = (double)GeneratrixY.Value;
            double z = (double)GeneratrixZ.Value;
            GeneratrixX.Value = 0;
            GeneratrixY.Value = 0;
            GeneratrixZ.Value = 0;
            GeneratrixListBox.Items.Add(new Point(x, y, z));
        }

        private void DeletePointGeneratrix_Click(object sender, EventArgs e)
        {
            if (GeneratrixListBox.SelectedIndex == -1)
                return;
            GeneratrixListBox.Items.RemoveAt(GeneratrixListBox.SelectedIndex);
        }

        private void Build_Click(object sender, EventArgs e)
        {
            Clear();
            List<Point> points = new List<Point>();

            foreach (var p in GeneratrixListBox.Items)
                points.Add((Point)p);

            int axis = 0;
            switch (AxisComboBox.SelectedItem.ToString())
            {
                case "OX":
                    axis = 0;
                    break;
                case "OY":
                    axis = 1;
                    break;
                case "OZ":
                    axis = 2;
                    break;
            }

            currentPolyhedron = PolyhedronFactory.RotationFigure(
                points,
                axis,
                (int)Partition.Value
            );

            DrawAxis(g, GetProjection(), Box.Width, Box.Height);
        }

        private void FunctionBuild_Click(object sender, EventArgs e)
        {
            double x0 = (double)FunctionX0UpDown.Value;
            double x1 = (double)FunctionX1UpDown.Value;
            double y0 = (double)FunctionY0UpDown.Value;
            double y1 = (double)FunctionY1UpDown.Value;
            double xStep = (double)FunctionXStepUpDown.Value;
            double yStep = (double)FunctionYStepUpDown.Value;
            string function = FunctionTextBox.Text;

            Clear();

            currentPolyhedron = PolyhedronFactory.Function(x0, x1, y0, y1, xStep, yStep, function);

            DrawAxis(g, GetProjection(), Box.Width, Box.Height);
        }
    }
}
