using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenSaving.UserInput
{
    /// <summary>
    /// Automatically detects user input activity.
    /// </summary>
    public class ActivityDetector : IDisposable
    {
        private readonly MouseHooker mouseHooker = new MouseHooker();
        private readonly KeyboardHooker keyboardHooker = new KeyboardHooker();
        private readonly Timer resetMoveDistTimer = new Timer();

        /// <summary>
        /// Gets the point that is measured against the current cursor position.
        /// </summary>
        public Point StartMovePoint { get; private set; }

        /// <summary>
        /// Gets or sets how far, in pixels, the mouse has to move within 1 second,
        /// to raise the <see cref="Detected"/> event.
        /// </summary>
        public int MouseMoveThreshold { get; set; } = 40;

        private bool enabled;
        /// <summary>
        /// Gets or sets a value indicating whether to detect input.
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;

                if (enabled)
                {
                    mouseHooker.Hook();
                    keyboardHooker.Hook();
                }
                else
                {
                    mouseHooker.Unhook();
                    keyboardHooker.Unhook();
                }
            }
        }

        /// <summary>
        /// Occurs when the mouse cursor has moved.
        /// </summary>
        public event EventHandler MouseMove
        {
            add { mouseHooker.MouseMove += value; }
            remove { mouseHooker.MouseMove -= value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityDetector"/> class.
        /// </summary>
        public ActivityDetector()
        {
            SetStartMovePoint();
            resetMoveDistTimer.Interval = 1000;
            resetMoveDistTimer.Tick += delegate{ SetStartMovePoint(); };
            mouseHooker.CallNextHook = false;
            keyboardHooker.CallNextHook = false;

            mouseHooker.MouseMove += delegate
            {
                resetMoveDistTimer.Restart();

                if (Cursor.Position.DistanceTo(StartMovePoint) >= MouseMoveThreshold)
                {
                    OnDetected();
                    SetStartMovePoint();
                }
            };

            mouseHooker.MouseButton += delegate { OnDetected(); };
            keyboardHooker.KeyDown += delegate { OnDetected(); };
        }

        private void SetStartMovePoint()
        {
            StartMovePoint = Cursor.Position;
        }

        /// <summary>
        /// Occurs when significant user input has been detected.
        /// </summary>
        public event EventHandler Detected;
        /// <summary>
        /// Raises the <see cref="Detected"/> event.
        /// </summary>
        protected virtual void OnDetected()
        {
            Detected?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            mouseHooker.Dispose();
            keyboardHooker.Dispose();
            resetMoveDistTimer.Dispose();
        }
    }
}
