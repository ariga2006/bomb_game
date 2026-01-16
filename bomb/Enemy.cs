using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace bomb
{
    public enum EnemyType
    {
        Random, // 1：ランダム移動
        Chase,  // 2：プレイヤー追跡
        Smart   // 5：ハイブリッド（60%追跡 / 40%ランダム）
    }

    public class Enemy
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private Random rand = new Random();
        private int moveDelay;   // 移動間隔（Tick数）
        private int tickCounter; // Tickカウンタ

        private int bombCooldown = 100; // 100tick は置けない
        private int bombTimer = 0;      // カウンタ

        public EnemyType Type { get; private set; }

        public Enemy(int startX, int startY, int speed, EnemyType type)
        {
            X = startX;
            Y = startY;
            moveDelay = speed;
            tickCounter = 0;
            Type = type;
        }


        public void Update(GameBoard board)
        {
            bombTimer++;

            // ★ プレイヤーとの距離
            int dist = Math.Abs(board.Player.X - X) + Math.Abs(board.Player.Y - Y);

            // ★ タイプごとの設定
            int cooldown = 120;   // デフォルト
            int baseChance = 5;   // デフォルト
            int bonus = 0;

            switch (Type)
            {
                case EnemyType.Random:
                    cooldown = 150;   // のんびり → 遅い
                    baseChance = 5;   // ほぼ置かない
                    if (dist <= 3) bonus = 10;
                    break;

                case EnemyType.Chase:
                    cooldown = 100;   // ちょっと早い
                    baseChance = 15;  // そこそこ置く
                    if (dist <= 3) bonus = 25;
                    break;

                case EnemyType.Smart:
                    cooldown = 60;    // かなり早い
                    baseChance = 25;  // 積極的
                    if (dist <= 3) bonus = 40;
                    break;
            }

            int finalChance = baseChance + bonus;


            if (dist <= 3) bonus = 45;   // 超近い → +45%
            else if (dist <= 5) bonus = 30; // 近い → +15%
            else bonus = 0;              // 遠い → 追加なし

            
            // クールダウン終了 + 確率で爆弾
            if (bombTimer >= bombCooldown && rand.Next(100) < finalChance)
            {
                CanPlaceBomb = true;   // ★ 復活させる
                TryPlaceBomb(board);
                bombTimer = 0;
            }

            tickCounter++;
            if (tickCounter >= moveDelay)
            {
                tickCounter = 0;
                Move(board);
            }
        }


        private void Move(GameBoard board)
        {
            // ★ 爆弾の爆発範囲にいるなら、最優先で逃げる
            if (board.IsDanger(X, Y))
            {
                MoveAwayFromDanger(board);
                return;
            }

            MoveRandom(board);   // ランダム移動
        }

        public bool CanPlaceBomb = true;

        //敵の爆弾所持
        public void TryPlaceBomb(GameBoard board)
        {
            if (!CanPlaceBomb) return;

            if (board.IsBomb(X, Y)) return;

            board.PlaceEnemyBomb(X, Y);

            CanPlaceBomb = false; // クールダウン（後で調整可能）
        }
        // 1：ランダム移動
        private void MoveRandom(GameBoard board)
        {
            int[][] dirs = {
                new int[]{1,0}, new int[]{-1,0},
                new int[]{0,1}, new int[]{0,-1}
            };

            var d = dirs[rand.Next(dirs.Length)];
            int nx = X + d[0];
            int ny = Y + d[1];

            

            if (!board.IsWall(nx, ny) && !board.IsBomb(nx, ny))
            {
                X = nx;
                Y = ny;
            }
        }

        //敵が逃げるための判定
        private void MoveAwayFromDanger(GameBoard board)
        {
            int[][] dirs = {
        new int[]{1,0}, new int[]{-1,0},
        new int[]{0,1}, new int[]{0,-1}
    };

            int bestScore = int.MinValue;
            int bestX = X;
            int bestY = Y;

            foreach (var d in dirs)
            {
                int nx = X + d[0];
                int ny = Y + d[1];

                // 移動できない場所は除外
                if (board.IsWall(nx, ny) || board.IsBomb(nx, ny))
                    continue;

                // ★ 安全度スコアを計算
                int score = EvaluateSafety(board, nx, ny);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestX = nx;
                    bestY = ny;
                }
            }

            // 最も安全な方向へ移動
            X = bestX;
            Y = bestY;
        }


        public void Draw(Graphics g, int cellSize)
        {
            Brush brush;

            switch (Type)
            {
                case EnemyType.Random:
                    brush = Brushes.Green;   // ランダム
                    break;

                case EnemyType.Chase:
                    brush = Brushes.Red;     // 追跡
                    break;

                case EnemyType.Smart:
                    brush = Brushes.Purple;    // スマート
                    break;

                default:
                    brush = Brushes.Gray;
                    break;
            }

            g.FillRectangle(brush, X * cellSize, Y * cellSize, cellSize, cellSize);
        }
        private int EvaluateSafety(GameBoard board, int x, int y)
        {
            int score = 0;

            // 危険地帯なら即アウト
            if (board.IsDanger(x, y))
                return -999;

            // 爆弾に近いほど危険
            if (board.IsBomb(x + 1, y)) score -= 50;
            if (board.IsBomb(x - 1, y)) score -= 50;
            if (board.IsBomb(x, y + 1)) score -= 50;
            if (board.IsBomb(x, y - 1)) score -= 50;

            // 壁に囲まれていると逃げにくい
            int wallCount = 0;
            if (board.IsWall(x + 1, y)) wallCount++;
            if (board.IsWall(x - 1, y)) wallCount++;
            if (board.IsWall(x, y + 1)) wallCount++;
            if (board.IsWall(x, y - 1)) wallCount++;
            score -= wallCount * 10;

            // プレイヤーに近いと危険（好みで調整）
            int distPlayer = Math.Abs(board.Player.X - x) + Math.Abs(board.Player.Y - y);
            score += distPlayer * 2;

            return score;
        }

    }
}