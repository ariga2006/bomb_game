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

        // 爆発範囲を返す
        public List<Point> Explode(int range = 2)
        {
            List<Point> blast = new List<Point>();

            // 爆心地
            blast.Add(new Point(X, Y));

            // 上下左右に広げる
            for (int i = 1; i <= range; i++)
            {
                blast.Add(new Point(X + i, Y)); // 右
                blast.Add(new Point(X - i, Y)); // 左
                blast.Add(new Point(X, Y + i)); // 下
                blast.Add(new Point(X, Y - i)); // 上
            }

            return blast;
        }

        public void Draw(Graphics g, int cellSize)
        {
            g.FillEllipse(Brushes.Red, X * cellSize, Y * cellSize, cellSize, cellSize);
        }

        // 爆風を描画（黄色）
        public void DrawBlast(Graphics g, int cellSize, List<Point> blast)
        {
            foreach (var p in blast)
            {
                g.FillRectangle(Brushes.Yellow, p.X * cellSize, p.Y * cellSize, cellSize, cellSize);
            }
        }
    }
}