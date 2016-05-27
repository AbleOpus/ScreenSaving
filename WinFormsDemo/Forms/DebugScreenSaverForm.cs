using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ScreenSaving.Forms;

namespace WinFormsDemo.Forms
{
    /// <summary>
    /// Represents a <see cref="Form"/> to be shown as an easily debugged screen saver.
    /// </summary>
    public partial class DebugScreenSaverForm : ScreenSaverForm
    {
        private readonly Font previewFont = new Font("Arial", 13, FontStyle.Regular);
        private readonly Font screenFont = new Font("Arial", 25, FontStyle.Regular);
        private readonly Brush foreBrush = Brushes.LimeGreen;
        private const float BORDER_SCALE = 0.02f;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugScreenSaverForm"/> class.
        /// </summary>
        public DebugScreenSaverForm()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (IsPreview) // Draw preview info
            {
                DrawStringCentered(e.Graphics, $"Preview\n{Size.Width}x{Size.Height}", previewFont, Bounds, foreBrush);
                DrawInsetBorder(e.Graphics, ClientRectangle, Height * BORDER_SCALE, foreBrush);
            }
            else
            {
                DrawScreenInfo(e.Graphics);
            }
        }

        private void DrawScreenInfo(Graphics graphics)
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                Point screenPos = screen.Bounds.Location;
                screenPos = PointToClient(screenPos);
                var rect = new Rectangle(screenPos, screen.Bounds.Size);
                DrawInsetBorder(graphics, rect, Height * BORDER_SCALE, foreBrush);
                var SB = new StringBuilder();

                if (Settings.Default.ShowScreenID)
                    SB.AppendLine(screen.DeviceName);

                if (Settings.Default.ShowScreenBounds)
                    SB.AppendLine(screen.Bounds.ToString());

                DrawStringCentered(graphics, SB.ToString(), screenFont, screen.Bounds, foreBrush);
            }
        }

        /// <summary>
        /// Draws the specified caption in the center of the specified <see cref="Rectangle"/>.
        /// </summary>
        private void DrawStringCentered(Graphics graphics, string caption, Font font, Rectangle rect, Brush brush)
        {
            if (String.IsNullOrWhiteSpace(caption)) return;
            var textSize = graphics.MeasureString(caption, font);
            float x = rect.X + rect.Width / 2f - textSize.Width / 2f;
            float y = rect.Y + rect.Height / 2f - textSize.Height / 2f;
            Point point = PointToClient(new Point((int)x, (int)y));
            graphics.DrawString(caption, font, brush, point);
        }

        // A work around a Pen bug. Screen borders drawn with a pen will be clipped on the primary monitor.
        /// <summary>
        /// Draws an inset border around the specified <see cref="Rectangle"/>.
        /// </summary>
        private static void DrawInsetBorder(Graphics graphics, Rectangle rect, float width, Brush brush)
        {
            graphics.FillRectangle(brush, rect.X, rect.Y, rect.Width, width);              // Top 
            graphics.FillRectangle(brush, rect.Right - width, rect.Y, width, rect.Height); // Right
            graphics.FillRectangle(brush, rect.X, rect.Bottom - width, rect.Width, width); // Bottom
            graphics.FillRectangle(brush, rect.X, rect.Y, width, rect.Height);             // Left
        }
    }
}
