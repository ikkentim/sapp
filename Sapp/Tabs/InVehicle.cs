using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogiFrame;
using System.Drawing;
using MemoryMaster.Memory;
using System.Diagnostics;

namespace Sapp.Tabs
{
    class InVehicle : Tab
    {
        bool flicker = false;
        LCDApplication app;

        bool quickSwitch;

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

            quickSwitch = GTA.gta.GetPointer(0xBA18FC).Pointing;
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

            if (Properties.Settings.Default.QuickSwitch && quickSwitch && !vehiclePointer.Pointing)
                app.ShowNextTab((new OnFoot(null)).GetType());
            else if (!quickSwitch && !vehiclePointer.Pointing)
                quickSwitch = true;


            e.graphics.Clear(Color.White);
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

            float speedx = vehiclePointer.GetMemory(68).GetFloat();
            float speedy = vehiclePointer.GetMemory(72).GetFloat();
            float speedz = vehiclePointer.GetMemory(76).GetFloat();
            int speed = (int)Math.Round(Math.Sqrt(((speedx * speedx) + (speedy * speedy)) + (speedz * speedz)) * 136.666667);

            //Position memory
            Pointer vehicleMatrixPointer = vehiclePointer.GetPointer(0x14);

            string locationName = SanAndreas.Data.GetLocationName(vehicleMatrixPointer.GetMemory(0x30).GetFloat(), vehicleMatrixPointer.GetMemory(0x34).GetFloat());
            float z = vehicleMatrixPointer.GetMemory(0x38).GetFloat();


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

            //Drawing 
            e.graphics.DrawString(SanAndreas.Data.getVehicleName(model), Drawing.Fonts.Regular, Brushes.Black, new Point(0, 25));
            e.graphics.DrawString(SanAndreas.Data.getRadioName(radioid), Drawing.Fonts.Regular, Brushes.Black, new Point(0, 0));
            e.graphics.DrawString(locationName, Drawing.Fonts.Regular, Brushes.Black, new Point(0, 8));

            //PopState
            flicker = !flicker;
            if (SanAndreas.Data.vehicleType(type) == SanAndreas.Vehicle.PLANECAR && !SanAndreas.Data.isPlane(model))
            {
                Drawing.Car.DrawCar(e.graphics, 135, 11, 
                    vehiclePointer.GetMemory(0x5A5).GetByte(),
                vehiclePointer.GetMemory(0x5A6).GetByte(),
                vehiclePointer.GetMemory(0x5A7).GetByte(),
                vehiclePointer.GetMemory(0x5A8).GetByte(),               
                flicker?1:0, seat, steats);
            }
            else if (SanAndreas.Data.vehicleType(type) == SanAndreas.Vehicle.BIKE)
            {
                Drawing.Car.DrawBike(e.graphics, 145, 7,
                    vehiclePointer.GetMemory(0x65C).GetByte(),
                    vehiclePointer.GetMemory(0x65D).GetByte(),
                    flicker?1:0, seat, steats);
            }
            else if (SanAndreas.Data.vehicleType(type) == SanAndreas.Vehicle.PLANECAR && SanAndreas.Data.isPlane(model))
                Drawing.Progressbar.Draw(e.graphics, new Rectangle(145, 8, 8, 32), Direction.Up, (z % 250) / 5, true);

            //Health
            Drawing.Progressbar.Draw(e.graphics, new Rectangle(2, 35, 50, 6), Direction.Right, (health - 250) / 750 * 100);
       
            //Nitro
            if (!(nosStatus > 0 && nos == 0))
            {
                if (nosStatus < 0.0)
                    Drawing.Progressbar.Draw(e.graphics, new Rectangle(118, 8, 8, 32), Direction.Up, 100 - (-nosStatus * 100));
                else
                    Drawing.Progressbar.Draw(e.graphics, new Rectangle(118, 8, 8, 32), Direction.Down, (nosStatus * 100));

                //Draw all nitro boxes
                for (int i = 0; i < nos - ((nosStatus == 1 || nosStatus < 0) ? 1 : 0); i++)
                    e.graphics.FillRectangle(Brushes.Black, new Rectangle(106 + ((i % 3) * 4), 31 + (((i - (i % 3)) / 3) * 4), 2, 2));
            }

            //Speed
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Near;
            SizeF measure = e.graphics.MeasureString(speed.ToString("00"), Drawing.Fonts.BigBold);
            e.graphics.DrawString(speed.ToString("00"), Drawing.Fonts.BigBold, Brushes.Black, new Point(108 - (int)measure.Width, e.lcd.Height - (int)measure.Height + 1));

        }
    }
}












