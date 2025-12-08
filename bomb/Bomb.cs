using System.Drawing;
<<<<<<< HEAD

<<<<<<< HEAD


namespace bomb
=======
public class Bomb
>>>>>>> 5d676e8 (爆風)
=======

public class Bomb
>>>>>>> 6b3911c1995c66d36f5fcc62d802ef05a443a901
{
    public int X { get; private set; }
    public int Y { get; private set; }
    private int timer = 20; // Tick回数で寿命管理
    private int range = 1;  // 爆発範囲
    public bool HasExploded { get; private set; } = false;
    private int explosionTimer = 5; // 爆風を表示する時間

    public Bomb(int x, int y, int range = 1)
    {
        X = x;
        Y = y;
        this.range = range;
    }

    public void Tick()
    {
        if (!HasExploded)
        {
            timer--;
            if (timer <= 0)
            {
                HasExploded = true;
            }
<<<<<<< HEAD
        }
        else
        {
            explosionTimer--;
        }
    }

<<<<<<< HEAD
        // 爆発範囲を返す
        public List<Point> Explode(int range = 2)
        {
            List<Point> blast = new List<Point>();

            // 爆心地
            blast.Add(new Point(X, Y));

            // 上下左右に広げる
            for (int i = 1; i <= range; i++)
            {
                blast.Add(new Point(X + i, Y)); // 右
                blast.Add(new Point(X - i, Y)); // 左
                blast.Add(new Point(X, Y + i)); // 下
                blast.Add(new Point(X, Y - i)); // 上
            }

            return blast;
=======
>>>>>>> 6b3911c1995c66d36f5fcc62d802ef05a443a901
        }
        else
        {
            explosionTimer--;
        }
    }

    public bool IsFinished()
    {
        return HasExploded && explosionTimer <= 0;
    }

    public void Draw(Graphics g, int cellSize)
    {
        if (!HasExploded)
        {
            g.FillEllipse(Brushes.Red, X * cellSize, Y * cellSize, cellSize, cellSize);
        }
<<<<<<< HEAD

        // 爆風を描画（黄色）
        public void DrawBlast(Graphics g, int cellSize, List<Point> blast)
        {
            foreach (var p in blast)
            {
                g.FillRectangle(Brushes.Yellow, p.X * cellSize, p.Y * cellSize, cellSize, cellSize);
            }
=======
    public bool IsFinished()
    {
        return HasExploded && explosionTimer <= 0;
    }

    public void Draw(Graphics g, int cellSize)
    {
        if (!HasExploded)
        {
            g.FillEllipse(Brushes.Red, X * cellSize, Y * cellSize, cellSize, cellSize);
        }
=======
>>>>>>> 6b3911c1995c66d36f5fcc62d802ef05a443a901
        else
        {
            DrawExplosion(g, cellSize);
        }
    }

    public void DrawExplosion(Graphics g, int cellSize)
    {
        Brush explosionBrush = Brushes.Orange;

        // 爆弾の中心
        g.FillRectangle(explosionBrush, X * cellSize, Y * cellSize, cellSize, cellSize);

        // 上下左右に爆風を描画
        for (int i = 1; i <= range; i++)
        {
            g.FillRectangle(explosionBrush, X * cellSize, (Y - i) * cellSize, cellSize, cellSize);
            g.FillRectangle(explosionBrush, X * cellSize, (Y + i) * cellSize, cellSize, cellSize);
            g.FillRectangle(explosionBrush, (X - i) * cellSize, Y * cellSize, cellSize, cellSize);
            g.FillRectangle(explosionBrush, (X + i) * cellSize, Y * cellSize, cellSize, cellSize);
<<<<<<< HEAD
>>>>>>> 5d676e8 (爆風)
=======
>>>>>>> 6b3911c1995c66d36f5fcc62d802ef05a443a901
        }
    }
}