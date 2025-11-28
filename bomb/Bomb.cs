using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace bomb
{
    public partial class Bomb
    {
        public int X { get; }
        public int Y { get; }
        private int timer = 30;

        public Bomb(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Tick()
        {
            timer--;
            return timer <= 0;
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Black, X * 20, Y * 20, 20, 20);
        }

    }
}
