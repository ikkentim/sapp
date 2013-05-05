using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace TestApp
{
    class Program2
    {
        static void Main2(string[] args)
        {
            Console.WriteLine("Waiting... FOR YOU! █");
            Console.ReadLine();
            Console.Write("Collecting font data... ");
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);

            Font font = new Font("Arial", 10.0f, FontStyle.Regular);

            CharMap[] cm = new CharMap[(int)Char.MaxValue];

            int mx = 0;
            int my = 0;

            for (int i = 0; i < (int)Char.MaxValue; i++)
            {
                string c = ((char)i).ToString();

                Size size = g.MeasureString(c, font).ToSize();
                CharMap map = new CharMap(c, font, size);

                if (size.Width > mx) mx = size.Width;
                if (size.Height > my) my = size.Height;

                cm[i] = map;
            }

            Console.WriteLine("Done!");
            Console.WriteLine("Max size: " + mx + " " + my);
            Console.WriteLine("Press return to continue...");
            Console.ReadLine();

            Console.WriteLine("Drawing sample:");
            Console.WriteLine();
            CharMap cmap = cm[(int)'s'];

            for (int y = 0; y < cmap.height; y++)
            {
                for (int x = 0; x < cmap.width; x++)
                    Console.Write((cmap.bytemap[x + y * cmap.width] == 255 ? '█' : ' '));

                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }

    class CharMap
    {
        public byte[] bytemap;
        public int width;
        public int height;
        public CharMap(string c, Font font, Size size)
        {
            this.width = size.Width;
            this.height = size.Height;

            Bitmap b = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(b);

            g.Clear(Color.White);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            g.SmoothingMode = SmoothingMode.Default;

            g.DrawString(c, font, Brushes.Black, new Point(0, 0));

            bytemap = ToByteMap(b);
        }

        private byte[] ToByteMap(Bitmap b)
        {
            byte[] result = new byte[b.Width * b.Height];

            for (int x = 0; x < b.Width; x++)
                for (int y = 0; y < b.Height; y++)
                {
                    Color pixel = b.GetPixel(x, y);
                    result[x + y * b.Width] = (byte)(pixel.R == 0x00 && pixel.G == 0x00 && pixel.B == 0x00 ? 0xff : 0x00);
                }

            return result;
        }
    }
}
