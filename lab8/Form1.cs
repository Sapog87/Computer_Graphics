using System.Windows.Forms;
using System;
using System.Drawing;
using System.Collections.Generic;

namespace lab8
{
    public partial class Form1 : Form
    {
        Polyhedron currentPolyhedron;
        bool without_colors = false;
        private Camera camera;

        List<Polyhedron> polyhedrons = new List<Polyhedron>();
        bool moreThanOneObj = false;

        public Form1()
        {
            InitializeComponent();
            currentPolyhedron = new Icosahedron(1);
            Matrix projection = Transformations.PerspectiveProjection(-0.1, 0.1, -0.1, 0.1, 0.1, 10);
            camera = new Camera(new Point(2, 2, 2), Math.PI / 4, -Math.PI / 4, projection);
            ProjectionComboBox.SelectedItem = ProjectionComboBox.Items[0];
            PrimitiveComboBox.SelectedItem = PrimitiveComboBox.Items[0];
        }

        private static double DegToRad(double deg)
        {
            return deg / 180 * Math.PI;
        }

        private void Scale()
        {
            double scalingX = (double)numericUpDown1.Value;
            double scalingY = (double)numericUpDown2.Value;
            double scalingZ = (double)numericUpDown3.Value;
            currentPolyhedron.Apply(Transformations.Scale(scalingX, scalingY, scalingZ));
            Box.Refresh();
        }

        private void Rotate()
        {
            double rotatingX = DegToRad((double)numericUpDown4.Value);
            double rotatingY = DegToRad((double)numericUpDown5.Value);
            double rotatingZ = DegToRad((double)numericUpDown6.Value);
            currentPolyhedron.Apply(Transformations.RotateX(rotatingX)
                * Transformations.RotateY(rotatingY)
                * Transformations.RotateZ(rotatingZ));
            Box.Refresh();
        }

        private void Translate()
        {
            double translatingX = (double)numericUpDown7.Value;
            double translatingY = (double)numericUpDown8.Value;
            double translatingZ = (double)numericUpDown9.Value;
            currentPolyhedron.Apply(Transformations.Translate(translatingX, translatingY, translatingZ));
            Box.Refresh();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            double delta = 0.1;
            switch (keyData)
            {
                case Keys.W: camera.Position *= Transformations.Translate(0.1 * camera.Forward); break;
                case Keys.A: camera.Position *= Transformations.Translate(0.1 * camera.Left); break;
                case Keys.S: camera.Position *= Transformations.Translate(0.1 * camera.Backward); break;
                case Keys.D: camera.Position *= Transformations.Translate(0.1 * camera.Right); break;
                case Keys.Q: camera.Position *= Transformations.Translate(0.1 * camera.Up); break;
                case Keys.E: camera.Position *= Transformations.Translate(0.1 * camera.Down); break;
                case Keys.Left: camera.Fi += delta; break;
                case Keys.Right: camera.Fi -= delta; break;
                case Keys.Up: camera.Theta += delta; break;
                case Keys.Down: camera.Theta -= delta; break;
            }
            Box.Refresh();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ApplyAffin_Click(object sender, EventArgs e)
        {
            Scale();
            Rotate();
            Translate();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Object Files(*.obj)|*.obj|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    currentPolyhedron.Save(saveDialog.FileName);
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно сохранить файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Object Files(*.obj)|*.obj|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                currentPolyhedron = new Polyhedron(openDialog.FileName);
            }
            catch
            {
                MessageBox.Show("Ошибка при чтении файла",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyPrimitive_Click(object sender, EventArgs e)
        {
            moreThanOneObj = false;
            without_colors = false;
            polyhedrons.Clear();
            switch (PrimitiveComboBox.SelectedItem.ToString())
            {
                case "Tetrahedron":
                    {
                        currentPolyhedron = new Tetrahedron(1);
                        break;
                    }
                case "Icosahedron":
                    {
                        currentPolyhedron = new Icosahedron(1);
                        break;
                    }
                case "Hexahedron":
                    {
                        currentPolyhedron = new Hexahedron(1);
                        break;
                    }
                default:
                    {
                        currentPolyhedron = new Tetrahedron(1);
                        break;
                    }
            }

            Matrix projection = Transformations.PerspectiveProjection(-0.1, 0.1, -0.1, 0.1, 0.1, 100);
            camera = new Camera(new Point(2, 2, 2), Math.PI / 4, -Math.PI / 4, projection);
            Box.Invalidate();
        }

        private void DrawWithoutColors_Click(object sender, EventArgs e)
        {
            without_colors = !without_colors;
            Box.Refresh();
        }

        private void Box_Paint(object sender, PaintEventArgs e)
        {
            if (currentPolyhedron == null)
                return;

            var graphics3D = new Graphics3D(e.Graphics, camera.ViewProjection, Box.Width, Box.Height, camera.Position);

            if (without_colors)
                currentPolyhedron.DrawNoColor(graphics3D);
            else if (moreThanOneObj)
                foreach (var pol in polyhedrons)
                    pol.Draw(graphics3D);
            else
                currentPolyhedron.Draw(graphics3D);

            e.Graphics.DrawImage(graphics3D.ColorBuffer, 0, 0);
        }

        private void DrawSceneBtn_Click(object sender, EventArgs e)
        {
            without_colors = false;
            moreThanOneObj = true;
            currentPolyhedron = new Icosahedron(1);
            currentPolyhedron.Apply(Transformations.Translate(0, 0.20, -0.40));
            polyhedrons.Add(currentPolyhedron);
            currentPolyhedron = new Tetrahedron(1);
            currentPolyhedron.Apply(Transformations.Translate(0, 0, 0.80));
            polyhedrons.Add(currentPolyhedron);
            Box.Invalidate();
        }
    }
}
