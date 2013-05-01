using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using LogiFrame;
namespace Sapp
{
    static class Program
    {
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            if (Settings.FileExists() == false)
            {
                System.Windows.Forms.Application.Run(new SettingsForm());
            }
            LCDApplication a = new LCDApplication();
        }
    }
}
