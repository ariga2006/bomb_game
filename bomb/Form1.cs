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
<<<<<<< HEAD
=======
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
=======
>>>>>>> 4b5cbbc (衝突消し)
   
>>>>>>> 0549bb0 (no message)
    public partial class Form1 : Form
    {
        private Timer gameTimer;
        private GameBoard board;

<<<<<<< HEAD
        public Form1()
        {
<<<<<<< HEAD
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
=======
            private Timer gameTimer;
            private GameBoard board;
>>>>>>> 4b5cbbc (衝突消し)

            gameTimer = new Timer();
            gameTimer.Interval = 100;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

<<<<<<< HEAD
=======

>>>>>>> 6b3911c1995c66d36f5fcc62d802ef05a443a901
            board = new GameBoard(20,20); // 15×15の盤面
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

<<<<<<< HEAD
            Invalidate();

        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            board.Update();

            Invalidate();
=======
            Invalidate(); // 再描画
>>>>>>> origin/master
        }

<<<<<<< HEAD
        private void Form1_Load(object sender, EventArgs e)
        {
            board = new GameBoard(15, 15);
        }
=======
                gameTimer = new Timer();
                gameTimer.Interval = 100; // 0.1秒ごとに更新
                gameTimer.Tick += GameTimer_Tick;
                gameTimer.Start();

             board = new GameBoard(20, 20);//15×15の盤面
            }

       
>>>>>>> 4b5cbbc (衝突消し)

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //矢印キーで移動
            if (e.KeyCode == Keys.Up) board.Player.Move(0, -1);
            if (e.KeyCode == Keys.Down) board.Player.Move(0, 1);
            if (e.KeyCode == Keys.Left) board.Player.Move(-1, 0);
            if (e.KeyCode == Keys.Right) board.Player.Move(1, 0);

            //スペースで爆弾設置
            if (e.KeyCode == Keys.Space) board.PlaceBomb();

            this.Invalidate();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            board.Update();
            this.Invalidate();
>>>>>>> 0549bb0 (no message)
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
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
}
=======
=======
>>>>>>> origin/master
=======

>>>>>>> 4b5cbbc (衝突消し)
}
>>>>>>> 0549bb0 (no message)
=======
}

>>>>>>> 6b3911c1995c66d36f5fcc62d802ef05a443a901
