using System;
using System.Windows.Forms;
using ScreenSaving.Forms;
using ScreenSaving.ScreenSavers;
using WinFormsDemo.Forms;

namespace WinFormsDemo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0 && args[0].Equals("/a", StringComparison.OrdinalIgnoreCase))
            {
                Application.Run(new MouseActivityTestForm());
            }
            else
            {
                WinFormsScreenSaver<DebugScreenSaverForm, SettingsDialog>.Run(args);
            }
        }
    }
}