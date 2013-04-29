using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

using LogiFrame;
using MemoryMaster;
using MemoryMaster.Memory;

namespace TestApp
{
    class Program
    {
        static Frame lcd;
        static void Main(string[] args)
        {
            Process[] processes = Process.GetProcessesByName("gta_sa");
            if (processes.Length > 0)
            {
                Process gta = processes[0];

                ProcessReader reader = new ProcessReader(gta);

                while (!gta.HasExited)
                {
                    Pointer playerPointer = reader.GetPointer(0xB6F5F0);
                    Pointer locationpointer = playerPointer.GetPointer(0x14);
                    Console.WriteLine("X:" + locationpointer.GetMemory(0x30).GetFloat());
                    Console.WriteLine("Y:" + locationpointer.GetMemory(0x34).GetFloat());

                    Console.ReadLine();
                }
            }

            Console.ReadLine();
            lcd = new Frame(LogiFrame.Logitech.Keyboard.G15, "TestApplication", 5);

            lcd.OnButtonPressed += new Frame.ButtonPressedHandler(lcd_OnButtonPressed);
            lcd.OnButtonReleased += new Frame.ButtonReleasedHandler(lcd_OnButtonReleased);
            lcd.OnRenderFrame += new Frame.RenderFrameHandler(lcd_OnRenderFrame);
            Console.WriteLine("Running app...");

            Console.ReadLine();
        }

        static void lcd_OnRenderFrame(RenderFrameEventArgs e)
        {
            //e.graphics.Clear(Color.White);
            //e.graphics.DrawString("TestApp!", new Font("Arial", 7, FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(1, 1));
            e.skipFrame = true;
        }

        static void lcd_OnButtonReleased(ButtonReleasedEventArgs e)
        {
            Console.WriteLine("released: " + e.button.ToString());      
        }

        static void lcd_OnButtonPressed(ButtonPressedEventArgs e)
        {
            Console.WriteLine("pressed: " + e.button.ToString());
        }
    }
}
