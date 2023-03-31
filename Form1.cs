namespace Paint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private class Points
        {
            private int index = 0;
            private Point[] points;
            public Points(int size)
            {
                if (size <= 0) { size = 2; }
                points = new Point[size];
            }
            public void SetPoint(int x, int y)
            {
                if (index >= points.Length)
                    index = 0;
                points[index] = new Point(x, y);
                index++;
            }
            public void ResetPoints()
            {
                index = 0;
            }
            public int GetCountOfpoints()
            {
                return index;
            }
            public Point[] GetPoints()
            {
                return points;
            }
        }
        private bool isMouse = false;
        private Points pointArray = new Points(2);
        Bitmap bitmap = new(100, 100);
        private Bitmap previousBitmap;
        Graphics graphics;
        Pen pen = new(Color.Black);
        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            bitmap = new(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(bitmap);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }
        private Bitmap GetPreviousBitmap()
        {
            return previousBitmap;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
            pointArray.ResetPoints();
        }

        private Graphics GetGraphics()
        {
            return graphics;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e, Graphics graphics)
        {
            if (!isMouse) { return; }
            pointArray.SetPoint(e.X, e.Y);
            if (pointArray.GetCountOfpoints() >= 2)
            {
                graphics.DrawLines(pen, pointArray.GetPoints());
                pictureBox1.Image = bitmap;
                pointArray.SetPoint(e.X, e.Y);
            }

            previousBitmap = (Bitmap)bitmap.Clone();

            if (!isMouse) { return; }
            pointArray.SetPoint(e.X, e.Y);
            if (pointArray.GetCountOfpoints() >= 2)
            {
                graphics.DrawLines(pen, pointArray.GetPoints());
                pictureBox1.Image = bitmap;
                pointArray.SetPoint(e.X, e.Y);
            }
        }

        private void ColorBtn_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new();
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = bitmap;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new();
            save.DefaultExt = ".png";
            save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (save.ShowDialog() == DialogResult.OK)
                pictureBox1.Image.Save(save.FileName);
        }
    }
}