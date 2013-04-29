using System.Drawing;

namespace LogiFrame
{
    /// <summary>
    /// Event agruments for ButtonReleasedHandler
    /// </summary>
    public class RenderFrameEventArgs
    {
        /// <summary>
        /// Graphics to draw with
        /// </summary>
        public Graphics graphics;

        /// <summary>
        /// Instance of frame this is called from
        /// </summary>
        public Frame lcd;

        /// <summary>
        /// Prevent frame from being pushed to the display
        /// </summary>
        public bool skipFrame = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphics"></param>
        public RenderFrameEventArgs(Frame lcd, Graphics graphics)
        {
            this.graphics = graphics;
            this.lcd = lcd;
        }
    }
}
