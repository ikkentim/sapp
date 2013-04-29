using System.Drawing;
using System;

namespace LogiFrame.Utilities
{
    internal static class Image
    {
        public static byte[] ToByteMap(Bitmap b)
        {
            byte[] result = new byte[b.Width * b.Height];
       
            for(int x=0;x<b.Width;x++)
                for(int y=0;y<b.Height;y++)
                {
                    Color pixel = b.GetPixel(x,y);
                    result[x + y * b.Width] = (byte)(pixel.R == 0x00 && pixel.G == 0x00 && pixel.B == 0x00 ? 0xff : 0x00);
                }

            return result;
        }
    }
}
