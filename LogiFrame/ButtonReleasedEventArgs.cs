namespace LogiFrame
{
    /// <summary>
    /// Event agruments for ButtonReleasedHandler
    /// </summary>
    public class ButtonReleasedEventArgs
    {
        /// <summary>
        /// The released button
        /// </summary>
        public Button button;

        /// <summary>
        /// Instance of frame this is called from
        /// </summary>
        public Frame lcd;

        /// <summary>
        /// Prevents next frame from being rendered and pushed
        /// </summary>
        public bool PreventUpdate = false;

        /// <summary>
        /// EventAgs constructor
        /// </summary>
        /// <param name="button">the released button</param>
        public ButtonReleasedEventArgs(Frame lcd, Button button)
        {
            this.button = button;
            this.lcd = lcd;
        }
    }
}
