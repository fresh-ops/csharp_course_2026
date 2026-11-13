namespace App.Lab4;

public class ClockwiseComparer : IComparer<I2DVertex>
{
    private I2DVertex center;

    public ClockwiseComparer(I2DVertex center)
    {
        this.center = center;
    }

    /// <summary>
    /// Compares two I2DVertex clockwise. The start point is 12 o'clock.
    /// </summary>
    public int Compare(I2DVertex x, I2DVertex y)
    {
        var aX = x.X - center.X;
        var aY = x.Y - center.Y;

        var bX = y.X - center.X;
        var bY = y.Y - center.Y;

        var isAOnLeftSide = aX < 0 || (aX == 0 && aY >= 0);
        var isBOnLeftSide = bX < 0 || (bX == 0 && bY >= 0);

        if (isAOnLeftSide != isBOnLeftSide)
        {
            return isAOnLeftSide ? -1 : 1;
        }

        var product = aX * bY - bX * aY;
        return -Math.Sign(product);
    }
}
