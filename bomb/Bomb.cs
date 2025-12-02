using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace bomb
{
    public partial class Bomb
        //爆弾の座標
    {
        public int X { get; }
        public int Y { get; }
        
        //爆弾のタイマー
        private int timer = 30;

        //爆弾を設置した位置
        public Bomb(int x, int y)
        {
            X = x;
            Y = y;
        }

        //爆発の処理
        public bool Tick()
        {
            timer--;
            return timer <= 0;
        }

        //爆弾の描画(1マスを20ピクセルとする)
        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Black, X * 20, Y * 20, 20, 20);
        }

    }
}
