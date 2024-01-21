using System.Configuration.Internal;
using System.Globalization;
using System.Windows;
using System.Windows.Media;


namespace _Chart2D
{
    internal class Axis
    {
        double width, height;
        Pen pen;

        public Axis(double w, double h)
        {
            width = w;
            height = h;

            pen = new Pen(Brushes.White, 1);
        }

        public void Draw(DrawingContext dc, DrawingVisual visual, Point mouse)
        {
            // axis X
            dc.DrawLine(pen, new Point(0, height / 2), new Point(width, height / 2));

            // axis Y
            dc.DrawLine(pen, new Point(width / 2, 0), new Point(width / 2, height));

            // X ortha text
            FormattedText formattedText = new FormattedText("X", CultureInfo.GetCultureInfo("en-us"),
                                                FlowDirection.LeftToRight, new Typeface("Verdana"), 12, Brushes.White,
                                                VisualTreeHelper.GetDpi(visual).PixelsPerDip);
            Point textPos = new Point(width - 14, height / 2 - 20);
            dc.DrawText(formattedText, textPos);

            // Y ortha text
            formattedText = new FormattedText("Y", CultureInfo.GetCultureInfo("en-us"),
                                                FlowDirection.LeftToRight, new Typeface("Verdana"), 12, Brushes.White,
                                                VisualTreeHelper.GetDpi(visual).PixelsPerDip);
            textPos = new Point(width / 2 + 6, 6);
            dc.DrawText(formattedText, textPos);

            // Coord. position text
            var x = Math.Round(ToX(mouse.X));
            var y = Math.Round(ToY(mouse.Y));

            var text = "(" + x + ", " + y + ")";
            formattedText = new FormattedText(text, CultureInfo.GetCultureInfo("en-us"),
                                                            FlowDirection.LeftToRight, new Typeface("Verdana"), 12, Brushes.White,
                                                            VisualTreeHelper.GetDpi(visual).PixelsPerDip);
            textPos = new Point(mouse.X, mouse.Y - 12);
            dc.DrawText(formattedText, textPos);
        }

        public double ToX(double x) => x - width / 2;
        public double ToY(double y) => (y - height / 2) * -1;
    }
}
