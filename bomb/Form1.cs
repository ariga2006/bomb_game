using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bomb
{
    
        public partial class Form1 : Form
        {
            private Timer gameTimer;
            private GameBoard board;
            private Player player;

            public Form1()
            {
                InitializeComponent();
                this.KeyPreview = true;
                this.KeyDown += Form1_KeyDown;

                gameTimer = new Timer();
                gameTimer.Interval = 100; // 0.1秒ごとに更新
                gameTimer.Tick += GameTimer_Tick;
                gameTimer.Start();
            }

            private void Form1_Load(object sender, EventArgs e)
            {
                board = new GameBoard(15, 15); // ← 幅と高さを渡す
                player = new Player(1, 1);     // ← Player も初期位置を渡すと良い
            }

            private void Form1_KeyDown(object sender, KeyEventArgs e)
            {
                // 矢印キーで移動、スペースで爆弾設置
            }

            private void GameTimer_Tick(object sender, EventArgs e)
            {
                // 爆弾タイマーや敵の動きを更新
                this.Invalidate(); // 再描画
            }


        }

    
}
