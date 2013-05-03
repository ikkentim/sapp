using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace Sapp
{
    static class ClientMessage
    {
        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(int hWnd, uint Msg, int wParam, int lParam);

        //[DllImport("user32.dll")]
        //public static extern bool EnableWindow(int hWnd, bool bEnable); experiMENTAL!

        public static void Send(string message)
        {
            //GTA:SA:MP

            int WindowToFind = FindWindow(null, "GTA:SA:MP");

            //EnableWindow(WindowToFind, false);
            //Thread.Sleep(2);
            PostMessage(WindowToFind, 0x0102, 't', 0);
            Thread.Sleep(2);

            foreach (char c in message)
            {
                PostMessage(WindowToFind, 0x0102, c, 0);
            }

            //Wait for text to process
            Thread.Sleep(message.Length + 10);

            //Send return
            SendMessage(WindowToFind, 0x0100, 13, 1);
            SendMessage(WindowToFind, 0x0101, 13, 1);
            //Thread.Sleep(2);
            //EnableWindow(WindowToFind, true);
        }
    }
}
