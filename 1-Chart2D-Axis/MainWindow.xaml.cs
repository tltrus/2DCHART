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
        DrawingVisual visual;
        DrawingContext dc;
        double width, height;
        Axis axis;
        Point mouse;

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

            Drawing();
        }

        private void g_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = e.GetPosition(g);
            Drawing();
        }

        private void Drawing()
        {
            g.RemoveVisual(visual);
            using (dc = visual.RenderOpen())
            {
                // axis drawing
                axis.Draw(dc, visual, mouse);

                dc.Close();
                g.AddVisual(visual);
            }
        }
    }
}