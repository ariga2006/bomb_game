using System;
using System.Drawing;

namespace bomb
{
    public class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsAlive { get; private set; } = true; // 生存フラグ
        private DateTime lastBombTime = DateTime.MinValue; // 最後に爆弾を置いた時刻

        public Player(int startX = 1, int startY = 1) // ← デフォルトを (1,1) に
        {
            X = startX;
            Y = startY;
        }

        public void Move(int dx, int dy, GameBoard board)
        {
            if (!IsAlive) return; // 死んでいたら動けない

            int newX = X + dx;
            int newY = Y + dy;

            // 壁 or 爆弾がある場所には移動できない
            if (!board.IsWall(newX, newY) && !board.IsBomb(newX, newY))
            {
                X = newX;
                Y = newY;
            }
        }


        // やられ判定
        public void Kill()
        {
            IsAlive = false;
        }
        // ★ クールタイム付き爆弾設置
        public void PlaceBomb(GameBoard board)
        {
            // 例: 800ms のクールタイム
            if ((DateTime.Now - lastBombTime).TotalMilliseconds < 800)
                return; // まだクールタイム中なら何もしない

            lastBombTime = DateTime.Now;
            board.PlaceBombInternal(this); // GameBoard 側の内部処理を呼ぶ
        }


        public void Draw(Graphics g, int cellSize)
        {
            if (IsAlive)
            {
                g.FillRectangle(Brushes.Blue, X * cellSize, Y * cellSize, cellSize, cellSize);
            }
            else
            {
                Pen pen = new Pen(Color.Red, 3);
                g.DrawLine(pen, X * cellSize, Y * cellSize,
                           (X + 1) * cellSize, (Y + 1) * cellSize);
                g.DrawLine(pen, (X + 1) * cellSize, Y * cellSize,
                           X * cellSize, (Y + 1) * cellSize);
            }
        }
  
    }
}