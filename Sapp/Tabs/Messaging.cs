using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogiFrame;
using System.Drawing;
using MemoryMaster.Memory;
using System.Text.RegularExpressions;

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
                app.ShowNextTab(new Offline(null).GetType());
                return;
            }

            e.graphics.Clear(Color.White);

            if (Sapp.Settings.MessageSets.Count <= currentSet)
                currentSet = 0;

            if (Sapp.Settings.MessageSets.Count > 0)
            {
                string text = Sapp.Settings.MessageSets[currentSet].Name + "\n";
                if (Sapp.Settings.MessageSets[currentSet].MessageOneName.Length != 0)
                    text += "1:" + Sapp.Settings.MessageSets[currentSet].MessageOneName + "\n";
                if (Sapp.Settings.MessageSets[currentSet].MessageTwoName.Length != 0)
                    text += "2:" + Sapp.Settings.MessageSets[currentSet].MessageTwoName;

                e.graphics.DrawString(text, Drawing.Fonts.Regular, Brushes.Black, new Point(1, 1));
            }
            else
                e.graphics.DrawString("No message sets created!\nCreate sets in the settings.", Drawing.Fonts.Regular, Brushes.Black, new Point(1, 1));

        }

        private void SendMessage(string message)
        {
            Pointer player = GTA.gta.GetPointer(0xB6F5F0);
            Pointer location = player.GetPointer(0x14);


            message = new Regex(@"\[location\]").Replace(message, delegate(Match m)
            {
                return SanAndreas.Data.GetLocationName(location.GetMemory(0x30).GetFloat(), location.GetMemory(0x34).GetFloat());
            });

            message = new Regex(@"\[username\]").Replace(message, delegate(Match m)
            {
                return GTA.samp.GetMemory(0x2123AF).GetString(20);
            });

            message = new Regex(@"\[playername\]").Replace(message, delegate(Match m)
            {
                return GTA.samp.GetMemory(0x2123AF).GetString(20);
            });

            message = new Regex(@"\[direction\]").Replace(message, delegate(Match m)
            {
                float rotation = (player.GetMemory(0x558).GetFloat() + (float)Math.PI) * (360 / (2 * (float)Math.PI));
               
                if (rotation >= 90 - 45 && rotation < 90 + 45)
                    return "East";
                else if (rotation >= 180 - 45 && rotation < 180 + 45)
                    return "North";
                else if (rotation >= 270 - 45 && rotation < 270 + 45)
                    return "West";
                else
                    return "South";
            });

            message = new Regex(@"\[direction2\]").Replace(message, delegate(Match m)
            {
                float rotation = (player.GetMemory(0x558).GetFloat() + (float)Math.PI) * (360 / (2 * (float)Math.PI));

                if (rotation >= 45 - 22.5f && rotation < 45 + 22.5f)
                    return "South-East";
                else if (rotation >= 90 - 22.5f && rotation < 90 + 22.5f)
                    return "East";
                else if (rotation >= 135 - 22.5f && rotation < 135 + 22.5f)
                    return "North-East";
                else if (rotation >= 180 - 22.5f && rotation < 180 + 22.5f)
                    return "North";
                else if (rotation >= 215 - 22.5f && rotation < 215 + 22.5f)
                    return "North-West";
                else if (rotation >= 270 - 22.5f && rotation < 270 + 22.5f)
                    return "West";
                else if (rotation >= 315 - 22.5f && rotation < 315 + 22.5f)
                    return "South-West";
                else
                    return "South";
            });
            
            ClientMessage.Send(message);
        }

        public void lcd_OnButtonPressed(ButtonPressedEventArgs e)
        {
            switch (e.button)
            {
                case Button.Button1:
                    if (Sapp.Settings.MessageSets.Count > 0 && Sapp.Settings.MessageSets[currentSet].MessageOneContent.Length > 0)
                        SendMessage(Sapp.Settings.MessageSets[currentSet].MessageOneContent);
                    break;
                case Button.Button2:
                    if (Sapp.Settings.MessageSets.Count > 0 && Sapp.Settings.MessageSets[currentSet].MessageTwoContent.Length > 0)
                        SendMessage(Sapp.Settings.MessageSets[currentSet].MessageTwoContent);
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
