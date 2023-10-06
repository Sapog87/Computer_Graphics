using System;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics graphics;

        List<Segment> segments = new List<Segment>();

        List<PointF> polygon = new List<PointF>();
        PointF minPolygonCoord, maxPolygonCoord;

        PointF startPoint, endPoint;
        PointF pointLocation;
        Boolean isMouseDown = false;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = bmp;
            graphics = Graphics.FromImage(pictureBox.Image);
            graphics.Clear(Color.White);
            pointRadioButton.Checked = true;
            startPoint = Point.Empty;
            endPoint = Point.Empty;
            pointLocation = new Point();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            graphics = Graphics.FromImage(pictureBox.Image);
            graphics.Clear(Color.White);
            segments.Clear();
            polygon.Clear();
            pointLocation = Point.Empty;
            pictureBox.Invalidate();
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (segmentRadioButton.Checked && isMouseDown)
            {
                isMouseDown = false;

                if (endPoint == Point.Empty)
                    return;

                segments.Add(new Segment(startPoint, endPoint));
                startPoint = Point.Empty;
                endPoint = Point.Empty;
            }
            else if (polygonRadioButton.Checked && isMouseDown)
            {
                isMouseDown = false;

                if (endPoint == Point.Empty)
                    return;

                polygon.Add(endPoint);

                if (endPoint.X < minPolygonCoord.X)
                    minPolygonCoord.X = endPoint.X;

                if (endPoint.Y < minPolygonCoord.Y)
                    minPolygonCoord.Y = endPoint.Y;

                if (endPoint.X > maxPolygonCoord.X)
                    maxPolygonCoord.X = endPoint.X;

                if (endPoint.Y > maxPolygonCoord.Y)
                    maxPolygonCoord.Y = endPoint.Y;

                startPoint = endPoint;
                endPoint = Point.Empty;
            }
            else if (pointRadioButton.Checked)
            {
                pointLocation = e.Location;

                if (polygon.Count != 0)
                {
                    string isPointInPolygon = IsPointInPolygon(pointLocation, polygon).ToString();
                    if (IsConvexPolygon(polygon))
                    {
                        belongsToConvex.Text = isPointInPolygon;
                        belongsToNConvex.Text = string.Empty;
                    }
                    else
                    {
                        belongsToConvex.Text = string.Empty;
                        belongsToNConvex.Text = isPointInPolygon;
                    }
                }

                try
                {
                    Segment segment = segments.Single();
                    relativePointPosition.Text =
                        PointPositionRelativeToSegment(segment.left, segment.right, pointLocation);
                }
                catch
                {
                    relativePointPosition.Text = "На рисунке должен быть только один отрезок";
                }
            }
            pictureBox.Invalidate();
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!pointRadioButton.Checked && isMouseDown)
                endPoint = e.Location;
            pictureBox.Invalidate();
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (segmentRadioButton.Checked)
            {
                isMouseDown = true;
                startPoint = e.Location;
            }
            else if (polygonRadioButton.Checked)
            {
                isMouseDown = true;
                if (polygon.Count == 0)
                {
                    startPoint = e.Location;
                    minPolygonCoord = e.Location;
                    maxPolygonCoord = e.Location;
                    polygon.Add(startPoint);
                }
            }
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            pictureBox.Image = bmp;
            graphics = Graphics.FromImage(pictureBox.Image);
            graphics.Clear(Color.White);

            if (segments.Count > 0)
            {
                foreach (Segment segment in segments)
                    graphics.DrawLine(Pens.Blue, segment.left, segment.right);
            }

            if (polygon.Count > 3)
            {
                for (int i = 0; i < polygon.Count - 1; ++i)
                    graphics.DrawLine(Pens.Blue, polygon[i], polygon[i + 1]);
                graphics.DrawLine(Pens.Blue, polygon[0], polygon[polygon.Count - 1]);
            }
            else
            {
                for (int i = 0; i < polygon.Count - 1; ++i)
                    graphics.DrawLine(Pens.Blue, polygon[i], polygon[i + 1]);
            }

            if (startPoint != Point.Empty && endPoint != Point.Empty)
                graphics.DrawLine(Pens.Green, startPoint, endPoint);

            if (pointLocation != Point.Empty)
            {
                graphics.DrawEllipse(Pens.Red, pointLocation.X - 1, pointLocation.Y - 1, 3, 3);
                graphics.FillEllipse(Brushes.Red, pointLocation.X - 1, pointLocation.Y - 1, 3, 3);
            }
        }

        private void movePolygonButton_Click(object sender, EventArgs e)
        {
            if (polygon.Count < 4)
                return;

            int x = (int)movePolygonNumericUpDownX.Value;
            int y = (int)movePolygonNumericUpDownX.Value;
            translate_coordinates(x, y);
            pictureBox.Invalidate();
        }

        private void rotatePolygonButton_Click(object sender, EventArgs e)
        {
            if (polygon.Count < 4)
                return;

            double angle = (double)rotatePolygonNumericUpDownAngle.Value;

            Matrix M = new Matrix(3, 3);

            M[0, 0] = Math.Cos(angle * Math.PI / 180);
            M[0, 1] = -Math.Sin(angle * Math.PI / 180);
            M[0, 2] = 0;

            M[1, 0] = Math.Sin(angle * Math.PI / 180);
            M[1, 1] = Math.Cos(angle * Math.PI / 180);
            M[1, 2] = 0;

            M[2, 0] = 0;
            M[2, 1] = 0;
            M[2, 2] = 1;

            scale_or_rotate_polygon(M, rotatePolygonCheckBox);

            pictureBox.Invalidate();
        }

        private void scalePolygonButton_Click(object sender, EventArgs e)
        {
            if (polygon.Count < 4)
                return;

            double x = (double)scalePolygonNumericUpDownX.Value;
            double y = (double)scalePolygonNumericUpDownY.Value;

            Matrix M = new Matrix(3, 3);

            M[0, 0] = x;
            M[0, 1] = 0;
            M[0, 2] = 0;

            M[1, 0] = 0;
            M[1, 1] = y;
            M[1, 2] = 0;

            M[2, 0] = 0;
            M[2, 1] = 0;
            M[2, 2] = 1;

            scale_or_rotate_polygon(M, scalePolygonCheckBox);

            pictureBox.Invalidate();
        }

        private void rotateSegmentButton_Click(object sender, EventArgs e)
        {
            double angle = 90;
            Matrix M = new Matrix(3, 3);

            M[0, 0] = Math.Cos(angle * Math.PI / 180);
            M[0, 1] = Math.Sin(angle * Math.PI / 180);
            M[0, 2] = 0;

            M[1, 0] = -Math.Sin(angle * Math.PI / 180);
            M[1, 1] = Math.Cos(angle * Math.PI / 180);
            M[1, 2] = 0;

            M[2, 0] = 0;
            M[2, 1] = 0;
            M[2, 2] = 1;

            rotate_segment(M);

            pictureBox.Invalidate();
        }

        private void translate_coordinates(double dx, double dy)
        {
            Matrix M = new Matrix(3, 3);

            M[0, 0] = 1;
            M[0, 1] = 0;
            M[0, 2] = 0;

            M[1, 0] = 0;
            M[1, 1] = 1;
            M[1, 2] = 0;

            M[2, 0] = dx;
            M[2, 1] = dy;
            M[2, 2] = 1;

            for (int i = 0; i < segments.Count; ++i)
            {
                Matrix vec = new Matrix(1, 3);
                vec[0, 0] = segments[i].left.X;
                vec[0, 1] = segments[i].left.Y;
                vec[0, 2] = 1;
                vec *= M;
                Point leftP = new Point((int)vec[0, 0], (int)vec[0, 1]);

                vec[0, 0] = segments[i].right.X;
                vec[0, 1] = segments[i].right.Y;
                vec[0, 2] = 1;
                vec *= M;
                PointF rightP = new Point((int)vec[0, 0], (int)vec[0, 1]);
                segments[i] = new Segment(leftP, rightP);
            }

            for (int i = 0; i < polygon.Count; ++i)
            {
                Matrix vec = new Matrix(1, 3);
                vec[0, 0] = polygon[i].X;
                vec[0, 1] = polygon[i].Y;
                vec[0, 2] = 1;
                vec *= M;
                polygon[i] = new PointF((float)vec[0, 0], (float)vec[0, 1]);
            }
        }

        private void scale_or_rotate_polygon(Matrix M, CheckBox checkBox)
        {
            for (int i = 0; i < polygon.Count; ++i)
            {
                PointF translationPoint;
                if (checkBox.Checked)
                {
                    if (pointLocation == Point.Empty)
                        return;
                    translationPoint = pointLocation;
                }
                else
                {
                    translationPoint = new PointF((minPolygonCoord.X + maxPolygonCoord.X) / 2, (minPolygonCoord.Y + maxPolygonCoord.Y) / 2);
                }

                translate_coordinates(-translationPoint.X, -translationPoint.Y);

                Matrix matrix = new Matrix(1, 3);
                matrix[0, 0] = polygon[i].X;
                matrix[0, 1] = polygon[i].Y;
                matrix[0, 2] = 1;
                matrix *= M;
                polygon[i] = new PointF((float)matrix[0, 0], (float)matrix[0, 1]);

                translate_coordinates(translationPoint.X, translationPoint.Y);
            }
        }

        private void rotate_segment(Matrix M)
        {
            for (int i = 0; i < segments.Count; ++i)
            {
                PointF translationPoint = new PointF(
                    (segments[i].left.X + segments[i].right.X) / 2,
                    (segments[i].left.Y + segments[i].right.Y) / 2);

                translate_coordinates(-translationPoint.X, -translationPoint.Y);

                Matrix matrix = new Matrix(1, 3);
                matrix[0, 0] = segments[i].left.X;
                matrix[0, 1] = segments[i].left.Y;
                matrix[0, 2] = 1;
                matrix *= M;
                PointF leftP = new PointF((float)matrix[0, 0], (float)matrix[0, 1]);

                matrix[0, 0] = segments[i].right.X;
                matrix[0, 1] = segments[i].right.Y;
                matrix[0, 2] = 1;
                matrix *= M;
                PointF rightP = new PointF((float)matrix[0, 0], (float)matrix[0, 1]);
                segments[i] = new Segment(leftP, rightP);

                translate_coordinates(translationPoint.X, translationPoint.Y);
            }
        }

        private void findIntersectionsButton_Click(object sender, EventArgs e)
        {
            if (segments.Count == 2)
            {
                float x1 = segments[0].left.X;
                float y1 = segments[0].left.Y;
                float x2 = segments[0].right.X;
                float y2 = segments[0].right.Y;
                float x3 = segments[1].left.X;
                float y3 = segments[1].left.Y;
                float x4 = segments[1].right.X;
                float y4 = segments[1].right.Y;

                float denominator = ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));
                if (denominator == 0)
                {
                    intersectionLabel.Text = "Прямые параллельны или совпадают";
                }
                else
                {
                    float x = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / denominator;
                    float y = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / denominator;

                    intersectionLabel.Text = $"Точка пересения: ({Math.Round(x)}, {Math.Round(y)})";
                }
            }
            else
            {
                intersectionLabel.Text = "Количество отрезков не равно 2";
            }
        }

        private bool IsConvexPolygon(List<PointF> vertices)
        {
            if (vertices.Count < 3)
            {
                return false;
            }

            bool isClockwise = false;
            for (int i = 0; i < 3; i++)
            {
                int j = (i + 1) % vertices.Count;
                int k = (i + 2) % vertices.Count;

                float crossProductZ = (vertices[j].X - vertices[i].X) * (vertices[k].Y - vertices[j].Y) -
                                      (vertices[j].Y - vertices[i].Y) * (vertices[k].X - vertices[j].X);

                if (i == 0)
                {
                    // Определение направления обхода вершин
                    float crossProduct = crossProductZ;
                    isClockwise = crossProductZ > 0;
                }

                if ((crossProductZ > 0) != isClockwise)
                {
                    return false;
                }
            }

            // Проверка оставшихся вершин
            for (int i = 3; i < vertices.Count; i++)
            {
                int j = (i + 1) % vertices.Count;
                int k = (i + 2) % vertices.Count;

                float crossProductZ = (vertices[j].X - vertices[i].X) * (vertices[k].Y - vertices[j].Y) -
                                      (vertices[j].Y - vertices[i].Y) * (vertices[k].X - vertices[j].X);

                if ((crossProductZ > 0) != isClockwise)
                {
                    return false;
                }
            }

            return true;
        }
        private bool IsPointInPolygon(PointF point, List<PointF> vertices)
        {
            // Проверка количества вершин
            if (vertices.Count < 3)
            {
                return false;
            }

            bool isInside = false;

            int j = vertices.Count - 1;
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].Y < point.Y && vertices[j].Y >= point.Y ||
                    vertices[j].Y < point.Y && vertices[i].Y >= point.Y)
                {
                    if (vertices[i].X + (point.Y - vertices[i].Y) / (vertices[j].Y - vertices[i].Y) * (vertices[j].X - vertices[i].X) < point.X)
                    {
                        isInside = !isInside;
                    }
                }

                j = i;
            }

            return isInside;
        }
        private string PointPositionRelativeToSegment(PointF p1, PointF p2, PointF targetPoint)
        {
            float D = (targetPoint.X - p1.X) * (p2.Y - p1.Y) - (targetPoint.Y - p1.Y) * (p2.X - p1.X);
            if (Math.Abs(D) < 500)
                return "на прямой, содержащей отрезок";
            else if (D < 0)
                return "слева";
            else if (D > 0)
                return "справа";
            else
                throw new Exception();
        }
    }
}

