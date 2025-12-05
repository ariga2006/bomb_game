using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace bomb
{
    public class GameBoard
    {
        public Player Player { get; private set; }
        private List<Bomb> bombs;
        private int[,] map; // 盤面データ
        private int cellSize = 30;

        public GameBoard(int width, int height)
        {
            Player = new Player(1, 1);
            bombs = new List<Bomb>();
            map = new int[height, width];

            // 外周を壁にする
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {
                        map[y, x] = 1; // 壁
                    }
                    else
                    {
                        map[y, x] = 0; // 空白
                    }
                }
            }
        }

        public bool IsWall(int x, int y)
        {
            return map[y, x] == 1;
        }

        public void PlaceBomb()
        {
            bombs.Add(new Bomb(Player.X, Player.Y));
        }

        public void Update()
        {
            for (int i = bombs.Count - 1; i >= 0; i--)
            {
                bombs[i].Tick();
                if (bombs[i].IsFinished())
                {
                    bombs.RemoveAt(i); // 爆風表示が終わったら削除
                }
            }
        }

        public void Draw(Graphics g)
        {
            // 壁描画
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == 1)
                    {
                        g.FillRectangle(Brushes.Gray, x * cellSize, y * cellSize, cellSize, cellSize);
                    }
                }
            }

            // プレイヤー描画
            Player.Draw(g, cellSize);

            // 爆弾 or 爆風描画
            foreach (var bomb in bombs)
            {
                bomb.Draw(g, cellSize);
            }
        }

    }
    }