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

            board = new GameBoard(15, 15); // ← ここで初期化

            gameTimer = new Timer();
            gameTimer.Interval = 100;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            board = new GameBoard(15, 15); // 幅と高さを渡す
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
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
}