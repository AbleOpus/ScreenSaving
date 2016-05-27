using System;

namespace ScreenSaving.UserInput
{
    /// <summary>
    /// Represents a system-wide keyboard hooker.
    /// </summary>
    internal sealed class KeyboardHooker : LowLevelHooker
    {
        private const int WM_KEYDOWN = 0x0100;

        /// <summary>
        /// Occurs when any key is depressed.
        /// </summary>
        public event EventHandler KeyDown = delegate { };

        protected override void ProcHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (wParam == (IntPtr) WM_KEYDOWN)
                KeyDown(this, EventArgs.Empty);
        }

        /// <summary>
        /// Gets a low-level system-wide keyboard hook identifier. 
        /// </summary>
        protected override int GetWindowsHookType()
        {
            return 13;
        }
    }
}
