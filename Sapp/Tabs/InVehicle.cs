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
            e.graphics.Clear(Color.White);

            Pointer vehiclePointer = GTA.gta.GetPointer(0xBA18FC);

            if (!vehiclePointer.Pointing)
            {
                e.graphics.DrawString("You are currently not in any vehicle", Drawing.Fonts.Regular, Brushes.Black, new Point(1, 1));
                return;
            }

            //General memory
            int radioid = GTA.gta.GetMemory(0x8CB7A5).GetShort();

            //Player Memory
            Pointer playerPointer = GTA.gta.GetPointer(0xB6F5F0);

            //Vehicle memory
            byte type = vehiclePointer.GetMemory(0x590).GetByte();
            short model = vehiclePointer.GetMemory(0x22).GetShort();

            float health = vehiclePointer.GetMemory(0x4C0).GetFloat();

            int nos = vehiclePointer.GetMemory(0x48A).GetByte();
            float nosStatus = vehiclePointer.GetMemory(0x8A4).GetFloat();


            //Position memory
            Pointer vehicleMatrixPointer = vehiclePointer.GetPointer(0x14);

            string locationName = SanAndreas.Data.GetLocationName(vehicleMatrixPointer.GetMemory(0x30).GetFloat(), vehicleMatrixPointer.GetMemory(0x34).GetFloat());
            float z = vehicleMatrixPointer.GetMemory(0x38).GetFloat();

            float speedx = vehicleMatrixPointer.GetMemory(0x44).GetFloat();
            float speedy = vehicleMatrixPointer.GetMemory(0x48).GetFloat();
            float speedz = vehicleMatrixPointer.GetMemory(0x4C).GetFloat();

            int speed = (int)Math.Round(Math.Sqrt(((speedx * speedx) + (speedy * speedy)) + (speedz * speedz)) * 136.666667)

            //Seats
            int seat = -1;
            bool[] steats = new bool[SanAndreas.Data.getSeats(model)];

            for (int i = 0; i < SanAndreas.Data.getSeats(model); i++)
            {
                Pointer passengerPointer = vehiclePointer.GetPointer(0x460 + i * 0x4);
                steats[i] = passengerPointer.Pointing;
                if (playerPointer == passengerPointer)
                    seat = i;
            }

            e.graphics.DrawString("veh.seat:" + seat, Drawing.Fonts.Regular, Brushes.Black, new Point(1, 1));
        }
    }
}