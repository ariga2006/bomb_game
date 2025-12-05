using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace bomb
{
    public class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Player(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move(int dx, int dy, GameBoard board)
        {
            int newX = X + dx;
            int newY = Y + dy;

            // 壁判定
            if (!board.IsWall(newX, newY))
            {
                X = newX;
                Y = newY;
            }
        }

        public void Draw(Graphics g, int cellSize)
        {
            g.FillRectangle(Brushes.Blue, X * cellSize, Y * cellSize, cellSize, cellSize);
        }
    }
}