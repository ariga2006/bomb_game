using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace bomb
{
    public class Bomb
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        private int timer = 20; // Tick回数で寿命管理

        public Bomb(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Tick()
        {
            timer--;
            return timer <= 0; // trueなら爆発終了
        }

        public void Draw(Graphics g, int cellSize)
        {
            g.FillEllipse(Brushes.Red, X * cellSize, Y * cellSize, cellSize, cellSize);
        }
    }
}