﻿using System.Windows;

namespace WpfDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //ScreenSaving.ScreenSavers.WpfScreenSaver<MainWindow, NoSettingsWindow>.Run(e.Args);
        }
    }
}
