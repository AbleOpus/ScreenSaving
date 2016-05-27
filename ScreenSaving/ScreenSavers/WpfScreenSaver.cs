using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using ScreenSaving.Presentation;

namespace ScreenSaving.ScreenSavers
{
    /// <summary>
    /// Facilitates Windows Presentation Foundation based screen savers.
    /// </summary>
    /// <typeparam name="TScreenSaver">The <see cref="Window"/> used to display the screen saver visuals.</typeparam>
    /// <typeparam name="TSettings">The <see cref="Window"/> used to show settings.</typeparam>
    internal sealed class WpfScreenSaver<TScreenSaver, TSettings> : StandardScreenSaver
        where TScreenSaver : ScreenSaverWindow, new()
        where TSettings : Window, new()
    {
        private WpfScreenSaver()
        {
        }

        /// <summary>
        /// Runs a screen saver from the given arguments and <see cref="Window"/> types.
        /// </summary>
        public static void Run(string[] args)
        {
            var screenSaver = new WpfScreenSaver<TScreenSaver, TSettings>();

            try
            {
                screenSaver.Initialize(args);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, System.Windows.Forms.Application.ProductName, 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Invoked when user input has been detected through the mouse or keyboard.
        /// </summary>
        protected override void OnActivityDetected()
        {
            Dispose();
            Application.Current.Shutdown();
        }

        private static IntPtr GetWindowHandle(Window window)
        {
            return new WindowInteropHelper(window).Handle;
        }

        /// <summary>
        /// Invoked when a preview command line switch (\p) is specified.
        /// </summary>
        /// <param name="previewWindow">Facilitates previewing the screen saver.</param>
        protected override void OnShowPreview(PreviewWindow previewWindow)
        {
            //var windowScreenSaver = new TScreenSaver();
            //previewWindow.Child = new WindowInteropHelper(windowScreenSaver).Handle;
            //// The drawing of this window is, by default, per screen,
            //// and needs to be aware of previewing.
            //windowScreenSaver.IsPreview = true;
            //Application.Current.Run(windowScreenSaver);

            var windowScreenSaver = new TScreenSaver();
            Rectangle rect;
            NativeMethods.GetClientRect(previewWindow.Handle, out rect);
            previewWindow.Child = GetWindowHandle(windowScreenSaver);
            // The drawing of this window is, by default, per screen,
            // and needs to be aware of previewing.
            windowScreenSaver.IsPreview = true;
            Application.Current.Run(windowScreenSaver);
        }

        /// <summary>
        /// Invoked when a configuration command line switch (\c) is specified.
        /// </summary>
        protected override void OnShowConfiguration()
        {
            if (typeof (TSettings) == typeof (NoSettingsWindow))
            {
                ShowNoConfigurationsMessage();
            }
            else
            {
                var dialogSettings = new TSettings();
                dialogSettings.ShowDialog();
                dialogSettings.Close();
            }
        }

        /// <summary>
        /// Invoked when a screen saver command line switch (\s) is specified.
        /// </summary>
        /// <param name="displayBounds">The bounds of the entire display. Facilitates multi-screen support.</param>
        protected override void OnShowScreenSaver(Rectangle displayBounds)
        {
            base.OnShowScreenSaver(displayBounds);
          //  var windowScreenSaver = new TScreenSaver();
          //  Application.Current.Run(windowScreenSaver);
        }
    }

}
