using System;
using System.Drawing;

namespace ScreenSaving
{
    /// <summary>
    /// Represents the preview window in screen saver settings.
    /// </summary>
    public class PreviewWindow
    {
        /// <summary>
        /// Gets the handle of the preview window.
        /// </summary>
        public IntPtr Handle { get; }

        private IntPtr child;
        /// <summary>
        /// Gets or sets the handle of the preview windows child. When set,
        /// the preview window will update accordingly.
        /// </summary>
        public IntPtr Child
        {
            get { return child; }
            set
            {
                child = value;
                NativeMethods.SetParent(child, Handle);
                //Make child window a close when parent closes
                long windowID = NativeMethods.GetWindowLong(child, NativeMethods.WINDOW_LONG_INDEX) | NativeMethods.WINDOW_LONG_OFFSET;
                NativeMethods.SetWindowLong(child, NativeMethods.WINDOW_LONG_INDEX, new IntPtr(windowID));
                // Resize child to fit parent
                Rectangle rect;
                NativeMethods.GetClientRect(Handle, out rect);
                NativeMethods.SetWindowPos(child, IntPtr.Zero, 0, 0, rect.Width, rect.Height, 0);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewWindow"/> class with the specified handle.
        /// </summary>
        /// <param name="handle">The handle to the preview window.</param>
        public PreviewWindow(IntPtr handle)
        {
            Handle = handle;
        }
    }
}
