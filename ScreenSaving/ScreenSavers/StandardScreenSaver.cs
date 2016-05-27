using System;
using System.Drawing;
using System.Windows.Forms;
using ScreenSaving.UserInput;

namespace ScreenSaving.ScreenSavers
{
    /// <summary>
    /// Represents a standard screen saver that detects user input from keyboard and mouse,
    /// and hides the cursor.
    /// </summary>
    public abstract class StandardScreenSaver : BaseScreenSaver, IDisposable
    {
        /// <summary>
        /// Gets or sets the message to show when no configurations are available.
        /// </summary>
        protected string NoConfigurationsMessage { get; set; } = 
            "This screen saver has no options that you can set.";

        /// <summary>
        /// Gets the input detector for this screen saver.
        /// </summary>
        protected ActivityDetector ActivityDetector { get; private set; }

        /// <summary>
        /// Shows a message indicating that no settings are available.
        /// </summary>
        protected void ShowNoConfigurationsMessage()
        {
            MessageBox.Show(NoConfigurationsMessage,
                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Invoked when a screen saver command line switch (\s) is specified.
        /// </summary>
        /// <param name="displayBounds">The bounds of the entire display. Facilitates multi-screen support.</param>
        /// <exception cref="InvalidOperationException"></exception>
        protected override void OnShowScreenSaver(Rectangle displayBounds)
        {
            Cursor.Hide();

            if (ActivityDetector != null)
            {
                throw new InvalidOperationException(
                    "Activity detector already instantiated. OnShowScreenSaver should only be called once.");
            }

            ActivityDetector = new ActivityDetector();
            ActivityDetector.Detected += delegate
            {
                OnActivityDetected();
            };
            ActivityDetector.Enabled = true;
        }

        /// <summary>
        /// Invoked when user input has been detected through the mouse or keyboard.
        /// </summary>
        protected abstract void OnActivityDetected();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public virtual void Dispose()
        {
            ActivityDetector?.Dispose();
        }
    }
}
