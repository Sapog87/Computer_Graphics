using GraphicsHelper;
using Point = GraphicsHelper.Point;

namespace AdvancedGraphics
{
    public partial class FormPlotting
    {
        private const int scaleFactor = 100;
        private const double threshold = 5;
        private double yawAngle;
        private double pitchAngle;
        private double[] upHorizon;
        private double[] downHorizon;

        private FloatingPoint GetScaledPoint(Point p)
        {
            var res = AffineTransformations.rotate(p, AxisType.Y, yawAngle);
            res = AffineTransformations.rotate(res, AxisType.X, pitchAngle);
            var x = Point.worldCenter.X + scaleFactor * res.Xf;
            if (x < 0 || x >= canvas.Width)
                return new FloatingPoint(x, scaleFactor * res.Yf, res.Zf * scaleFactor, Visibilty.INVISIBLE);
            else
                return new FloatingPoint(x, scaleFactor * res.Yf, res.Zf * scaleFactor, ref upHorizon, ref downHorizon);
        }

        public void changeViewAngles(double shiftX = 0, double shiftY = 0)
        {
            pitchAngle = Math.Clamp(pitchAngle + shiftY, -89.0, 89.0);
            yawAngle = Math.Clamp(yawAngle + shiftX, -89.0, 89.0);
        }

        private Color GetColorByVisibility(Visibilty v)
        {
            return v switch
            {
                Visibilty.VISIBLE_UP => Color.Red,
                Visibilty.VISIBLE_DOWN => Color.Blue,
                Visibilty.INVISIBLE => Color.White,
                _ => Color.White,
            };
        }

        void UpdateHorizons(FloatingPoint last, FloatingPoint curr)
        {
            if (last.X < 0 || curr.X >= canvas.Width)
                return;

            if (curr.X - last.X == 0)
            {
                upHorizon[curr.X] = Math.Max(upHorizon[curr.X], curr.Yf);
                downHorizon[curr.X] = Math.Min(downHorizon[curr.X], curr.Yf);
            }
            else
            {
                var tg = (curr.Yf - last.Yf) / (curr.Xf - last.Xf);
                for (int x = last.X; x <= curr.X; x++)
                {
                    double y = tg * (x - last.Xf) + last.Yf;
                    upHorizon[x] = Math.Max(upHorizon[x], y);
                    downHorizon[x] = Math.Min(downHorizon[x], y);
                }
            }
        }

        FloatingPoint FindIntersect(FloatingPoint previous, FloatingPoint curr, bool visible)
        {
            double xStep = (curr.Xf - previous.Xf * 1.0) / 20;
            double yStep = (curr.Yf - previous.Yf * 1.0) / 20;
            for (int i = 1; i <= 20; i++)
            {
                FloatingPoint point = new FloatingPoint(Math.Clamp(previous.Xf + i * xStep, 0, canvas.Width - 1), previous.Yf + i * yStep, curr.Zf,
                    ref upHorizon, ref downHorizon);
                if (visible)
                {
                    if (point.Visibility == Visibilty.INVISIBLE)
                        return point;
                }
                else
                {
                    if (point.Visibility != Visibilty.INVISIBLE)
                        return point;
                }
            }

            return curr;
        }


        void FloatingHorizon(Func<double, double, double> f)
        {
            double step = threshold * 2.0 / 75;

            upHorizon = new double[canvas.Width];
            downHorizon = new double[canvas.Width];

            Array.Fill(upHorizon, double.MinValue);
            Array.Fill(downHorizon, double.MaxValue);

            var bitmap = new Bitmap(canvas.Width, canvas.Height);
            var fbitmap = new FastBitmap.FastBitmap(bitmap);

            for (double z = threshold; z >= -threshold; z -= step)
            {
                FloatingPoint previous = GetScaledPoint(new Point(-threshold, f(-threshold, z), z));
                for (double x = -threshold; x <= threshold; x += step)
                {
                    FloatingPoint current = GetScaledPoint(new Point(x, f(x, z), z));

                    if (current.Visibility == Visibilty.VISIBLE_UP)
                    {
                        if (previous.Visibility != Visibilty.INVISIBLE)
                        {
                            Liner.DrawLine(ref fbitmap, previous.toSimple2D(), current.toSimple2D(), GetColorByVisibility(Visibilty.VISIBLE_UP));
                            UpdateHorizons(previous, current);
                        }
                        else
                        {
                            var mid = FindIntersect(current, previous, true);
                            Liner.DrawLine(ref fbitmap, current.toSimple2D(), mid.toSimple2D(), GetColorByVisibility(Visibilty.VISIBLE_UP));
                            UpdateHorizons(current, mid);
                        }

                    }
                    else if (current.Visibility == Visibilty.VISIBLE_DOWN)
                    {
                        if (previous.Visibility != Visibilty.INVISIBLE)
                        {
                            Liner.DrawLine(ref fbitmap, previous.toSimple2D(), current.toSimple2D(),
                                GetColorByVisibility(Visibilty.VISIBLE_DOWN));
                            UpdateHorizons(previous, current);
                        }
                        else
                        {
                            var mid = FindIntersect(current, previous, true);
                            Liner.DrawLine(ref fbitmap, current.toSimple2D(), mid.toSimple2D(), GetColorByVisibility(Visibilty.VISIBLE_DOWN));
                            UpdateHorizons(current, mid);
                        }

                    }
                    else
                    {
                        if (previous.Visibility == Visibilty.VISIBLE_UP)
                        {
                            var mid = FindIntersect(current, previous, false);
                            Liner.DrawLine(ref fbitmap, previous.toSimple2D(), mid.toSimple2D(), GetColorByVisibility(Visibilty.VISIBLE_UP));
                            UpdateHorizons(previous, mid);
                        }
                        else if (previous.Visibility == Visibilty.VISIBLE_DOWN)
                        {
                            var mid = FindIntersect(current, previous, false);
                            Liner.DrawLine(ref fbitmap, previous.toSimple2D(), mid.toSimple2D(), GetColorByVisibility(Visibilty.VISIBLE_DOWN));
                            UpdateHorizons(previous, mid);
                        }
                    }

                    previous = current;
                }
            }

            fbitmap.Dispose();
            canvas.Image = bitmap;
        }


        void Redraw()
        {
            FloatingHorizon(SelectedFunction);
        }
    }
}
