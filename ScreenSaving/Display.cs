using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ScreenSaving
{
    /// <summary>
    /// Provides functionality to work with the display (all screens as a whole).
    /// </summary>
    internal static class Display
    {
        /// <summary>
        /// Gets the combined bounds of all screens.
        /// </summary>
        public static Rectangle GetBounds()
        {
            var rect = new Rectangle();
            return Screen.AllScreens.Aggregate(rect, (current, screen) =>
                Rectangle.Union(current, screen.Bounds));
        }
    }
}
