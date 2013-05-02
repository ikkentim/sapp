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
        static extern int GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(int hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(int hWnd, uint Msg, int wParam, int lParam);


        public static void Send(string message)
        {
            int handle = 0;

            StringBuilder Buff = new StringBuilder(256);
            handle = GetForegroundWindow();

            if (GetWindowText(0, Buff, 256) > 0)
            {
                //GTA:SA:MP
                if (Buff.ToString().Contains("GTA"))
                {
                    int WindowToFind = FindWindow(null, "GTA:SA:MP");//Possibly same id as handle

                    PostMessage(WindowToFind, 0x0102, 't', 0);
                    Thread.Sleep(2);

                    foreach (char c in message)
                    {
                        PostMessage(WindowToFind, 0x0102, c, 0);
                        Thread.Sleep(2);//To be twitched :D
                    }

                    //Wait for text to process
                    Thread.Sleep(message.Length + 10);

                    //Send return
                    SendMessage(WindowToFind, 0x0100, 13, 1);
                    SendMessage(WindowToFind, 0x0101, 13, 1);
                }
            }
        }
    }
}
