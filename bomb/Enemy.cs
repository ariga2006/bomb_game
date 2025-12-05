using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bomb
{
    public class Enemy
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        private Random rand = new Random();
        
        // ★ 引数付きコンストラクタを追加
        public Enemy(int startX, int startY)
        {
            X = startX;
            Y = startY;
        }

        // ランダム移動（壁判定あり）
        public void Move(GameBoard board)
        {
            int dx = 0, dy = 0;
            int dir = rand.Next(4); // 0=上,1=下,2=左,3=右

            switch (dir)
            {
                case 0: dy = -1; break;
                case 1: dy = 1; break;
                case 2: dx = -1; break;
                case 3: dx = 1; break;
            }

            int newX = X + dx;
            int newY = Y + dy;

            if (!board.IsWall(newX, newY))
            {
                X = newX;
                Y = newY;
            }
        }

        public void Draw(Graphics g, int cellSize)
        {
            g.FillRectangle(Brushes.Green, X * cellSize, Y * cellSize, cellSize, cellSize);
        }
    }
}