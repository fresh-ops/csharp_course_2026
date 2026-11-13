namespace App.Lab4;

public class Simple2DVertex : I2DVertex
{
    public double X { get; set; }
    public double Y { get; set; }

    public Simple2DVertex(double x, double y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"(x = {X:F3}, y = {Y:F3})";
}
