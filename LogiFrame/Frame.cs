using System;
using System.Drawing;
using System.Threading;
using System.Drawing.Drawing2D;

namespace LogiFrame
{
    /// <summary>
    /// Main class for the Logitech LCD Screen Application Framework LogiFrame
    /// </summary>
    public class Frame
    {
        public delegate void ButtonPressedHandler(ButtonPressedEventArgs e);
        public event ButtonPressedHandler OnButtonPressed;

        public delegate void ButtonReleasedHandler(ButtonReleasedEventArgs e);
        public event ButtonReleasedHandler OnButtonReleased;

        public delegate void RenderFrameHandler(RenderFrameEventArgs e);
        public event RenderFrameHandler OnRenderFrame;

        public int Width;
        public int Height;

        private Bitmap bitmap;
        private Graphics graphics;
        private Interfaces.DisplayInterface lcd;

        private Thread updateThread = null;
        private int refreshRate;
        private long buttonState = 0;

        // <exception cref="FrameRateOutOfBoundsException">LogiFrame's framerate has to be between 0 and 60.</exception>
        public Frame(Logitech.Keyboard keyboard, string applicationName, int framesPerSecond)
        {

            //Save keyboard specific variables
            switch (keyboard)
            {
                case Logitech.Keyboard.G15:
                    lcd = new Interfaces.G15(applicationName);
                    Width = Logitech.Sizes.G15.Width;
                    Height = Logitech.Sizes.G15.Height;
                    break;
                case Logitech.Keyboard.G510:
                    lcd = new Interfaces.G510(applicationName);
                    Width = Logitech.Sizes.G510.Width;
                    Height = Logitech.Sizes.G510.Height;
                    break;

                case Logitech.Keyboard.Simulator:
                    lcd = new Interfaces.Simulator(applicationName);
                    Width = Logitech.Sizes.Simulator.Width;
                    Height = Logitech.Sizes.Simulator.Height;
                    break;
            }

            //Prepare graphics
            bitmap = new Bitmap(Width, Height);

            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            graphics.SmoothingMode = SmoothingMode.Default;

            //Set FPS
            SetFramesPerSecond(framesPerSecond);
        }

        /// <summary>
        /// Destructor (Calls Close)
        /// </summary>
        ~ Frame() 
        {
            Close();
        }

        public void Update()
        {
            bool renderFrame = true;
            //Read buttons
            long c = lcd.ReadSoftButtons();
            for (int b = 0; b < lcd.GetButtons(); b++)
            {
                long i = (long)Math.Pow(2, b);
                if (OnButtonPressed != null && (c ^ buttonState) == i && (c & i) == i)
                {
                    ButtonPressedEventArgs e = new ButtonPressedEventArgs(this, (Button)i);
                    OnButtonPressed(e);
                    renderFrame = renderFrame || !e.PreventUpdate;

                    e = null;
                }
                if (OnButtonReleased != null && (c ^ buttonState) == i && (buttonState & i) == i)
                {
                    ButtonReleasedEventArgs e = new ButtonReleasedEventArgs(this, (Button)i);
                    OnButtonReleased(e);
                    renderFrame = renderFrame || !e.PreventUpdate;

                    e = null;
                }
            }
            buttonState = c;

            if (renderFrame && OnRenderFrame != null)
            {
                RenderFrameEventArgs e = new RenderFrameEventArgs(this, graphics);
                OnRenderFrame(e);

                if (!e.skipFrame)
                    lcd.UpdateScreen(bitmap);

                e = null;
            }
        }

        public bool IsButtonPressed(Button button)
        {
            return (buttonState & (long)button) == (long)button;
        }

        private void UpdateThread()
        {
            while (true)
            {
                Update();

                Thread.Sleep(refreshRate);
            }
        }

        public void SetUpdatePriority(UpdatePriority updatePriority)
        {
            lcd.SetUpdatePriority(updatePriority);
        }

        public void Close()
        {
            lcd.Close();

            StopThread();
        }

        public void SetFramesPerSecond(int framesPerSecond)
        {
            //Calculate refresh rate
            if (framesPerSecond < 0 || framesPerSecond > 60)
                throw new FrameRateOutOfBoundsException("LogiFrame's framerate has to be between 0 and 60.");

            refreshRate = framesPerSecond == 0 ? 0 : 1000 / framesPerSecond;

            if(updateThread == null)
                StartThread();

        }
        private void StartThread()
        {

            if (refreshRate > 0)
            {
                updateThread = new Thread(UpdateThread);
                updateThread.Start();
            }
        }

        private void StopThread()
        {
            if (updateThread != null)
            {
                try
                {
                    updateThread.Abort();
                }
                catch (Exception e)
                {
                }
            }
        }
    }
}
