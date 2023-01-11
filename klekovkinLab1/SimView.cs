using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
namespace klekovkinLab1
{    
    public partial class SimView : Form
    {
        Random rand = new Random();
        Graphics g;
        Graphics lidarG;
        Bitmap fieldBM;
        Bitmap lidarBM;
        Pen mainPen = new Pen(Color.Black, 2);
        public bool modelTimTick = false;
        public readonly Matrix3x2 basisToWindow;
        public readonly Matrix3x2 basisToLidarW;
        public readonly Matrix3x2 basisToLidarWSt;

        Vector2 manObsCenter;
        bool drawObsFlag = false;
        float manObsRad = 0;

        public int FieldWidth { get => fieldView.Width;}
        public int FieldHeight { get => fieldView.Height;}
        public int LidarWidth { get => lidarView.Width; }
        public int LidarHeight { get => lidarView.Height; }

        
        public SimView()
        {
            InitializeComponent();
            fieldBM = new Bitmap(fieldView.Width, fieldView.Height);
            lidarBM = new Bitmap(lidarView.Width, lidarView.Height);
            lidarG = Graphics.FromImage(lidarBM);
            g = Graphics.FromImage(fieldBM);
            basisToWindow = new Matrix3x2(1, 0,
                                          0, -1,
                                          0, fieldView.Height);

            basisToLidarW = new Matrix3x2(0.5f, 0,
                                          0, -0.5f,
                                          0, lidarView.Height);
            basisToLidarWSt = new Matrix3x2(0.5f, 0,
                                          0, -0.5f,
                                          0, lidarView.Height);
        }

        void DrawCoordsLine()
        {
            Point zero = Vector2.Transform(new Vector2(0, 0), basisToWindow).toPoint();
            Point axisX = Vector2.Transform(new Vector2(100, 0), basisToWindow).toPoint();
            Point axisY = Vector2.Transform(new Vector2(0, 100), basisToWindow).toPoint();
            DrawLine(zero, axisX, new Pen(Color.Red, 5));
            DrawLine(zero, axisY, new Pen(Color.Green, 5));
        }

        public void DrawLidarP(Point point, int radius, Pen pen)
        {
            lidarG.DrawEllipse(pen, point.X - radius, point.Y - radius, 2 * radius, 2 * radius);
        }

        public void DrawLidarLine(Point begin, Point end, Pen pen)
        {
            lidarG.DrawLine(pen, begin, end);   
        }

        public void DrawLidarLine(Vector2 begin, Vector2 end, Pen pen)
        {
            lidarG.DrawLine(pen, begin.toPoint(), end.toPoint());
        }

        public void DrawCurcle(Point centalPt, int radius, Pen pen)
        {
            g.DrawEllipse(pen, centalPt.X - radius, centalPt.Y - radius, 2 * radius, 2 * radius);
        }

        public void DrawCurcle(PointF centalPt, int radius, Pen pen)
        {
            g.DrawEllipse(pen, centalPt.X - radius, centalPt.Y - radius, 2 * radius, 2 * radius);
        }

        public void DrawCurcle(Vector2 centalPt, int radius, Pen pen)
        {
            g.DrawEllipse(pen, centalPt.X - radius, centalPt.Y - radius, 2 * radius, 2 * radius);
        }


        public void DrawLine(Point beginPt, Point endPt, Pen pen)
        {
            g.DrawLine(pen, beginPt, endPt);
        }

        public void DrawLine(PointF beginPt, PointF endPt, Pen pen)
        {
            g.DrawLine(pen, beginPt, endPt);
        }

        public void DrawLine(Vector2 beginPt, Vector2 endPt, Pen pen)
        {
            Point _beginPt = new Point((int)beginPt.X, (int)beginPt.Y);
            Point _endPt = new Point((int)endPt.X, (int)endPt.Y);
            g.DrawLine(pen, _beginPt, _endPt);
        }

        public void DrawPolygon(Point[] points, Pen pen)
        {
            g.DrawPolygon(pen, points);
        }

        public void DrawPolygon(PointF[] points, Pen pen)
        {
            g.DrawPolygon(pen, points);
        }

        public void DrawPolygon(Vector2[] points, Pen pen)
        {
            Point[] _points = new Point[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                _points[i].X = (int)points[i].X;
                _points[i].Y = (int)points[i].Y;
            }
            g.DrawPolygon(pen, _points);
        }

