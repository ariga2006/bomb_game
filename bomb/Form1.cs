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
        private Button startButton;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            gameTimer = new Timer();
            gameTimer.Interval = 100;
            gameTimer.Tick += GameTimer_Tick;
            // ★ 最初は止めておく
            gameTimer.Stop();

            // ★ ウィンドウサイズは固定（盤面に合わせるならここで設定）
            this.ClientSize = new Size(21 * 30, 21 * 30);

            // ★ スタートボタン作成
            startButton = new Button();
            startButton.Text = "START";
            startButton.Font = new Font("Arial", 24, FontStyle.Bold);
            startButton.Size = new Size(200, 80);
            // ★ フォーム中央に配置
            startButton.Location = new Point(
                (this.ClientSize.Width - startButton.Width) / 2,
                (this.ClientSize.Height - startButton.Height) / 2
            );
            startButton.Click += StartButton_Click;
            this.Controls.Add(startButton);

        }

        


        private void StartButton_Click(object sender, EventArgs e)
        {
            // ボタンを消す
            startButton.Visible = false;
            // すでにゲーム中なら再生成しない
            if (board == null || !board.Player.IsAlive)
            {
                board = new GameBoard(21, 21);
                gameTimer.Start();
                Invalidate();
            }


            // ★ ウィンドウサイズを盤面に合わせる
            this.ClientSize = new Size(board.Width * 30, board.Height * 30);
        }

       
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (board == null) return; // ★ ゲーム開始前は無効

            if (e.KeyCode == Keys.Up) board.Player.Move(0, -1, board);
            if (e.KeyCode == Keys.Down) board.Player.Move(0, 1, board);
            if (e.KeyCode == Keys.Left) board.Player.Move(-1, 0, board);
            if (e.KeyCode == Keys.Right) board.Player.Move(1, 0, board);

            if (e.KeyCode == Keys.W) board.Player.Move(0, -1, board);
            if (e.KeyCode == Keys.S) board.Player.Move(0, 1, board);
            if (e.KeyCode == Keys.A) board.Player.Move(-1, 0, board);
            if (e.KeyCode == Keys.D) board.Player.Move(1, 0, board);

            if (e.KeyCode == Keys.Space) board.PlaceBomb();
            Invalidate(); // ★ 爆弾設置後に再描画


        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (board != null)
            {
                board.Update();
                Invalidate();
            }
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