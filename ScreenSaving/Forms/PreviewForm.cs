using System;
using System.Windows.Forms;

namespace ScreenSaving.Forms
{
    /// <summary>
    /// Represents a Form to preview the screen saver while it is in preview mode.
    /// </summary>
    internal partial class PreviewForm : Form
    {
        /// <summary>
        /// Gets the handle of the control that is to house the screen saver.
        /// </summary>
        public IntPtr PreviewAreaHandle => panel.Handle;

        public PreviewForm()
        {
            InitializeComponent();
        }
    }
}
