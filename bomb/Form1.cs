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

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            gameTimer = new Timer();
            gameTimer.Interval = 100;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            // ★ 盤面を 21×21 にする
            board = new GameBoard(21, 21);

            // ★ ウィンドウサイズを盤面に合わせる
            this.ClientSize = new Size(board.Width * 30, board.Height * 30);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // 矢印キー
            if (e.KeyCode == Keys.Up) board.Player.Move(0, -1, board);
            if (e.KeyCode == Keys.Down) board.Player.Move(0, 1, board);
            if (e.KeyCode == Keys.Left) board.Player.Move(-1, 0, board);
            if (e.KeyCode == Keys.Right) board.Player.Move(1, 0, board);

            // WASDキー
            if (e.KeyCode == Keys.W) board.Player.Move(0, -1, board);
            if (e.KeyCode == Keys.S) board.Player.Move(0, 1, board);
            if (e.KeyCode == Keys.A) board.Player.Move(-1, 0, board);
            if (e.KeyCode == Keys.D) board.Player.Move(1, 0, board);

            // スペースで爆弾設置
            if (e.KeyCode == Keys.Space) board.PlaceBomb();

            Invalidate();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            board.Update();
            Invalidate();
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
}