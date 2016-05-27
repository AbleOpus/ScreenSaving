using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenSaving
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Stops then starts the timer.
        /// </summary>
        public static void Restart(this Timer timer)
        {
            timer.Stop();
            timer.Start();
        }

        /// <summary>
        /// The distance, in pixels, from this point to the specified point.
        /// </summary>
        public static int DistanceTo(this Point current, Point point)
        {
            double dx = current.X - point.X, dy = current.Y - point.Y;
            return (int)(Math.Sqrt(dx * dx + dy * dy) + 0.5);
        }
    }
}