class Segment
{
    public PointF left, right;

    public Segment()
    {
        left = new PointF();
        right = new PointF();
    }

    public Segment(PointF l, PointF r)
    {
        left = l;
        right = r;
    }
}

class Matrix
{
    private double[,] data;
    private int m;
    private int n;
    public int M { get => m; }
    public int N { get => n; }

    public Matrix(int m, int n)
    {
        this.m = m;
        this.n = n;
        data = new double[m, n];
    }

    public void ApplyActionOverMatrix(Action<int, int> func)
    {
        for (var i = 0; i < M; i++)
            for (var j = 0; j < N; j++)
                func(i, j);
    }

    public double this[int x, int y]
    {
        get { return data[x, y]; }
        set { data[x, y] = value; }
    }

    public static Matrix operator *(Matrix matrix, double value)
    {
        Matrix result = new Matrix(matrix.M, matrix.N);
        result.ApplyActionOverMatrix((i, j) => result[i, j] = matrix[i, j] * value);
        return result;
    }

    public static Matrix operator *(Matrix matrix, Matrix matrix2)
    {
        if (matrix.N != matrix2.M)
        {
            throw new ArgumentException("matrixes can not be multiplied");
        }
        var result = new Matrix(matrix.M, matrix2.N);
        result.ApplyActionOverMatrix((i, j) =>
        {
            for (var k = 0; k < matrix.N; k++)
            {
                result[i, j] += matrix[i, k] * matrix2[k, j];
            }
        });
        return result;
    }
}