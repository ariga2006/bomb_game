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

        public GameBoard(int width, int height)
        {
            Player = new Player(1, 1);
            bombs = new List<Bomb>();
        }

        public void PlaceBomb()
        {
            bombs.Add(new Bomb(Player.X, Player.Y));
        }

        public void Update()
        {
            for (int i = bombs.Count - 1; i >= 0; i--)
            {
                if (bombs[i].Tick())
                {
                    bombs.RemoveAt(i);
                }
            }
        }

        // ← これが必要！
        public void Draw(Graphics g)
        {
            // プレイヤー描画
            Player.Draw(g);

            // 爆弾描画
            foreach (var bomb in bombs)
            {
                bomb.Draw(g);
            }

        }
    }
}
