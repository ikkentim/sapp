using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogiFrame;
using System.Drawing;

namespace Sapp.Tabs
{
    class Messaging : Tab
    {
        LCDApplication app;

        int currentSet = 0;
        public Messaging(LCDApplication app)
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
        }

        void lcd_OnRenderFrame(RenderFrameEventArgs e)
        {
            if (!IsShowAble())
            {
                app.ShowNextTab();
                return;
            }

            e.graphics.Clear(Color.White);

            if (Sapp.Settings.MessageSets.Count <= currentSet)
                currentSet = 0;

            if (Sapp.Settings.MessageSets.Count > 0)
            {
                string text = Sapp.Settings.MessageSets[currentSet].Name + "\n" +
                    "A:" + Sapp.Settings.MessageSets[currentSet].MessageOneName + "\n" +
                    "B:" + Sapp.Settings.MessageSets[currentSet].MessageTwoName;

                e.graphics.DrawString(text, Drawing.Fonts.Regular, Brushes.Black, new Point(1, 1));
            }
            else
                e.graphics.DrawString("No message sets created!\nCreate sets in the settings.", Drawing.Fonts.Regular, Brushes.Black, new Point(1, 1));

        }

        public void lcd_OnButtonPressed(ButtonPressedEventArgs e)
        {
            switch (e.button)
            {
                case Button.Button1:
                    break;
                case Button.Button2:
                    break;
                case Button.Button3:
                    currentSet = (currentSet + 1) % Sapp.Settings.MessageSets.Count;
                    break;
                case Button.Button4:
                    app.ShowNextTab();
                    break;

            }
                
        }

        public void Hide(Frame lcd)
        {
            lcd.OnButtonPressed -= lcd_OnButtonPressed;
            lcd.OnRenderFrame -= lcd_OnRenderFrame;
        }
    }
}
