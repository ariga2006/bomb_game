using System.Drawing;

public class Bomb
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
        }
    }
}