using System;
using System.Drawing;

namespace ScreenSaving.ScreenSavers
{
    /// <summary>
    /// Represents a typical screen saver.
    /// </summary>
    public abstract class BaseScreenSaver
    {
        /// <summary>
        /// Gets the original command line arguments passed into this instance's constructor.
        /// </summary>
        public string[] CommandLineArguments { get; private set; }

        /// <summary>
        /// Gets the screen saver mode determined by the command line arguments.
        /// </summary>
        public ScreenSaverMode ScreenSaverMode { get; private set; }

        /// <summary>
        /// Gets the preview window handle. Value will be 0 if no handle was provided to this instance."/>.
        /// </summary>
        public IntPtr PreviewHandle { get; private set; }

        /// <summary>
        /// Processes screen saver arguments and initializes the screen saver.
        /// </summary>
        /// <param name="arguments">The command line arguments that have been passed into Main().</param>
        /// <exception cref="ArgumentException"></exception>
        public void Initialize(string[] arguments)
        {
            // No arguments specified, treat like /s.
            if (arguments.Length == 0)
            {
                ScreenSaverMode = ScreenSaverMode.ScreenSaver;
            }

            string firstArgument = arguments[0].ToLower().Trim();
            string secondArgument = null;

            // Handle cases where arguments are separated by colon.
            // Example: /c:1234567 or /P:1234567 or /P|1234567 (Windows 10).
            if (firstArgument.Length > 2)
            {
                secondArgument = firstArgument.Substring(3).Trim();
                firstArgument = firstArgument.Substring(0, 2);
            }
            else if (arguments.Length > 1)
            {
                secondArgument = arguments[1];
            }

            switch (firstArgument)
            {
                case "/c":
                    ScreenSaverMode = ScreenSaverMode.Configure;
                    OnShowConfiguration();
                    break;
                case "/s":
                    ScreenSaverMode = ScreenSaverMode.ScreenSaver;
                    OnShowScreenSaver(Display.GetBounds());
                    break;

                case "/p":
                    if (secondArgument == null)
                        throw new ArgumentException("The preview window handle was expected but not provided.", nameof(arguments));

                    long handleNum;
                    bool success = long.TryParse(secondArgument, out handleNum);

                    if (!success)
                        throw new ArgumentException("The provided preview handle was not valid.", nameof(arguments));

                    PreviewHandle = new IntPtr(handleNum);
                    ScreenSaverMode = ScreenSaverMode.Preview;
                    // Allows the screen saver to size properly within the official preview window (only apparent with atypical DPI scaling).
                    NativeMethods.SetProcessDPIAware();
                    OnShowPreview(new PreviewWindow(PreviewHandle));
                    break;

                default:
                    throw new ArgumentException(
                        "The command line argument \"" + firstArgument + "\" is not valid.",
                        nameof(arguments));
            }
        }

        /// <summary>
        /// Invoked when a preview command line switch (\p) is specified.
        /// </summary>
        /// <param name="previewWindow">Facilitates previewing the screen saver.</param>
        protected abstract void OnShowPreview(PreviewWindow previewWindow);

        /// <summary>
        /// Invoked when a screen saver command line switch (\s) is specified.
        /// </summary>
        /// <param name="displayBounds">The bounds of the entire display. Facilitates multi-screen support.</param>
        protected abstract void OnShowScreenSaver(Rectangle displayBounds);

        /// <summary>
        /// Invoked when a configuration command line switch (\c) is specified.
        /// </summary>
        protected abstract void OnShowConfiguration();

        /// <summary>
        /// Converts this instance to a <see cref="string"/>.
        /// </summary>
        /// <param name="originalData">
        /// When true and the original data is present, the command line arguments passed into the instance
        ///  will be returned. Otherwise, the values of the other properties will be return.
        /// </param>
        public string ToString(bool originalData)
        {
            if (CommandLineArguments == null || CommandLineArguments.Length == 0)
                return ToString();

            string result = string.Empty;

            for (int i = 0; i < CommandLineArguments.Length; i++)
            {
                result += CommandLineArguments[i];

                if (i < CommandLineArguments.Length - 1)
                    result += "|";
            }

            return result.TrimEnd('|');
        }
    }
}