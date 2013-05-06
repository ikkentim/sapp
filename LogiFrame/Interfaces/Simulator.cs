using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace LogiFrame.Interfaces
{
    class Simulator : DisplayInterface
    {
        string n = "";
        LogiFrame.Simulator f;
        Frame lcd;
        public Simulator(Frame lcd, string name)
        {
            n = name;
            this.lcd = lcd;
            System.Threading.Thread t = new System.Threading.Thread(threadVoid);
            t.Start();

            while(f==null)
                System.Threading.Thread.Sleep(5);
        }

        private void threadVoid()
        {
            Application.Run(f = new LogiFrame.Simulator(lcd, n)); // or whatever
        }

        public int GetButtons()
        {
            return 4;
        }

        public long ReadSoftButtons()
        {
            return f == null || f.Disposing || f.IsDisposed ? 0 : f.ReadSoftButtons();
        }

        public void SetUpdatePriority(UpdatePriority priority)
        {
            if (f != null && !f.Disposing && !f.IsDisposed)
                f.SetUpdatePriority(priority);
        }

        public void UpdateScreen(Bitmap bitmap)
        {
            if (f != null && !f.Disposing && !f.IsDisposed)
                f.UpdateScreen(bitmap);
        }

        public void UpdateScreen(byte[] bytemap)
        {
            if (bytemap == null || bytemap.Length != Logitech.Sizes.Simulator.Height * Logitech.Sizes.Simulator.Width)
                return;
            Bitmap bmp = new Bitmap(Logitech.Sizes.Simulator.Width, Logitech.Sizes.Simulator.Height);

            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                    bmp.SetPixel(x, y, bytemap[x + y * bmp.Width] == 255 ? Color.Black : Color.White);

            UpdateScreen(bmp);
        }

        public void Close()
        {

        }
    }
}
