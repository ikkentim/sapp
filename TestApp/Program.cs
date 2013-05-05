using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;

namespace TestApp
{
    class Program
    {

        static void Mainz(string[] args)
        {
            int itterations = 100;
            byte[] sample = new byte[1];

            #region GDI+
            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < itterations; i++)
            {
                sample = drawFromGDI('m');
            }
            watch.Stop();
            Console.Write("Using GDI+:");
            Console.WriteLine(" Time Elapsed {0} ms", watch.Elapsed.TotalMilliseconds);

            drawMap(sample, 15, 16);
            #endregion

            #region GDI+ with open graphics object
            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var watchg = Stopwatch.StartNew();

            Bitmap bmp = new Bitmap(160, 43);
            Graphics g = Graphics.FromImage(bmp);

            for (int i = 0; i < itterations; i++)
            {
                sample = drawToGDI(g, 'm');
            }
            g.Dispose();
            bmp.Dispose();

            watchg.Stop();
            Console.Write("Using GDI+ with open graphics object:");
            Console.WriteLine(" Time Elapsed {0} ms", watchg.Elapsed.TotalMilliseconds);

            drawMap(sample, 15, 16);
            #endregion

            #region File
            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var watchw = Stopwatch.StartNew();
            for (int i = 0; i < itterations; i++)
            {
                sample = drawFromFile('m');
            }
            watchw.Stop();
            Console.Write("Using File:");
            Console.WriteLine(" Time Elapsed {0} ms", watchw.Elapsed.TotalMilliseconds);

            drawMap(sample, 15, 16);
            #endregion

            #region Open filestream
            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var watch2w = Stopwatch.StartNew();

            FileStream fs = File.OpenRead("arial10.dat");

            for (int i = 0; i < itterations; i++)
            {
                sample = drawFromFileStream(fs, 'm');
            }

            fs.Close();
            watch2w.Stop();
            Console.Write("Using Open filestream:");
            Console.WriteLine(" Time Elapsed {0} ms", watch2w.Elapsed.TotalMilliseconds);

            drawMap(sample, 15, 16);
            #endregion

            #region OPen filestream compressed
            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var watch22w = Stopwatch.StartNew();

            FileStream fs2 = File.OpenRead("arial10c.dat");

            for (int i = 0; i < itterations; i++)
            {
                sample = drawFromFileStreamCompressed(fs2, 'm');
            }

            fs.Close();
            watch22w.Stop();
            Console.Write("Using Open filestream compressed:");
            Console.WriteLine(" Time Elapsed {0} ms", watch22w.Elapsed.TotalMilliseconds);

            drawMap(sample, 15, 16);
            #endregion

            Console.ReadLine();

        }


