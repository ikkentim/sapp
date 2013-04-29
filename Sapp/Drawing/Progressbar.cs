using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Sapp.Drawing
{
    static class Progressbar
    {

        public static void Drawing(Graphics g, Point p)
        {

        }

       public static void Draw(Graphics g, Rectangle rectangle, Direction dir, float percentage, bool overbar=false)
        {
            switch (dir)
            {
                case Direction.Right:
                    g.DrawRectangle(new Pen(Brushes.Black), rectangle);
                    g.FillRectangle(Brushes.Black, new Rectangle(rectangle.X, rectangle.Y, (int)Math.Ceiling(percentage / 100 * rectangle.Width), rectangle.Height));
                    if (overbar) 
                        g.DrawLine(new Pen(Brushes.Black), new Point(rectangle.X + (int)Math.Ceiling(percentage / 100 * rectangle.Width), rectangle.Y - 2), new Point(rectangle.X + (int)Math.Ceiling(percentage / 100 * rectangle.Width), rectangle.Y + rectangle.Height + 2));
                    break;
                case Direction.Left:
                    g.DrawRectangle(new Pen(Brushes.Black), rectangle);
                    g.FillRectangle(Brushes.Black, new Rectangle(rectangle.X + rectangle.Width - (int)Math.Ceiling(percentage / 100 * rectangle.Width), rectangle.Y, (int)Math.Ceiling(percentage / 100 * rectangle.Width), rectangle.Height));
                    if (overbar)
                        g.DrawLine(new Pen(Brushes.Black), new Point(rectangle.X + rectangle.Width + (int)Math.Ceiling(percentage / 100 * rectangle.Width), rectangle.Y - 2), new Point(rectangle.X + (int)Math.Ceiling(percentage / 100 * rectangle.Width), rectangle.Y + rectangle.Height + 2));

                    break;
                case Direction.Up:
                    g.DrawRectangle(new Pen(Brushes.Black), rectangle);
                    g.FillRectangle(Brushes.Black, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - (int)Math.Ceiling(percentage / 100 * rectangle.Height), rectangle.Width, (int)Math.Ceiling(percentage / 100 * rectangle.Height)));
                    if (overbar)
                        g.DrawLine(new Pen(Brushes.Black), new Point(rectangle.X - 2, rectangle.Y + rectangle.Height - (int)Math.Ceiling(percentage / 100 * rectangle.Height)), new Point(rectangle.X + rectangle.Width + 2, rectangle.Y + rectangle.Height - (int)Math.Ceiling(percentage / 100 * rectangle.Height)));
                    break;
                case Direction.Down:
                    g.DrawRectangle(new Pen(Brushes.Black), rectangle);
                    g.FillRectangle(Brushes.Black, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, (int)Math.Ceiling(percentage / 100 * rectangle.Height)));
                    if (overbar)
                        g.DrawLine(new Pen(Brushes.Black), new Point(rectangle.X - 2, rectangle.Y + (int)Math.Ceiling(percentage / 100 * rectangle.Height)), new Point(rectangle.X + rectangle.Width + 2, rectangle.Y + (int)Math.Ceiling(percentage / 100 * rectangle.Height)));
                    break;
            }
    }
}
