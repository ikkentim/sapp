using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using LogiFrame;
using System.Drawing;

namespace Sapp.Tabs
{
    class LoadingGame : Tab
    {
        LCDApplication app;
        public LoadingGame(LCDApplication app)
        {
            this.app = app;
        }

        public bool IsShowAble()
        {
            GTA.Find();
            return GTA.gta != null && GTA.samp == null;
        }
        public void Show(Frame lcd)
        { 
            lcd.OnButtonPressed += new Frame.ButtonPressedHandler(lcd_OnButtonPressed);
            lcd.OnRenderFrame += new Frame.RenderFrameHandler(lcd_OnRenderFrame);

            lcd.SetFramesPerSecond(4);
        }

        void lcd_OnRenderFrame(RenderFrameEventArgs e)
        {
            if (!IsShowAble())
            {
                app.ShowNextTab();
                return;
            }

            e.graphics.Clear(Color.White);
            e.graphics.DrawString("Loading game", Drawing.Fonts.BigBold, Brushes.Black, new Point(1, 1));

        }

        public void lcd_OnButtonPressed(ButtonPressedEventArgs e)
        {
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
