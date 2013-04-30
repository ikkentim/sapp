using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogiFrame;
using System.Drawing;
using System.Configuration;

namespace Sapp.Tabs
{
    class Settings : Tab
    {
        LCDApplication app;

        int CurrentSetting = 0;

        public Settings(LCDApplication app)
        {
            this.app = app;
        }

        public bool IsShowAble()
        {
            return true;
        }
        public void Show(Frame lcd)
        { 
            lcd.OnButtonPressed += new Frame.ButtonPressedHandler(lcd_OnButtonPressed);
            lcd.OnRenderFrame += new Frame.RenderFrameHandler(lcd_OnRenderFrame);

            lcd.SetFramesPerSecond(5);
        }

        void lcd_OnRenderFrame(RenderFrameEventArgs e)
        {
            e.graphics.Clear(Color.White);
            switch (CurrentSetting)
            {
                case 0:
                    e.graphics.DrawString("Quick Switch:", Drawing.Fonts.BigBold, Brushes.Black, new Point(1, 1));
                    e.graphics.DrawString(Properties.Settings.Default.QuickSwitch ? "Enabled" : "Disabled", Drawing.Fonts.Big, Brushes.Black, new Point(1, 13));
                    break;
                case 1:
                                      e.graphics.DrawString("Library:", Drawing.Fonts.BigBold, Brushes.Black, new Point(1, 1));
                    e.graphics.DrawString(Properties.Settings.Default.Library, Drawing.Fonts.Big, Brushes.Black, new Point(1, 13));
                    break;
            }
        }

        public void lcd_OnButtonPressed(ButtonPressedEventArgs e)
        {
            if(e.button == Button.Button1)
                switch (CurrentSetting)
                {
                    case 0:
                        Properties.Settings.Default.QuickSwitch = !Properties.Settings.Default.QuickSwitch;
                        break;
                    case 1:
                        Properties.Settings.Default.Library = Properties.Settings.Default.Library == "G15" ? "G510" : "G15";
                        break;
                }
            if (e.button == Button.Button3)
                CurrentSetting = (CurrentSetting + 1) % 2;
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
