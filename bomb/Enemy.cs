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
            tickCounter++;
            if (tickCounter >= moveDelay)
            {
                tickCounter = 0;
                Move(board);
            }
        }

        private void Move(GameBoard board)
        {
            switch (Type)
            {
                case EnemyType.Random:
                    MoveRandom(board);
                    break;

                case EnemyType.Chase:
                    MoveChase(board);
                    break;

                case EnemyType.Smart:
                    MoveSmart(board);
                    break;
            }
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

        // 2：プレイヤー追跡
        private void MoveChase(GameBoard board)
        {
            int dx = 0, dy = 0;

            // ★ まず X 方向を優先して追う
            if (board.Player.X > X) dx = 1;
            else if (board.Player.X < X) dx = -1;

            // X 方向に動けるなら動く
            if (dx != 0)
            {
                int nx = X + dx;
                if (!board.IsWall(nx, Y) && !board.IsBomb(nx, Y))
                {
                    X = nx;
                    return;
                }
            }

            // ★ X が無理なら Y 方向を試す
            if (board.Player.Y > Y) dy = 1;
            else if (board.Player.Y < Y) dy = -1;

            int ny2 = Y + dy;
            if (!board.IsWall(X, ny2) && !board.IsBomb(X, ny2))
            {
                Y = ny2;
            }
        }

        // 5：ハイブリッド（60%追跡 / 40%ランダム）
        private void MoveSmart(GameBoard board)
        {
            if (rand.Next(100) < 60)
                MoveChase(board);   // 60% 追跡
            else
                MoveRandom(board);  // 40% ランダム
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