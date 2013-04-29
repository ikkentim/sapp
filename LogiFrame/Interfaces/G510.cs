
namespace LogiFrame.Interfaces
{
    internal class G510 : DisplayInterface
    {
        private LgLcd.NET.LgLcd.lgLcdConnectContext conn = new LgLcd.NET.LgLcd.lgLcdConnectContext();
        private LgLcd.NET.LgLcd.lgLcdOpenContext opn = new LgLcd.NET.LgLcd.lgLcdOpenContext();
        private LgLcd.NET.LgLcd.lgLcdBitmap160x43x1 bmp = new LgLcd.NET.LgLcd.lgLcdBitmap160x43x1();

        private uint LCDUpdatePriority = LgLcd.NET.LgLcd.LGLCD_PRIORITY_NORMAL;

        public G510(string name)
        {
            LgLcd.NET.LgLcd.lgLcdInit();
            conn.appFriendlyName = name;
            conn.isAutostartable = true;
            conn.isPersistent = false;
            conn.connection = LgLcd.NET.LgLcd.LGLCD_INVALID_CONNECTION;
            LgLcd.NET.LgLcd.lgLcdConnect(ref conn);
            opn.connection = conn.connection;
            opn.index = 1;
            LgLcd.NET.LgLcd.lgLcdOpen(ref opn);

            bmp.hdr = new LgLcd.NET.LgLcd.lgLcdBitmapHeader();
            bmp.hdr.Format = LgLcd.NET.LgLcd.LGLCD_BMP_FORMAT_160x43x1;
        }

        public void SetUpdatePriority(UpdatePriority priority)
        {
            switch (priority)
            {
                case UpdatePriority.Alert:
                    LCDUpdatePriority = LgLcd.NET.LgLcd.LGLCD_PRIORITY_ALERT;
                    break;
                case UpdatePriority.Background:
                    LCDUpdatePriority = LgLcd.NET.LgLcd.LGLCD_PRIORITY_BACKGROUND;
                    break;
                case UpdatePriority.IdleNoShow:
                    LCDUpdatePriority = LgLcd.NET.LgLcd.LGLCD_PRIORITY_IDLE_NO_SHOW;
                    break;
                case UpdatePriority.Normal:
                    LCDUpdatePriority = LgLcd.NET.LgLcd.LGLCD_PRIORITY_NORMAL;
                    break;

            }
        }
        public void UpdateScreen(System.Drawing.Bitmap bitmap)
        {
            bmp.pixels = Utilities.Image.ToByteMap(bitmap);

            LgLcd.NET.LgLcd.lgLcdUpdateBitmap(opn.device, ref bmp, LCDUpdatePriority);

        }

        public long ReadSoftButtons()
        {
            int intButtons = 0;
            LgLcd.NET.LgLcd.lgLcdReadSoftButtons(opn.device, out intButtons);
            return (long)intButtons;
        }

        public int GetButtons()
        {
            return 4;
        }

        public void Close()
        {
            LgLcd.NET.LgLcd.lgLcdClose(opn.device);
            LgLcd.NET.LgLcd.lgLcdDisconnect(conn.connection);
            LgLcd.NET.LgLcd.lgLcdDeInit();
        }
    }
}
