using System;
using System.Windows.Forms;

namespace ScreenSaving.Forms
{
    /// <summary>
    /// Represents a Form which specifies that no settings are to be shown.
    /// This Form is not actually shown, but rather is used to indicate that
    /// no settings is available.
    /// </summary>
    public class NoSettingsForm : Form
    {
        /// <summary>
        /// Not used with this class.
        /// </summary>
        [Obsolete("This class is not meant to be instantiated.", true)]
        public NoSettingsForm()
        {
            throw new NotImplementedException();
        }
    }
}
