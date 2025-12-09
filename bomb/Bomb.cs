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
            int[][] dirs = new int[][]
            {
        new int[] { 1, 0 },   // 右
        new int[] { -1, 0 },  // 左
        new int[] { 0, 1 },   // 下
        new int[] { 0, -1 }   // 上
            };

            foreach (var dir in dirs)
            {
                int dx = dir[0];
                int dy = dir[1];

                for (int i = 1; i <= range; i++)
                {
                    int nx = X + dx * i;
                    int ny = Y + dy * i;

                    // 範囲外チェック（map があるならここで判定）
                    if (nx < 0 || ny < 0 || nx >= map.GetLength(1) || ny >= map.GetLength(0))
                        break;

                    // ★ 壊せない壁なら爆風を止める
                    if (map[ny, nx] == 1)
                        break;

                    // 爆風に追加
                    blast.Add(new Point(nx, ny));

                    // 壊せる障害物なら消すが、その先も広がる
                    if (map[ny, nx] == 2)
                    {
                        map[ny, nx] = 0; // 消す
                                         // break しない → 爆風はさらに広がる
                    }
                }

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