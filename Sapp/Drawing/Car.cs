using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Sapp.Drawing
{
    static class Car
    {
        public static void DrawCar(Graphics g, int x, int y, int pop_lf, int pop_lr, int pop_rf, int pop_rr, int pop_show, int seat, bool[] seats)
        {
            Pen pen = new Pen(Brushes.Black);
            if (pop_lf == 0 || (pop_lf == pop_show))
                g.DrawRectangle(pen, new Rectangle(x + 0, y + 1, 1, 5));
            if (pop_lr == 0 || (pop_lr == pop_show))
                g.DrawRectangle(pen, new Rectangle(x + 0, y + 22, 1, 5));

            if (pop_rf == 0 || (pop_rf == pop_show))
                g.DrawRectangle(pen, new Rectangle(x + 19, y + 1, 1, 5));
            if (pop_rr == 0 || (pop_rr == pop_show))
                g.DrawRectangle(pen, new Rectangle(x + 19, y + 22, 1, 5));

            if (seats.Length >= 1)
            {
                g.DrawRectangle(pen, new Rectangle(x + 4, y + 3, 5, 7));
                if (seat == 0)
                    g.FillRectangle(Brushes.Black, new Rectangle(x + 4, y + 3, 5, 7));
                else if (seats[0])
                    drawGrid(g, new Rectangle(x + 4, y + 3, 5, 7));
            }

            if (seats.Length >= 2)
            {
                g.DrawRectangle(pen, new Rectangle(x + 11, y + 3, 5, 7));
                if (seat == 1)
                    g.FillRectangle(Brushes.Black, new Rectangle(x + 11, y + 3, 5, 7));
                else if (seats[1])
                    drawGrid(g, new Rectangle(x + 11, y + 3, 5, 7));
            }

            if (seats.Length >= 3)
            {
                g.DrawRectangle(pen, new Rectangle(x + 4, y + 13, 5, 7));
                if (seat == 2)
                    g.FillRectangle(Brushes.Black, new Rectangle(x + 4, y + 13, 5, 7));
                else if (seats[2])
                    drawGrid(g, new Rectangle(x + 4, y + 13, 5, 7));
            }

            if (seats.Length >= 4)
            {
                g.DrawRectangle(pen, new Rectangle(x + 11, y + 13, 5, 7));
                if (seat == 3)
                    g.FillRectangle(Brushes.Black, new Rectangle(x + 11, y + 13, 5, 7));
                else if (seats[3])
                    drawGrid(g, new Rectangle(x + 11, y + 13, 5, 7));
            }

            g.DrawRectangle(pen, new Rectangle(x + 2, y + 0, 16, 28));
        }

        public static void DrawBike(Graphics g, int x, int y, int pop_f, int pop_r, int pop_show, int seat, bool[] seats)
        {
            Pen pen = new Pen(Brushes.Black);
            if (pop_f == 0 || (pop_f == pop_show))
                g.DrawRectangle(pen, new Rectangle(x + 2, y + 0, 1, 5));
            if (pop_r == 0 || (pop_r == pop_show))
                g.DrawRectangle(pen, new Rectangle(x + 2, y + 25, 1, 5));

            if (seats.Length == 1)
            {
                g.DrawRectangle(pen, new Rectangle(x, y + 7, 5, 16));
                if (seat == 0)
                    g.FillRectangle(Brushes.Black, new Rectangle(x, y + 7, 5, 16));
                else if (seats[0])
                    drawGrid(g, new Rectangle(x, y + 7, 5, 16));
            }
            else
            {
                if (seats.Length > 1)
                {
                    g.DrawRectangle(pen, new Rectangle(x, y + 7, 5, 7));
                    if (seat == 0)
                        g.FillRectangle(Brushes.Black, new Rectangle(x, y + 7, 5, 7));
                    else if (seats[0])
                        drawGrid(g, new Rectangle(x, y + 7, 5, 7));
                }

                if (seats.Length >= 2)
                {
                    g.DrawRectangle(pen, new Rectangle(x, y + 16, 5, 7));
                    if (seat == 1)
                        g.FillRectangle(Brushes.Black, new Rectangle(x, y + 16, 5, 7));
                    else if (seats[1])
                        drawGrid(g, new Rectangle(x, y + 16, 5, 7));
                }
            }
        }

        private static void drawGrid(Graphics g, Rectangle rectangle)
        {
            Bitmap bm = new Bitmap(1, 1);
            bm.SetPixel(0, 0, Color.Black);

            for (int w = 0; w < rectangle.Width; w++)
            {
                for (int h = 0; h < rectangle.Height; h++)
                {
                    if ((w * rectangle.Width + h) % 2 == 1)
                    {
                        g.DrawImageUnscaled(bm, rectangle.X + w, rectangle.Y + h);
                    }

                }
            }
        }
    }
}
