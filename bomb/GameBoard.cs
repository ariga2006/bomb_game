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
        private List<Point> blasts = new List<Point>();
        private int blastTimer = 0; // 爆風の寿命管理
        private bool isGameClear = false;
        // 起動時メッセージ表示用
        private bool showStartMessage = true;     // 表示するかどうか
        private int startMessageTimer = 10;      // 表示時間（Update呼び出し回数で管理）
        public void PlaceBomb()
        {
            bombs.Add(new Bomb(Player.X, Player.Y, map));
        }

        public int Width => map.GetLength(1);
        public int Height => map.GetLength(0);
        public int[,] Map => map; // 爆風判定用に公開

        public GameBoard(int width, int height)
        {
            Player = new Player(1, 1);
            bombs = new List<Bomb>();
            enemies = new List<Enemy>();
            map = new int[height, width];

            // 敵の初期位置リスト
            List<Point> enemyStarts = new List<Point>
            {
                new Point(5, 5),
                new Point(width - 3, height - 3)
            };

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
                        // プレイヤー周囲は必ず空白
                        if (Math.Abs(x - Player.X) <= 1 && Math.Abs(y - Player.Y) <= 1)
                        {
                            map[y, x] = 0;
                            continue;
                        }

                        // 敵周囲も必ず空白
                        bool nearEnemy = false;
                        foreach (var e in enemyStarts)
                        {
                            if (Math.Abs(x - e.X) <= 1 && Math.Abs(y - e.Y) <= 1)
                            {
                                nearEnemy = true;
                                break;
                            }
                        }
                        if (nearEnemy)
                        {
                            map[y, x] = 0;
                            continue;
                        }

                        // ランダム配置
                        int r = rand.Next(100);
                        if (r < 15) map[y, x] = 1; // 壊せない壁
                        else if (r < 35) map[y, x] = 2; // 壊せる障害物
                        else map[y, x] = 0; // 空白
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

        

        public void Update()
        {// 起動時メッセージが表示中ならタイマーを減らす
            if (showStartMessage)
            {
                startMessageTimer--;
                if (startMessageTimer <= 0)
                {
                    showStartMessage = false; // 一定時間経過で非表示にする
                }
                return; // メッセージ表示中はゲーム更新しない
            }


            // 爆弾更新
            for (int i = bombs.Count - 1; i >= 0; i--)
            {
                if (bombs[i].Tick())
                {
                    var blast = bombs[i].Explode();

                    blasts.AddRange(blast);
                    blastTimer = 5; // 爆風を5Tick残す

                    foreach (var p in blast)
                    {
                        if (p.X < 0 || p.Y < 0 || p.X >= map.GetLength(1) || p.Y >= map.GetLength(0))
                            continue;

                        if (map[p.Y, p.X] == 2)
                            map[p.Y, p.X] = 0;

                        if (Player.X == p.X && Player.Y == p.Y)
                            Player.Kill();

                        enemies.RemoveAll(e => e.X == p.X && e.Y == p.Y);
                    }

                    bombs.RemoveAt(i);
                }
            }

            if (blastTimer > 0)
            {
                blastTimer--;
                if (blastTimer == 0)
                    blasts.Clear();
            }

            foreach (var enemy in enemies)
            {
                enemy.Update(this);

                if (enemy.X == Player.X && enemy.Y == Player.Y)
                {
                    Player.Kill();
                }
            }

            if (enemies.Count == 0 && Player.IsAlive)
            {
                isGameClear = true;
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
                        // 壊せない壁 → 灰色レンガ模様
                        Rectangle rect = new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize);
                        g.FillRectangle(Brushes.Gray, rect);

                        g.DrawLine(Pens.Black, rect.Left, rect.Top + cellSize / 2, rect.Right, rect.Top + cellSize / 2);
                        g.DrawLine(Pens.Black, rect.Left + cellSize / 2, rect.Top, rect.Left + cellSize / 2, rect.Top + cellSize / 2);
                    }
                    else if (map[y, x] == 2)
                    {
                        // 壊せる壁 → 茶色の木箱風
                        Rectangle rect = new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize);
                        g.FillRectangle(Brushes.SaddleBrown, rect);

                        g.DrawRectangle(Pens.Black, rect);
                        g.DrawLine(Pens.Black, rect.Left, rect.Top, rect.Right, rect.Bottom);
                        g.DrawLine(Pens.Black, rect.Right, rect.Top, rect.Left, rect.Bottom);
                    }
                }
            }

            // プレイヤー描画
            Player.Draw(g, cellSize);

            // 爆弾描画
            foreach (var bomb in bombs)
                bomb.Draw(g, cellSize);

            // 爆風描画（黄色）
            foreach (var p in blasts)
                g.FillRectangle(Brushes.Yellow, p.X * cellSize, p.Y * cellSize, cellSize, cellSize);

            // 敵描画
            foreach (var enemy in enemies)
                enemy.Draw(g, cellSize);
            // ★ 死亡後にゲームオーバー表示
            if (!Player.IsAlive)
            {
                string text = "GAME OVER";
                Font font = new Font("Arial", 32, FontStyle.Bold);
                SizeF textSize = g.MeasureString(text, font);

                float centerX = (map.GetLength(1) * cellSize - textSize.Width) / 2;
                float centerY = (map.GetLength(0) * cellSize - textSize.Height) / 2;

                g.DrawString(text, font, Brushes.Red, centerX, centerY);
            }
            // ★ ゲームクリア表示
            if (isGameClear)
            {
                string text = "GAME CLEAR!";
                Font font = new Font("Arial", 32, FontStyle.Bold);
                SizeF textSize = g.MeasureString(text, font);

                float centerX = (map.GetLength(1) * cellSize - textSize.Width) / 2;
                float centerY = (map.GetLength(0) * cellSize - textSize.Height) / 2;

                g.DrawString(text, font, Brushes.Green, centerX, centerY);
            }
        }


        public bool IsBomb(int x, int y)
        {
            foreach (var bomb in bombs)
            {
                if (bomb.X == x && bomb.Y == y)
                    return true;
            }
            return false;
        }
    }
}
