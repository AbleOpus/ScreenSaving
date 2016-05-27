using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ScreenSaving.UserInput;

namespace ScreenSaving.Forms
{
    /// <summary>
    /// Represents a Form for testing the mouse move aspect of the <see cref="ActivityDetector"/>.
    /// </summary>
    public sealed partial class MouseActivityTestForm : Form
    {
        private readonly ActivityDetector detector = new ActivityDetector();
        private readonly Timer timerUpdate = new Timer();
        private readonly Pen distancePen = new Pen(Color.DarkRed, 1f);
        private readonly Pen thresholdPen = new Pen(Color.DarkRed, 2f);
        /// <summary>
        /// The counter which determines whether to show the detected caption. If it is greater
        /// than 0, the caption will be shown.
        /// </summary>
        private int showDetectedCaptionTicks;

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseActivityTestForm"/> class.
        /// </summary>
        public MouseActivityTestForm()
        {
            InitializeComponent();
            detector.Detected += delegate { showDetectedCaptionTicks = 5; };
            thresholdPen.DashStyle = DashStyle.Dash;
            timerUpdate.Tick += TimerUpdateOnTick;
            timerUpdate.Interval = 20;
            timerUpdate.Start();
            detector.Enabled = true;
            numberBoxMoveThresh.Value = detector.MouseMoveThreshold;
        }

        private void TimerUpdateOnTick(object sender, EventArgs e)
        {
            Invalidate(false);

            if (showDetectedCaptionTicks > 0)
                showDetectedCaptionTicks--;
        }

        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data. </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawCursorInfo(e.Graphics);

            if (showDetectedCaptionTicks > 0)
                e.Graphics.DrawString("Detected", Font, Brushes.DarkGreen, 10, 10);
        }

        private void DrawCursorInfo(Graphics graphics)
        {
            const int DIAMETER = 20;
            Point startPos = PointToClient(detector.StartMovePoint);

            // Draw start pos.
            graphics.FillEllipse(Brushes.DarkRed, startPos.X - DIAMETER / 2, startPos.Y - DIAMETER / 2,
                DIAMETER, DIAMETER);

            // Draw distance from start pos.
            float distance = Cursor.Position.DistanceTo(detector.StartMovePoint);
            graphics.DrawEllipse(distancePen, startPos.X - distance,
                startPos.Y - distance, distance * 2, distance * 2);

            // Draw distance threshold.
            graphics.DrawEllipse(thresholdPen, startPos.X - detector.MouseMoveThreshold, startPos.Y
                - detector.MouseMoveThreshold, detector.MouseMoveThreshold * 2, detector.MouseMoveThreshold * 2);
        }

        private void numberBoxMoveThresh_ValueChanged(object sender, EventArgs e)
        {
            detector.MouseMoveThreshold = (int)numberBoxMoveThresh.Value;
        }
    }
}
