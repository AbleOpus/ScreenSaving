using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ScreenSaving.UserInput;

namespace ScreenSaving
{
    internal static class NativeMethods
    {
        internal const int WINDOW_LONG_OFFSET = 0x40000000;
        internal const int WINDOW_LONG_INDEX = -16;

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool SetProcessDPIAware();

        [DllImport("User32.dll")]
        internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("User32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("User32.dll", SetLastError = true)]
        internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("User32.dll")]
        internal static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        /// <summary>
        /// Installs an application-defined hook procedure into a hook chain. 
        /// You would install a hook procedure to monitor the system for certain 
        /// types of events. These events are associated either with a specific 
        /// thread or with all threads in the same desktop as the calling thread.
        /// </summary>
        /// <param name="idHook">
        /// The type of hook procedure to be installed. This 
        /// parameter can be one of the following values.
        /// </param>
        /// <param name="lpfn">A pointer to the hook procedure.</param>
        /// <param name="hMod">A handle to the DLL containing the hook procedure pointed to by the lpfn parameter.</param>
        /// <param name="dwThreadId">The identifier of the thread with which the hook procedure is to be associated.</param>
        /// <returns>If function succeeds, returns the handle to the hook procedure, otherwise <c>IntPtr.Zero</c>.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelHookProc lpfn, IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// Removes a hook procedure installed in a hook chain by the SetWindowsHookEx function.
        /// </summary>
        /// <param name="hhk">A handle to the hook to be removed.</param>
        /// <returns>True if the function succeeds, otherwise false.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// Passes the hook information to the next hook procedure in the current hook chain. 
        /// A hook procedure can call this function either before or after processing the hook information.
        /// </summary>
        /// <param name="hhk">This parameter is ignored.</param>
        /// <param name="nCode">The hook code passed to the current hook procedure.</param>
        /// <param name="wParam">The wParam value passed to the current hook procedure. 
        /// The meaning of this parameter depends on the type of hook associated with the 
        /// current hook chain.</param>
        /// <param name="lParam">The lParam value passed to the current hook procedure.
        /// The meaning of this parameter depends on the type of hook associated with the 
        /// current hook chain.</param>
        /// <returns>This value is returned by the next hook procedure in the chain.
        /// The current hook procedure must also return this value. The meaning of the
        ///  return value depends on the hook type.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Retrieves a module handle for the specified module.
        /// </summary>
        /// <param name="lpModuleName">The name of the loaded module (either a .dll or .exe file).</param>
        /// <returns>If the function succeeds, the return value is a handle to the specified module, 
        /// otherwise <c>Itptr.Zero</c>.</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