        private void generateBtn_click(object sender, EventArgs e)
        {
            genBtnClick();
            //g.Clear(Color.White);
            //int x = rand.Next(0, fieldView.Width);
            //int y = rand.Next(0, fieldView.Height);
            //int rad = rand.Next(1, 50);
            //DrawCurcle(new Point(x, y), rad, mainPen);
        }

        private void manualCB_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            changeControlMode(0, checkBox.Checked);
        }

        private void modelUpddateTim_Tick(object sender, EventArgs e)
        {
            tick();
        }

        private void viewUpdateTim_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            lidarG.Clear(Color.White);
            viewTick();            
            DrawCoordsLine();
            DrawLidarLine(Vector2.Transform(new Vector2(lidarView.Width - 30, lidarView.Height / 3), basisToLidarWSt).toPoint(),
                Vector2.Transform(new Vector2(lidarView.Width + 30, lidarView.Height / 3), basisToLidarWSt).toPoint(),
                new Pen(Color.Black, 3));
            DrawLidarLine(Vector2.Transform(new Vector2(lidarView.Width, lidarView.Height / 3 + 10), basisToLidarWSt).toPoint(),
                Vector2.Transform(new Vector2(lidarView.Width, lidarView.Height / 3), basisToLidarWSt).toPoint(),
                new Pen(Color.Black, 3));
            if (drawObsFlag)
            {
                Pen tempObsPen = new Pen(Color.Black, 2);
                tempObsPen.DashStyle = DashStyle.DashDot;
                DrawCurcle(manObsCenter, (int)manObsRad, tempObsPen);
            }
            //DrawLidarP(Vector2.Transform(new Vector2(100, 100), basisToLidarW).toPoint(), 3, new Pen(Color.Blue, 3));
            fieldView.Image = fieldBM;
            lidarView.Image = lidarBM;
        }

        public new void Show()
        {
            Application.Run(this);
        }

        public delegate void modelTickContainer();
        public delegate void KeyDownContainer(object sender, KeyEventArgs e);
        public delegate void KeyUpContainer(object sender, KeyEventArgs e);
        public delegate void genBtnCLickContainer();
        public delegate void viewTickContainer();
        public delegate void cleanBtnClkContainer();
        public delegate void SetTgtPoint(Vector2 point);
        public delegate void AddManObstacle(Vector2 center, float rad);
        public delegate void ChangeControlMode(int robotId, bool manualControl);

        public event cleanBtnClkContainer cleanBtnClick;
        public event viewTickContainer viewTick;
        public event genBtnCLickContainer genBtnClick; 
        public event KeyDownContainer keyDown;
        public event KeyUpContainer keyUp;
        public event modelTickContainer tick;
        public event SetTgtPoint setTgtPoint;
        public event AddManObstacle addManObstacle;
        public event ChangeControlMode changeControlMode;

        private void SimView_KeyDown(object sender, KeyEventArgs e)
        {
            keyDown(sender, e);
        }

        private void SimView_KeyUp(object sender, KeyEventArgs e)
        {
            keyUp(sender, e);
        }

        private void fieldView_MouseMove(object sender, MouseEventArgs e)
        {
            //DrawCurcle(new Point(e.X, e.Y), 5, new Pen(Color.Magenta, 2));
            if (drawObsFlag)
            {
                manObsRad = (manObsCenter - new Vector2(e.X, e.Y)).Length();
            }
        }

        private void fieldView_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button){
                case MouseButtons.Left:
                    setTgtPoint(new Vector2(e.X, e.Y));
                    break;
                case MouseButtons.Right:
                    drawObsFlag = true;
                    manObsRad = 0;
                    manObsCenter = new Vector2(e.X, e.Y);
                    break;
                default:
                    break;
            }
        }

        private void fieldView_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    break;
                case MouseButtons.Right:
                    drawObsFlag = false;
                    addManObstacle(manObsCenter,
                        (manObsCenter - new Vector2(e.X, e.Y)).Length());
                    break;
                default:
                    break;
            }
        }

        private void cleanBtn_Click(object sender, EventArgs e)
        {
            cleanBtnClick();
        }
    }
}