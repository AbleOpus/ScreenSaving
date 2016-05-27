namespace ScreenSaving
{
    /// <summary>
    /// Specifies the various modes that a screen saver can run in.
    /// </summary>
    public enum ScreenSaverMode
    {
        /// <summary>
        /// Show the screen saver in full-screen.
        /// </summary>
        ScreenSaver,
        /// <summary>
        /// Show the screen saver in the preview window.
        /// </summary>
        Preview,
        /// <summary>
        /// Show screen saver configurations.
        /// </summary>
        Configure
    }

    /// <summary>
    /// Specifies messages, which indicate what mouse operation was performed.
    /// </summary>
    internal enum MouseMessages
    {
        /// <summary>
        /// The mouse pointer has moved.
        /// </summary>
        MouseMove = 0x0200,
        /// <summary>
        /// The left mouse button was depressed.
        /// </summary>
        LeftButtonDown = 0x0201,
        /// <summary>
        /// The left mouse button was unpressed.
        /// </summary>
        LeftButtonUp = 0x0202,
        /// <summary>
        /// The right mouse button was depressed.
        /// </summary>
        RightButtonDown = 0x0204,
        /// <summary>
        /// The right mouse button was unpressed.
        /// </summary>
        RightButtonUp = 0x0205,
        /// <summary>
        /// The mouse wheel has scrolled.
        /// </summary>
        MouseWheel = 0x020A,
        /// <summary>
        /// The middle mouse button has been depressed.
        /// </summary>
        MiddleButtonDown = 0x0207
    }
}
