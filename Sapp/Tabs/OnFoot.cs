using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogiFrame;
using System.Drawing;

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
            e.graphics.Clear(Color.White);
            Bitmap weapon = (Bitmap)(Properties.Resources.ResourceManager.GetObject("weapon_0"));
            e.graphics.DrawImage(weapon, new Point(0, 0));

            e.graphics.DrawString("Armour:", Drawing.Fonts.Regular, Brushes.Black, new PointF(e.lcd.Width - 90, -1));
            drawing.drawBar(e.grahpics, new Rectangle(e.lcd.Width - 52, 2, 50, 6), Direction.Right, (float)Math.Ceiling(playerarmor / 100 * 100));

            //health
            e.graphics.DrawString("Health:", Drawing.Fonts.Regular, Brushes.Black, new PointF(e.lcd.Width - 87, 7));
            drawing.drawBar(e.graphics, new Rectangle(e.lcd.Width - 52, 10, 50, 6), Direction.Right, (float)Math.Ceiling(playerhealth / playermaxhealth * 100));


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
