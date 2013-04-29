using System;

namespace LogiFrame
{
    /// <summary>
    /// FrameRateOutOfBoundsException
    /// </summary>
    public class FrameRateOutOfBoundsException : Exception
    {
        public FrameRateOutOfBoundsException(string message) : base(message)
        {
        }
    }
}
