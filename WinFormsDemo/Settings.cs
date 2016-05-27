using ScreenSaving;
using System;
using System.Drawing;

namespace WinFormsDemo
{
    /// <summary>
    /// Represents user settings for the <see cref="WinFormsDemo"/> application.
    /// </summary>
    [Serializable]
    class Settings : SettingsBase<Settings>
    {
        /// <summary>
        /// Gets or sets the foreground <see cref="Color"/> used to display
        /// debug information.
        /// </summary>
        public Color ForeColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show screen identifiers.
        /// </summary>
        public bool ShowScreenID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show screen bounds.
        /// </summary>
        public bool ShowScreenBounds { get; set; }

        public override void Reset()
        {
            ForeColor = Color.YellowGreen;
            ShowScreenID = true;
            ShowScreenBounds = true;
        }
    }
}
