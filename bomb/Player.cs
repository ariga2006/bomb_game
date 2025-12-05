using System;
using System.Drawing;

namespace bomb
{
    public class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsAlive { get; private set; } = true; // 生存フラグ

        public Player(int startX = 1, int startY = 1) // ← デフォルトを (1,1) に
        {
            X = startX;
            Y = startY;
        }

        public void Move(int dx, int dy, GameBoard board)
        {
            if (!IsAlive) return; // 死んでいたら動けない

            int newX = X + dx;
            int newY = Y + dy;

            // 壁判定
            if (!board.IsWall(newX, newY))
            {
                X = newX;
                Y = newY;
            }
        }

        // やられ判定
        public void Kill()
        {
            IsAlive = false;
        }

        public void Draw(Graphics g, int cellSize)
        {
            if (IsAlive)
            {
                g.FillRectangle(Brushes.Blue, X * cellSize, Y * cellSize, cellSize, cellSize);
            }
            else
            {
                g.FillRectangle(Brushes.DarkRed, X * cellSize, Y * cellSize, cellSize, cellSize);
            }
        }
    }
}