using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
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

        public void UpdateScreen(System.Drawing.Bitmap bitmap)
        {
            if (f != null && !f.Disposing && !f.IsDisposed)
                f.UpdateScreen(bitmap);
        }

        public void Close()
        {

        }
    }
}
