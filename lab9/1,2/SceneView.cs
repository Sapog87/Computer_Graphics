using System.Drawing;
using System.Windows.Forms;
using System;
using System.Diagnostics;

namespace GuroLightning
{
    public class SceneView : Control
    {
        public Camera Camera { get; set; }
        public Mesh Drawable { get; set; }
        public Graphics3D Graphics3D { get; private set; }

        public SceneView() : base()
        {
            var flags = ControlStyles.AllPaintingInWmPaint
                      | ControlStyles.DoubleBuffer
                      | ControlStyles.UserPaint;
            SetStyle(flags, true);
            ResizeRedraw = true;
            Graphics3D = new Graphics3D(this);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Graphics3D.Resize();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (null == Camera)
                return;

            Debug.WriteLine("start");

            Graphics3D.StartDrawing();
            Drawable.Draw(Graphics3D);
            e.Graphics.DrawImage(Graphics3D.FinishDrawing(), 0, 0);

            Debug.WriteLine("finish");
        }
    }
}
