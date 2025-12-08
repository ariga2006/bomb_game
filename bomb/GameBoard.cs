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
        private List<Enemy> enemies;
        private int[,] map; // 盤面データ
        private int cellSize = 30;
        private Random rand = new Random();

        public GameBoard(int width, int height)
        {
            Player = new Player(1, 1);
            bombs = new List<Bomb>();
            enemies = new List<Enemy>();
            map = new int[height, width];

            // 外周は壊せない壁
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {
                        map[y, x] = 1; // 壊せない壁
                    }
                    else
                    {
                        // ランダムで障害物を追加
                        if (!(x == Player.X && y == Player.Y))
                        {
                            int r = rand.Next(100);
                            if (r < 15) map[y, x] = 1; // 15% 壊せない壁
                            else if (r < 35) map[y, x] = 2; // 20% 壊せる障害物
                            else map[y, x] = 0; // 空白
                        }
                        else
                        {
                            map[y, x] = 0; // プレイヤー位置は空白
                        }
                    }
                }
            }

            // 敵を生成
            enemies.Add(new Enemy(5, 5, 3));
            enemies.Add(new Enemy(width - 3, height - 3, 4));
        }

        public bool IsWall(int x, int y)
        {
            return map[y, x] == 1 || map[y, x] == 2;
        }

        public void PlaceBomb()
        {
            bombs.Add(new Bomb(Player.X, Player.Y));
        }

        public void Update()
        {
            // --- 爆弾更新 ---
            for (int i = bombs.Count - 1; i >= 0; i--)
            {
<<<<<<< HEAD
                bombs[i].Tick();
                if (bombs[i].IsFinished())
=======
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
>>>>>>> 6b3911c1995c66d36f5fcc62d802ef05a443a901
                {
<<<<<<< HEAD
                    // 爆発処理
                    var blast = bombs[i].Explode();
                    foreach (var p in blast)
                    {
                        // 壊せる障害物なら消す
                        if (map[p.Y, p.X] == 2)
                        {
                            map[p.Y, p.X] = 0;
                        }

                        // プレイヤーが爆風にいたら死亡
                        if (Player.X == p.X && Player.Y == p.Y)
                        {
                            Player.Kill();
                        }
                    }

                    bombs.RemoveAt(i);
=======
                    bombs.RemoveAt(i); // 爆風表示が終わったら削除
>>>>>>> 5d676e8 (爆風)
                }
            }
<<<<<<< HEAD

            // 敵更新
            foreach (var enemy in enemies)
            {
                enemy.Update(this);
            }
=======
>>>>>>> 6b3911c1995c66d36f5fcc62d802ef05a443a901
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
                        g.FillRectangle(Brushes.Gray, x * cellSize, y * cellSize, cellSize, cellSize); // 壊せない壁
                    }
                    else if (map[y, x] == 2)
                    {
                        g.FillRectangle(Brushes.Brown, x * cellSize, y * cellSize, cellSize, cellSize); // 壊せる障害物
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

            // 敵描画
            foreach (var enemy in enemies)
            {
                enemy.Draw(g, cellSize);
            }
        }

    }
    }