using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace bomb
{
    using System.Drawing;

    namespace bomb   // GameBoard と同じ namespace に揃える
    {
        public class Player
        {
            // 座標（マス単位）
            public int X { get; private set; }
            public int Y { get; private set; }

            // ライフ（残機）
            public int Lives { get; private set; } = 3;

            // コンストラクタ：初期位置を指定
            public Player(int startX, int startY)
            {
                X = startX;
                Y = startY;
            }

            // 移動処理
            public void Move(int dx, int dy)
            {
                X += dx;
                Y += dy;
            }

            // ダメージ処理（ライフを減らす）
            public void Damage()
            {
                if (Lives > 0)
                    Lives--;
            }

            // ライフ回復
            public void Heal()
            {
                Lives++;
            }

            // 描画処理
            public void Draw(Graphics g)
            {
                int cellSize = 20; // 1マスのサイズ
                g.FillRectangle(Brushes.Blue, X * cellSize, Y * cellSize, cellSize, cellSize);

                // ライフ表示（左上に赤丸で残機を描画）
                for (int i = 0; i < Lives; i++)
                {
                    g.FillEllipse(Brushes.Red, 5 + i * 15, 5, 10, 10);
                }
            }
            public void Move(string direction)
            {
                switch (direction.ToLower())
                {
                    case "up": Move(0, -1); break;
                    case "down": Move(0, 1); break;
                    case "left": Move(-1, 0); break;
                    case "right": Move(1, 0); break;
                }
            }

        }
    }
}
