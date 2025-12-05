using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace bomb
{
    // (´・ω:;.:...
    public partial class Player
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Player(int startX, int startY)
        {
            X = startX;
            Y = startY;
        }

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Blue, X * 20, Y * 20, 20, 20);
        }

    }
}
