using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ScreenSaving.Forms
{
    /// <summary>
    /// Represents a basic screen saver.
    /// </summary>
    public partial class ScreenSaverForm : Form
    {
        /// <summary>
        /// Gets or sets a value indicating whether the screen saver will show as a preview.
        /// </summary>
        [Browsable(false)]
        public bool IsPreview { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenSaverForm"/> class.
        /// </summary>
        public ScreenSaverForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
#if DEBUG
            TopMost = false;
#endif
        }
    }
}
