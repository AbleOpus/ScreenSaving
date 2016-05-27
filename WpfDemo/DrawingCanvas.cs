using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using FlowDirection = System.Windows.FlowDirection;

namespace WpfDemo
{
    class DrawingCanvas : Canvas
    {
        private readonly Pen pen = new Pen(Brushes.Lime, 20);
        private readonly Typeface typeFace = new Typeface(new FontFamily("Arial"), default(FontStyle), default(FontWeight), default(FontStretch));

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (((MainWindow)Parent).IsPreview)
            {
                drawingContext.DrawText(new FormattedText("Preview", CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight, typeFace, 30, Brushes.Lime), new Point(20, 20));
            }
            else if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                double inset = pen.Thickness;

                foreach (var screen in Screen.AllScreens)
                {
                    var location = PointFromScreen(new Point(screen.Bounds.X, screen.Bounds.Y));

                    Rect rect = new Rect(location.X + inset / 2, location.Y + inset / 2,
                        screen.Bounds.Width - inset, screen.Bounds.Height - inset);

                    drawingContext.DrawRectangle(Brushes.Transparent, pen, rect);

                    drawingContext.DrawText(new FormattedText(screen.DeviceName, CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight, typeFace, 30, Brushes.Lime),
                        new Point(location.X + 100, location.Y + 100));
                }
            }
        }
    }
}
