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
        public Simulator(string name)
        {
            n = name;

            System.Threading.Thread t = new System.Threading.Thread(threadVoid);
            t.Start();

        }

        private void threadVoid()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(f = new LogiFrame.Simulator(n)); // or whatever
        }

        public int GetButtons()
        {
            return 4;
        }

        public long ReadSoftButtons()
        {
            return f==null?0:f.ReadSoftButtons();
        }

        public void SetUpdatePriority(UpdatePriority priority)
        {
            if (f != null)
                f.SetUpdatePriority(priority);
        }

        public void UpdateScreen(System.Drawing.Bitmap bitmap)
        {
            if (f != null)
                f.UpdateScreen(bitmap);
        }

        public void Close()
        {
            if (f != null)
                f.Close();
        }
    }
}