        static void drawMap(byte[] b, int w, int h)
        {
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    Console.Write(b[x + y * w] == 255 ? "x" : " ");

                }
                Console.WriteLine();
            }
        }

        static byte[] drawFromGDI(char i)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);

            Font font = new Font("Arial", 10.0f, FontStyle.Regular);

            string c = i.ToString();

            Size size = g.MeasureString(c, font).ToSize();
            CharMap map = new CharMap(c, font, size);

            //Got it, in map...
            
            g.Dispose();
            bmp.Dispose();

            return map.bytemap;
        }
        static byte[] drawToGDI(Graphics g, char i)
        {
 
            Font font = new Font("Arial", 10.0f, FontStyle.Regular);

            string c = i.ToString();

            Size size = g.MeasureString(c, font).ToSize();
            CharMap map = new CharMap(c, font, size);

            //Got it, in map...
            return map.bytemap;
        }

        static byte[] drawFromFileStream(FileStream fs, char ch)
        {

            byte[] pointer = new byte[4];
            int pointerLocation = 4 * (int)ch;

            fs.Position = pointerLocation;
            fs.Read(pointer, 0, 4);

            int loc = BitConverter.ToInt32(pointer, 0);

            fs.Position = loc;
            byte[] size = new byte[2];
            fs.Read(size, 0, size.Length);

            byte[] c = new byte[size[0] * size[1]];

            fs.Read(c, 0, c.Length);

            //Got it, in c...
            return c;
        }
        static byte[] drawFromFileStreamCompressed(FileStream fs, char ch)
        {
            CharByteMap cbm = new CharByteMap(fs, ch);
            byte[] target = new byte[cbm.width * cbm.height];

            cbm.PrintToByteMap(target, cbm.width, 0, 0);

            return target;
        }
        static byte[] drawFromFile(char ch)
        {
            FileStream fs = File.OpenRead("arial10.dat");

            byte[] pointer = new byte[4];
            int pointerLocation = 4 * (int)ch;

            fs.Position = pointerLocation;
            fs.Read(pointer, 0, 4);

            int loc = BitConverter.ToInt32(pointer, 0);

            fs.Position = loc;
            byte[] size = new byte[2];
            fs.Read(size, 0, size.Length);

            byte[] c = new byte[size[0] * size[1]];

            fs.Read(c, 0,c.Length);

            fs.Close();

            return c;
        }

        static void Main(string[] args)
        {
            Console.Write("Collecting font data...");

            Graphics g = Graphics.FromImage(new Bitmap(1, 1));
            Font f = new Font("Arial", 10.0f, FontStyle.Regular);

            List<byte> dat = new List<byte>();
            for (int i = 0; i < (int)Char.MaxValue; i++)
            {
                Size s = g.MeasureString(((char)i).ToString(), f).ToSize();

                byte[] d = new byte[s.Height * s.Width];

                dat.InsertRange(i * 4, BitConverter.GetBytes(4 * (Char.MaxValue - i) + dat.Count));

                Bitmap b = new Bitmap(s.Width, s.Height);
                Graphics h = Graphics.FromImage(b);
                h.Clear(Color.White);
                h.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                h.SmoothingMode = SmoothingMode.Default;

                h.DrawString(((char)i).ToString(), f, Brushes.Black, new Point(0, 0));

                for (int x = 0; x < b.Width; x++)
                    for (int y = 0; y < b.Height; y++)
                    {
                        Color pixel = b.GetPixel(x, y);
                        d[x + y * b.Width] = (byte)(pixel.R == 0x00 && pixel.G == 0x00 && pixel.B == 0x00 ? 0xff : 0x00);
                    }

                CharByteMap cbm = new CharByteMap((byte)s.Width, (byte)s.Height, d);
                dat.Add((byte)s.Width);
                dat.Add((byte)s.Height);
                dat.AddRange(cbm.Compress());
            }

            File.WriteAllBytes("arial10c.dat", dat.ToArray());
        }
        static void Mainx(string[] args)
        {
            Console.Write("Collecting font data...");

            Graphics g = Graphics.FromImage(new Bitmap(1, 1));
            Font f = new Font("Arial", 10.0f, FontStyle.Regular);

            List<byte> dat = new List<byte>();
            for (int i = 0; i < (int)Char.MaxValue; i++)
            {
                Size s = g.MeasureString(((char)i).ToString(), f).ToSize();

                byte[] d = new byte[s.Height * s.Width + 2];
                d[0] = (byte)s.Width;
                d[1] = (byte)s.Height;

                dat.InsertRange(i * 4, BitConverter.GetBytes(4 * (Char.MaxValue - i) + dat.Count));

                Bitmap b = new Bitmap(s.Width, s.Height);
                Graphics h = Graphics.FromImage(b);
                h.Clear(Color.White);
                h.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                h.SmoothingMode = SmoothingMode.Default;

                    h.DrawString(((char)i).ToString(), f, Brushes.Black, new Point(0, 0));

                for (int x = 0; x < b.Width; x++)
                    for (int y = 0; y < b.Height; y++)
                    {
                        Color pixel = b.GetPixel(x, y);
                        d[x + y * b.Width + 2] = (byte)(pixel.R == 0x00 && pixel.G == 0x00 && pixel.B == 0x00 ? 0xff : 0x00);
                    }

                dat.AddRange(d);
            }

            File.WriteAllBytes("arial10.dat", dat.ToArray());

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