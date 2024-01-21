using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _Chart2D
{
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer;
        DrawingVisual visual;
        DrawingContext dc;
        double width, height;
        Axis axis;
        Point mouse;
        int factor = 25;


        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            width = g.Width;
            height = g.Height;

            mouse = new Point();
            visual = new DrawingVisual();

            axis = new Axis(width, height);


            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            //timer.Start();
        }

        private void g_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = e.GetPosition(g);
            Drawing();
        }
        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                factor -= 1;
            }
            else
            {
                // increasing
                factor += 1;
            }

            if (factor <= 0) factor = 1;
            if (factor >= 58) factor = 58;

            Drawing();
        }
        private void Drawing()
        {
            g.RemoveVisual(visual);
            using (dc = visual.RenderOpen())
            {
                // axis drawing
                axis.Draw(dc, visual);

                axis.SetFactor(factor);

                // func
                double y = 0;
                PointCollection points = new PointCollection();

                for (int i = -360; i < 360; i += 5)
                {
                    double x = i * Math.PI / 180;
                    y = Math.Sin(x);
                    points.Add(new Point(axis.Xto(x), axis.Yto(y)));
                }

                StreamGeometry streamGeometry = new StreamGeometry();
                using (StreamGeometryContext geometryContext = streamGeometry.Open())
                {
                    Point startpoint = points[0];
                    points.RemoveAt(0);
                    geometryContext.BeginFigure(startpoint, false, false);
                    geometryContext.PolyLineTo(points, true, true);
                }
                dc.DrawGeometry(null, new Pen(Brushes.Red, 2), streamGeometry);



                dc.Close();
                g.AddVisual(visual);
            }
        }
        private void timerTick(object sender, EventArgs e) => Drawing();
    }
}