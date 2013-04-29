namespace LogiFrame
{
    /// <summary>
    /// Event agruments for ButtonPressedHandler
    /// </summary>
    public class ButtonPressedEventArgs
    {
        /// <summary>
        /// The pressed button
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
        /// <param name="button">the pressed button</param>
        public ButtonPressedEventArgs(Frame lcd, Button button)
        {
            this.button = button;
            this.lcd = lcd;
        }
    }
}
