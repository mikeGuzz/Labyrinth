using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace Labyrinth
{
    public class Collision
    {
        public Rectangle Bounds { get; set; }
        public bool IsTrigger { get; set; }
        public bool IsEnabled { get; set; } = true;

        public Collision(int x, int y, int w, int h)
        {
            Bounds = new Rectangle(x, y, w, h);
        }

        public Collision(Rectangle bounds)
        {
            Bounds = bounds;
        }

        public Collision(int x, int y, int w, int h, bool isTrigger)
        {
            Bounds = new Rectangle(x, y, w, h);
            IsTrigger = isTrigger;
        }

        public Collision(int x, int y, int w, int h, bool isTrigger, bool isEnabled)
        {
            Bounds = new Rectangle(x, y, w, h);
            IsTrigger = isTrigger;
            IsEnabled = isEnabled;
        }

        public Collision(Rectangle bounds, bool isTrigger)
        {
            Bounds = bounds;
            IsTrigger = isTrigger;
        }

        public Collision(Rectangle bounds, bool isTrigger, bool isEnabled)
        {
            Bounds = bounds;
            IsTrigger = isTrigger;
            IsEnabled = isEnabled;
        }

        public void Draw(Graphics g, Pen pen)
        {
            if(!IsEnabled) 
                return;
            g.DrawRectangle(pen, Bounds);
        }

        public void Fill(Graphics g, Brush brush)
        {
            if (!IsEnabled)
                return;
            g.FillRectangle(brush, Bounds);
        }

        public bool IsHit(Point p)
        {
            return p.X > Bounds.X && p.Y > Bounds.Y && p.X < Bounds.X + Bounds.Width && p.Y < Bounds.Y + Bounds.Height;
        }

        public bool Collide(Rectangle r)
        {
            return Bounds.IntersectsWith(r);
        }
    }
}
