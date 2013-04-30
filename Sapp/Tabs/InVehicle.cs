using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogiFrame;
using System.Drawing;
using MemoryMaster.Memory;

namespace Sapp.Tabs
{
    class InVehicle : Tab
    {
        LCDApplication app;
        public InVehicle(LCDApplication app)
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

        public void Hide(Frame lcd)
        {
            lcd.OnButtonPressed -= new Frame.ButtonPressedHandler(lcd_OnButtonPressed);
            lcd.OnRenderFrame -= new Frame.RenderFrameHandler(lcd_OnRenderFrame);
        }

        void lcd_OnButtonPressed(ButtonPressedEventArgs e)
        {
            if (e.button == Button.Button4)
                app.ShowNextTab();
        }


        void lcd_OnRenderFrame(RenderFrameEventArgs e)
        {
            Pointer vehiclePointer = GTA.gta.GetPointer(0xBA18FC);

            e.graphics.Clear(Color.White);
            e.graphics.DrawString("veh", Drawing.Fonts.Regular, Brushes.Black, new Point(1, 1));
        }
    }
}