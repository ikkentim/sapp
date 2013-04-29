using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogiFrame;
using System.Drawing;
using MemoryMaster.Memory;

namespace Sapp.Tabs
{
    class OnFoot : Tab
    {
        LCDApplication app;
        public OnFoot(LCDApplication app)
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

            lcd.SetFramesPerSecond(5);
        }

        void lcd_OnRenderFrame(RenderFrameEventArgs e)
        {
            Pointer player = GTA.gta.GetPointer(0xB6F5F0);
            Pointer location = player.GetPointer(0x14);

            float health = player.GetMemory(0x540).GetFloat();
            float maxhealth = player.GetMemory(0x544).GetFloat();
            float armor = player.GetMemory(0x548).GetFloat();

            string locationName = SanAndreas.Data.GetLocationName(location.GetMemory(0x30).GetFloat(), location.GetMemory(0x34).GetFloat());

            int money = GTA.gta.GetMemory(0xB7CE50).GetValue();

            string time = GTA.gta.GetMemory(0xB70153).GetValue().ToString("00") + ":" + GTA.gta.GetMemory(0xB70152).GetValue().ToString("00");

            int weaponSlot = player.GetMemory(0x718).GetValue();
            
            int weaponid = player.GetMemory(0x5A0 + 0x1C * weaponSlot + 0).GetValue();//MemoryReader.getVal(gta, PlayerPointer + 0x5A0 + 28 * weaponslot + 0);
            int clip = player.GetMemory(0x5A0 + 0x1C * weaponSlot + 0x8).GetValue();
            int remaining = player.GetMemory(0x5A0 + 0x1C * weaponSlot + 0xC).GetValue();

            //Drawing
            e.graphics.Clear(Color.White);
            Bitmap weapon = (Bitmap)(Properties.Resources.ResourceManager.GetObject("weapon_0"));
            e.graphics.DrawImage(weapon, new Point(0, 0));

            e.graphics.DrawString("Armor:", Drawing.Fonts.Regular, Brushes.Black, new PointF(e.lcd.Width - 90, -1));
            Drawing.Progressbar.Draw(e.graphics, new Rectangle(e.lcd.Width - 52, 2, 50, 6), Direction.Right, (float)Math.Ceiling(armor / 100 * 100));

            e.graphics.DrawString("Health:", Drawing.Fonts.Regular, Brushes.Black, new PointF(e.lcd.Width - 87, 7));
            Drawing.Progressbar.Draw(e.graphics, new Rectangle(e.lcd.Width - 52, 10, 50, 6), Direction.Right, (float)Math.Ceiling(health / maxhealth * 100));


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
