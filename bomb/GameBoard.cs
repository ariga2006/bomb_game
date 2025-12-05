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
        private List<Enemy> enemies;   // ← 敵リスト追加
        private int[,] map; // 盤面データ
        private int cellSize = 30;

        public GameBoard(int width, int height)
        {
            Player = new Player();
            bombs = new List<Bomb>();
            enemies = new List<Enemy>();
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
            // 敵を生成（例：2体）
            enemies.Add(new Enemy(5, 5));
            enemies.Add(new Enemy(width - 3, height - 3));
        }

        public bool IsWall(int x, int y)
        {
            return map[y, x] == 1;
        }

        public void PlaceBomb()
        {
            bombs.Add(new Bomb(Player.X, Player.Y));
        }
        private int enemyTickCounter = 0;

        public void Update()
        {
            // --- 爆弾更新 ---
            for (int i = bombs.Count - 1; i >= 0; i--)
            {
                var bomb = bombs[i];
                if (bomb == null) { bombs.RemoveAt(i); continue; }

                bomb.Tick();                  // 状態だけ更新
                if (bomb.IsFinished())        // 終了判定は別メソッド
                {
                    bombs.RemoveAt(i);
                }
            }

            // --- 敵の移動 ---
            enemyTickCounter++;
            if (enemyTickCounter >= 5)
            {
                enemyTickCounter = 0;
                foreach (var enemy in enemies)
                {
                    enemy.Move(this);
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
            // 敵描画 ← ここを追加！
            foreach (var enemy in enemies)
            {
                enemy.Draw(g, cellSize);
            }
        }

    }
    }