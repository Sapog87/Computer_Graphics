using GraphicsHelper;
using System;
using System.Drawing;
using System.Windows.Forms;
using Point = GraphicsHelper.Point;

namespace AdvancedGraphics
{
    public partial class FormPlotting : Form
    {
        Func<double, double, double> SelectedFunction { get; set; }

        public FormPlotting()
        {
            InitializeComponent();

            Point.worldCenter = new PointF(canvas.Width / 2, canvas.Height / 1.5f);
            Point.projection = ProjectionType.PERSPECTIVE;
            Point.setProjection(canvas.Size, 1, 100, 45);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SelectedFunction = (double x, double y) => x * x + y * y;
            Redraw();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SelectedFunction = (double x, double y) => Math.Sin(x) + Math.Cos(y);
            Redraw();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            SelectedFunction = (double x, double y) => Math.Sin(x) * Math.Cos(y);
            Redraw();
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            SelectedFunction = (double x, double y) => x * y;
            Redraw();
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            SelectedFunction = (double x, double y) => x * Math.Pow(y, 2);
            Redraw();
        }

        private void FormPlotting_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':
                    changeViewAngles(shiftY: 2);
                    break;
                case 'a':
                    changeViewAngles(shiftX: -2);
                    break;
                case 's':
                    changeViewAngles(shiftY: -2);
                    break;
                case 'd':
                    changeViewAngles(shiftX: 2);
                    break;
                default: return;
            }
            Redraw();
            e.Handled = true;
        }        
    }
}
