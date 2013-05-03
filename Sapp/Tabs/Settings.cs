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
                    e.graphics.DrawString(Sapp.Settings.QuickSwitch ? "Enabled" : "Disabled", Drawing.Fonts.Big, Brushes.Black, new Point(1, 13));
                    break;
                case 1:
                    e.graphics.DrawString("Chat scroll speed:", Drawing.Fonts.BigBold, Brushes.Black, new Point(1, 1));
                    switch (Sapp.Settings.ChatScrollSpeed)
                    {
                        case 0:
                            e.graphics.DrawString("No scroll", Drawing.Fonts.Big, Brushes.Black, new Point(1, 13));
                            break;
                        case 1:
                            e.graphics.DrawString("Slow", Drawing.Fonts.Big, Brushes.Black, new Point(1, 13));
                            break;
                        case 2:
                            e.graphics.DrawString("Normal", Drawing.Fonts.Big, Brushes.Black, new Point(1, 13));
                            break;
                        case 3:
                            e.graphics.DrawString("Fast", Drawing.Fonts.Big, Brushes.Black, new Point(1, 13));
                            break;
                        case 4:
                            e.graphics.DrawString("Very Fast", Drawing.Fonts.Big, Brushes.Black, new Point(1, 13));
                            break;
                        case 5:
                            e.graphics.DrawString("NITRO SPEED", Drawing.Fonts.Big, Brushes.Black, new Point(1, 13));
                            break;

                    }
                    break;
                case 2:
                    e.graphics.DrawString("Library:", Drawing.Fonts.BigBold, Brushes.Black, new Point(1, 1));
                    e.graphics.DrawString(Sapp.Settings.Library, Drawing.Fonts.Big, Brushes.Black, new Point(1, 13));
                    break;
                case 3:
                    e.graphics.DrawString("Open Settings Window", Drawing.Fonts.BigBold, Brushes.Black, new Point(1, 1));
                    break;
            }
        }

        public void lcd_OnButtonPressed(ButtonPressedEventArgs e)
        {
            if(e.button == Button.Button1)
                switch (CurrentSetting)
                {
                    case 0:
                        Sapp.Settings.QuickSwitch = !Sapp.Settings.QuickSwitch;
                        break;
                    case 1:
                        Sapp.Settings.ChatScrollSpeed = (Sapp.Settings.ChatScrollSpeed + 1) % 6;
                        break;
                    case 2:
                        Sapp.Settings.Library = Sapp.Settings.Library == "G15" ? "G510" : "G15";
                        break;
                    case 3:
                        System.Windows.Forms.Application.Run(new SettingsForm());
                        break;
                }
            if (e.button == Button.Button3)
                CurrentSetting = (CurrentSetting + 1) % 4;
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
