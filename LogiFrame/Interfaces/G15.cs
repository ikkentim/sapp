using lgLcdClassLibrary;

namespace LogiFrame.Interfaces
{
    internal class G15 : DisplayInterface
    {
        private LCDInterface lcd;

        private long updatePriority = LCDInterface.lglcd_PRIORITY_NORMAL;

        public G15(string name)
        {
            lcd = new LCDInterface();
            lcd.Open(name, true);
        }

        public void SetUpdatePriority(UpdatePriority priority)
        {
            switch (priority)
            {
                case UpdatePriority.Alert:
                    updatePriority = LCDInterface.lglcd_PRIORITY_ALERT;
                    break;
                case UpdatePriority.Background:
                    updatePriority = LCDInterface.lglcd_PRIORITY_BACKGROUND;
                    break;
                case UpdatePriority.IdleNoShow:
                    updatePriority = LCDInterface.lglcd_PRIORITY_IDLE_NO_SHOW;
                    break;
                case UpdatePriority.Normal:
                    updatePriority = LCDInterface.lglcd_PRIORITY_NORMAL;
                    break;

            }
        }

        public void UpdateScreen(System.Drawing.Bitmap bitmap)
        {
            
            lcd.DisplayBitmap(ref Utilities.Image.ToByteMap(bitmap)[0], updatePriority);
        }

        public long ReadSoftButtons()
        {
            long buttons = 0;
            lcd.ReadSoftButtons(ref buttons);
            return buttons;
        }

        public int GetButtons()
        {
            return 4;
        }

        public void Close()
        {
            lcd.Close();
        }
    }
}
