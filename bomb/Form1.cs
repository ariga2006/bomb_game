using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private Timer delayTimer;
        private bool isGameEnded = false;

        // ★追加：「最初の1回だけメッセージを出したか？」
        private bool hasShownStartMessage = false;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            gameTimer = new Timer();
            gameTimer.Interval = 100;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Stop();

            startButton = new Button();
            startButton.Text = "START";
            startButton.Font = new Font("Arial", 24, FontStyle.Bold);
            startButton.Size = new Size(200, 80);
            startButton.Location = new Point(
                (this.ClientSize.Width - startButton.Width) / 2,
                (this.ClientSize.Height - startButton.Height) / 2
            );
            startButton.Click += StartButton_Click;
            this.Controls.Add(startButton);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            startButton.Visible = false;

            if (board == null || !board.Player.IsAlive)
            {
                board = new GameBoard(21, 21);
                gameTimer.Start();
                Invalidate();
            }

            // ★ 最初の1回だけスタートメッセージを出す
            if (!hasShownStartMessage)
            {
                board.ShowStartMessage();
                hasShownStartMessage = true;
            }

            this.ClientSize = new Size(board.Width * 30, board.Height * 30);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // ★ 終了中ならスペースキーで再挑戦
            if (isGameEnded && e.KeyCode == Keys.Space)
            {
                board = new GameBoard(21, 21);
                gameTimer.Start();
                isGameEnded = false;
                startButton.Visible = false;

                // ★ここでは絶対に ShowStartMessage() を呼ばない
                Invalidate();
                return;
            }

            if (board == null) return;
            if (isGameEnded) return;
            if (!board.Player.IsAlive) return;
            if (board.IsGameClear) return;

            if (e.KeyCode == Keys.Up) board.Player.Move(0, -1, board);
            if (e.KeyCode == Keys.Down) board.Player.Move(0, 1, board);
            if (e.KeyCode == Keys.Left) board.Player.Move(-1, 0, board);
            if (e.KeyCode == Keys.Right) board.Player.Move(1, 0, board);

            if (e.KeyCode == Keys.W) board.Player.Move(0, -1, board);
            if (e.KeyCode == Keys.S) board.Player.Move(0, 1, board);
            if (e.KeyCode == Keys.A) board.Player.Move(-1, 0, board);
            if (e.KeyCode == Keys.D) board.Player.Move(1, 0, board);

            if (e.KeyCode == Keys.Space) board.PlaceBomb();

            Invalidate();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (board != null)
            {
                board.Update();

                // ゲーム終了したらフラグを立てる
                if (!board.Player.IsAlive || board.IsGameClear)
                {
                    isGameEnded = true;
                }

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