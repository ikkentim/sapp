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

            Pointer playerPointer = GTA.gta.GetPointer(0xB6F5F0);

            byte type = vehiclePointer.GetMemory(0x590).GetByte();
            short model = vehiclePointer.GetMemory(0x22).GetShort();

            Pointer vehicleMatrixPointer = vehiclePointer.GetPointer(0x14);


            int seat = -1;
            bool[] steats = new bool[SanAndreas.Data.getSeats(model)];

            for (int i = 0; i < SanAndreas.Data.getSeats(model); i++)
            {
                Pointer passengerPointer = vehiclePointer.GetPointer(0x460 + i * 0x4);
                //int dat = MemoryReader.getVal(gta, VehiclePointer + (0x460 + (i * 0x4)));
                steats[i] = passengerPointer.Pointing;
                if (playerPointer == passengerPointer)
                    seat = i;
            }

            /*
            int radioid = MemoryReader.getValB(gta, 0x8CB7A5);
            float carx = MemoryReader.getValF(gta, VehicleMatrixPointer + 0x30);
            float cary = MemoryReader.getValF(gta, VehicleMatrixPointer + 0x34);
            float carz = MemoryReader.getValF(gta, VehicleMatrixPointer + 0x38);
            float carh = MemoryReader.getValF(gta, VehiclePointer + 0x4C0);

            int nos = MemoryReader.getValB(gta, VehiclePointer + 0x48A);
            float nosF = MemoryReader.getValF(gta, VehiclePointer + 0x8A4);

            float speed_x = MemoryReader.getValF(gta, VehiclePointer + 0x44);
            float speed_y = MemoryReader.getValF(gta, VehiclePointer + 0x48);
            float speed_z = MemoryReader.getValF(gta, VehiclePointer + 0x4C);
            int speed = (int)Math.Round(Math.Sqrt(((speed_x * speed_x) + (speed_y * speed_y)) + (speed_z * speed_z)) * 136.666667);

            string loc = gtaData.getLocationName((double)carx, (double)cary);
            */



            e.graphics.DrawString("veh.seat:" + seat, Drawing.Fonts.Regular, Brushes.Black, new Point(1, 1));
        }
    }
}