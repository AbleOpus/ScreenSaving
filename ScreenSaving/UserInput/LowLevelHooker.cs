using System;
using System.Diagnostics;

namespace ScreenSaving.UserInput
{
    internal delegate IntPtr LowLevelHookProc(int nCode, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Represents a low-level hooking abstraction.
    /// </summary>
    internal abstract class LowLevelHooker : IDisposable
    {
        private LowLevelHookProc proc;
        private IntPtr hookId = IntPtr.Zero;

        /// <summary>
        /// Gets or sets a value indicating whether to call the next hook in the chain.
        /// </summary>
        public bool CallNextHook { get; set; }

        /// <summary>
        /// Implements logic to process data passed through the hook callback.
        /// </summary>
        protected abstract void ProcHookCallback(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Gets the windows hook type numerical ID.
        /// </summary>
        protected abstract int GetWindowsHookType();

        /// <summary>
        /// Installs the mouse move hook.
        /// </summary>
        /// <returns>True, if function succeeds, otherwise false.</returns>
        public bool Hook()
        {
            proc = HookCallback;

            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                IntPtr moduleHandle = NativeMethods.GetModuleHandle(curModule.ModuleName);
                hookId = NativeMethods.SetWindowsHookEx(GetWindowsHookType(), proc, moduleHandle, 0);
            }

            return hookId != IntPtr.Zero;
        }

        /// <summary>
        /// Uninstalls the mouse move hook.
        /// </summary>
        /// <returns>True, if function succeeds, otherwise false.</returns>
        public bool Unhook()
        {
            return NativeMethods.UnhookWindowsHookEx(hookId);
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
                ProcHookCallback(nCode, wParam, lParam);

            return CallNextHook ? 
                NativeMethods.CallNextHookEx(hookId, nCode, wParam, lParam)
                : IntPtr.Zero;
        }

        public void Dispose()
        {
            Unhook();
        }
    }
}
