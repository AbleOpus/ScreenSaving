using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using ScreenSaving.Forms;

namespace ScreenSaving.ScreenSavers
{
    /// <summary>
    /// Facilitates Windows.Forms based screen savers.
    /// </summary>
    /// <typeparam name="TScreenSaver">The <see cref="ScreenSaverForm"/> used to display the screen saver visuals.</typeparam>
    /// <typeparam name="TSettings">The <see cref="Form"/> used to show settings.</typeparam>
    public sealed class WinFormsScreenSaver<TScreenSaver, TSettings> : StandardScreenSaver
        where TScreenSaver : ScreenSaverForm, new() where TSettings : Form, new()
    {
        private readonly TScreenSaver formScreenSaver = new TScreenSaver();

        private WinFormsScreenSaver(){ } 

        /// <summary>
        /// Runs a screen saver from the given arguments and <see cref="Form"/> types.
        /// </summary>
        public static void Run(string[] args)
        {
#if DEBUG
            StringBuilder SB = new StringBuilder();
            SB.AppendLine("Command line args:");

            foreach (string s in args)
                SB.AppendLine(s);

            Debug.WriteLine(SB.ToString());
#endif

            var screenSaver = new WinFormsScreenSaver<TScreenSaver, TSettings>();
            
            try
            {
                screenSaver.Initialize(args);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Invoked when user activity has been detected through the mouse or keyboard.
        /// </summary>
        protected override void OnActivityDetected()
        {
            Dispose();
            Application.Exit();
        }

        /// <summary>
        /// Invoked when a preview command line switch (\p) is specified.
        /// </summary>
        /// <param name="previewWindow">Facilitates previewing the screen saver.</param>
        protected override void OnShowPreview(PreviewWindow previewWindow)
        {
            formScreenSaver.IsPreview = true;
#if DEBUG
            PreviewForm formPreview = new PreviewForm();
            PreviewWindow window = new PreviewWindow(formPreview.PreviewAreaHandle);
            window.Child = formScreenSaver.Handle;
            formScreenSaver.Show();
            Application.Run(formPreview);
#else
            Rectangle rect;
            NativeMethods.GetClientRect(previewWindow.Handle,out rect);
            previewWindow.Child = formScreenSaver.Handle;
            Application.Run(formScreenSaver);
#endif
        }

        /// <summary>
        /// Invoked when a screen saver command line switch (\s) is specified.
        /// </summary>
        /// <param name="displayBounds">The bounds of the entire display. Facilitates multi-screen support.</param>
        protected override void OnShowScreenSaver(Rectangle displayBounds)
        {
            base.OnShowScreenSaver(displayBounds);
            formScreenSaver.Bounds = displayBounds;
            // When the display settings change, automatically resize screen saver.
            SystemEvents.DisplaySettingsChanged += delegate
            {
                if (!formScreenSaver.IsDisposed)
                    formScreenSaver.Bounds = displayBounds;
            };
            Application.Run(formScreenSaver);
        }

        /// <summary>
        /// Invoked when a configuration command line switch (\c) is specified.
        /// </summary>
        protected override void OnShowConfiguration()
        {
            if (typeof (TSettings) == typeof (NoSettingsForm))
            {
                MessageBox.Show("This screen saver has no options that you can set.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                using (var dialogSettings = new TSettings())
                {
                    dialogSettings.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public override void Dispose()
        {
            if (!formScreenSaver.IsDisposed)
                formScreenSaver.Dispose();

            base.Dispose();
        }
    }
}