using System;
using System.Windows;

namespace ScreenSaving.Presentation
{
    /// <summary>
    /// Represents a window form screen saving.
    /// </summary>
    public class ScreenSaverWindow : Window
    {
        /// <summary>
        /// Gets or sets a value indicating whether the screen saver will show as a preview.
        /// </summary>
        public bool IsPreview { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenSaverWindow"/> class.
        /// </summary>
        public ScreenSaverWindow()
        {
            Topmost = true;
            WindowStyle = WindowStyle.None;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.FrameworkElement.Initialized"/> event. 
        /// This method is invoked whenever <see cref="P:System.Windows.FrameworkElement.IsInitialized"/> is set to true internally. 
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.RoutedEventArgs"/> that contains the event data.</param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            var bounds = Display.GetBounds();
            Left = bounds.Left;
            Top = bounds.Top;
            Width = bounds.Width;
            Height = bounds.Height;
            ResizeMode = ResizeMode.NoResize;
        }
    }
}
