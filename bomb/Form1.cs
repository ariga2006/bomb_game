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
<<<<<<< HEAD
    public partial class Form1 : Form
    {
        private Timer gameTimer;
        private GameBoard board;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load; // ← これが必要！
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            gameTimer = new Timer();
            gameTimer.Interval = 100;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
=======
   
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

        private void Form1_KeyDow(object sender, KeyEventArgs e) // ← KeyDown に修正
        {
            // 矢印キー
            if (e.KeyCode == Keys.Up) board.Player.Move(0, -1);
            if (e.KeyCode == Keys.Down) board.Player.Move(0, 1);
            if (e.KeyCode == Keys.Left) board.Player.Move(-1, 0);
            if (e.KeyCode == Keys.Right) board.Player.Move(1, 0);

            // WASDキー
            if (e.KeyCode == Keys.W) board.Player.Move(0, -1);
            if (e.KeyCode == Keys.S) board.Player.Move(0, 1);
            if (e.KeyCode == Keys.A) board.Player.Move(-1, 0);
            if (e.KeyCode == Keys.D) board.Player.Move(1, 0);

            // スペースで爆弾設置
            if (e.KeyCode == Keys.Space) board.PlaceBomb();

            Invalidate(); // 再描画
>>>>>>> origin/master
        }

<<<<<<< HEAD
        private void Form1_Load(object sender, EventArgs e)
        {
            board = new GameBoard(15, 15);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) board.Player.Move(0, -1);
            if (e.KeyCode == Keys.Down) board.Player.Move(0, 1);
            if (e.KeyCode == Keys.Left) board.Player.Move(-1, 0);
            if (e.KeyCode == Keys.Right) board.Player.Move(1, 0);
            if (e.KeyCode == Keys.Space) board.PlaceBomb();

            this.Invalidate();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            board.Update();
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (board != null)
            {
                board.Draw(e.Graphics);
            }
        }
    }
=======
>>>>>>> origin/master
}
