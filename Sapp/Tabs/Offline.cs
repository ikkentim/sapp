using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogiFrame;
using System.Drawing;
using System.Diagnostics;

namespace Sapp.Tabs
{
    class Offline : Tab
    {
        LCDApplication app;
        public Offline(LCDApplication app)
        {
            this.app = app;
        }

        public bool IsShowAble()
        {
            GTA.Find();
            return GTA.gta == null;
        }
        public void Show(Frame lcd)
        { 
            lcd.OnButtonPressed += new Frame.ButtonPressedHandler(lcd_OnButtonPressed);
            lcd.OnRenderFrame += new Frame.RenderFrameHandler(lcd_OnRenderFrame);

            lcd.SetFramesPerSecond(1);
        }

        void lcd_OnRenderFrame(RenderFrameEventArgs e)
        {
            e.graphics.Clear(Color.White);
            e.graphics.DrawString("[LSRES]", Drawing.Fonts.BigBold, Brushes.Black, new Point(1, 1));
            e.graphics.DrawString("Join", Drawing.Fonts.Regular, Brushes.Black, new Point(6, 30));
            e.graphics.DrawString("Website", Drawing.Fonts.Regular, Brushes.Black, new Point(36, 30));

            if (!IsShowAble())
                app.ShowNextTab();
        }

        public void lcd_OnButtonPressed(ButtonPressedEventArgs e)
        {
            if (e.button == Button.Button1)
                System.Diagnostics.Process.Start("samp://samp.lsres.net");
            if (e.button == Button.Button2)
                System.Diagnostics.Process.Start("http://www.lsres.net");

            if (e.button == Button.Button4)
                app.ShowNextTab();
        }
        public void Hide(Frame lcd)
        {
            lcd.OnButtonPressed -= lcd_OnButtonPressed;
            lcd.OnRenderFrame -= lcd_OnRenderFrame;
        }
    }
}
