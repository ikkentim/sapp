using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogiFrame;
using System.Drawing;
using MemoryMaster.Memory;

namespace Sapp.Tabs
{
    class Chat : Tab
    {
        /*
         * Chat pointer: samp.dll+212A24
         * chatpointer+0x24E + 0xFC * chatline(=0-99) [length: 0xFC]
         * username: samp.dll+2123AF
         * 
         * My Score when i Joined: samp.dll+EA074
         * 
         * 
         * >>Guessing scoreboard below:
         * 
         * Pointer to score pool: samp.dll+1216DC
         */

        LCDApplication app;

        int currentPage = 24;
        int currentScroll = 0;
        public Chat(LCDApplication app)
        {
            this.app = app;
        }

        public bool IsShowAble()
        {
            GTA.Find();
            return GTA.samp != null;
        }

        public void Show(Frame lcd)
        {
            lcd.OnButtonPressed += new Frame.ButtonPressedHandler(lcd_OnButtonPressed);
            lcd.OnRenderFrame += new Frame.RenderFrameHandler(lcd_OnRenderFrame);

            currentScroll = 0;
            lcd.SetFramesPerSecond(7);
        }


        public void Hide(Frame lcd)
        {
            lcd.OnRenderFrame -= lcd_OnRenderFrame;
            lcd.OnButtonPressed -= lcd_OnButtonPressed;

        }

        void lcd_OnRenderFrame(RenderFrameEventArgs e)
        {
            if (!IsShowAble())
            {
                app.ShowNextTab();
                return;
            }

            e.graphics.Clear(Color.White);

            Pointer chatPointer = GTA.samp.GetPointer(0x212A24);


            string[] lines = 
            {
                Sapp.Chat.RemoveColorTags(chatPointer.GetMemory(0x24E + (0xFC * (currentPage * 4 + -1))).GetString(128)),
                Sapp.Chat.RemoveColorTags(chatPointer.GetMemory(0x24E + (0xFC * (currentPage * 4 + 0))).GetString(128)),
                Sapp.Chat.RemoveColorTags(chatPointer.GetMemory(0x24E + (0xFC * (currentPage * 4 + 1))).GetString(128)),
                Sapp.Chat.RemoveColorTags(chatPointer.GetMemory(0x24E + (0xFC * (currentPage * 4 + 2))).GetString(128))
            };


            int width = 0;
            foreach (string l in lines)
            {
                int tmp = Convert.ToInt32(e.graphics.MeasureString(l, Drawing.Fonts.Regular).Width);
                if (tmp > width) width = tmp;
            }

            currentScroll = width < e.lcd.Width ? 0 : (currentScroll + Sapp.Settings.ChatScrollSpeed) % (e.lcd.Width - (int)(width * 1.1));

            e.graphics.DrawString(String.Join("\n", lines), Drawing.Fonts.Regular, Brushes.Black, new Point(-currentScroll, 0));

            e.graphics.FillRectangle(Brushes.Black, new Rectangle(e.lcd.Width - 25, 0, 25, 11));
            e.graphics.DrawString((currentPage+1).ToString("00") + "/25", Drawing.Fonts.Regular, Brushes.White, new Point(e.lcd.Width - 25, 1));

        }

        void lcd_OnButtonPressed(ButtonPressedEventArgs e)
        {
            switch (e.button)
            {
                case Button.Button1:
                    currentScroll = 0;
                    currentPage--;
                    if (currentPage < 0)
                        currentPage = 24;
                    break;
                case Button.Button2:
                    currentScroll = 0;
                    currentPage = (currentPage + 1) % 25;
                    break;
                case Button.Button4:
                    app.ShowNextTab();
                    break;

            }
        }
    }
}
