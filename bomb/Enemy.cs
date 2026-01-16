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
       
        private int bombCooldown = 180; // 爆弾を置く間隔
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
            //  一定間隔で爆弾を置く
            bombTimer++;
            if (bombTimer >= bombCooldown)
            {
                bombTimer = 0;
                TryPlaceBomb(board);
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

            // ランダム順に試す
            foreach (var d in dirs.OrderBy(x => rand.Next()))
            {
                int nx = X + d[0];
                int ny = Y + d[1];

                // 壁・爆弾・危険地帯を避ける
                if (!board.IsWall(nx, ny) &&
                    !board.IsBomb(nx, ny) &&
                    !board.IsDanger(nx, ny))
                {
                    X = nx;
                    Y = ny;
                    return;
                }
            }
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

       
    }
}