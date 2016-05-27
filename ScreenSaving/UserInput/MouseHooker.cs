using System;

namespace ScreenSaving.UserInput
{
    /// <summary>
    /// Represents a system-wide mouse hooker.
    /// </summary>
    internal sealed class MouseHooker : LowLevelHooker
    {
        /// <summary>
        /// Occurs when any button mouse activity is detected.
        /// </summary>
        public event EventHandler MouseButton = delegate { };

        /// <summary>
        /// Occurs when the mouse has moved.
        /// </summary>
        public event EventHandler MouseMove = delegate { };

        protected override void ProcHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // Process message and raise the appropriate event
            switch ((MouseMessages)wParam)
            {
                case MouseMessages.MouseMove:
                    MouseMove(this, EventArgs.Empty);
                    break;

                case MouseMessages.RightButtonDown:
                case MouseMessages.LeftButtonDown:
                case MouseMessages.RightButtonUp:
                case MouseMessages.LeftButtonUp:
                case MouseMessages.MouseWheel:
                case MouseMessages.MiddleButtonDown:
                    MouseButton(this, EventArgs.Empty);
                    break;
            }
        }

        /// <summary>
        /// Gets a low-level system-wide mouse hook identifier.
        /// </summary>
        protected override int GetWindowsHookType()
        {
            return 14;
        }
    }
}
