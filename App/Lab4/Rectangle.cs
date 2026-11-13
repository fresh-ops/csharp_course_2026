namespace App.Lab4;

public class Rectangle : IGeometry
{
    private const int RectangleVertexCount = 4;

    private I2DVertex baseVertex;
    private double rotationAngle;
    private double width;
    private double height;

    public Rectangle(I2DVertex baseVertex, double rotationAngle, double width, double height)
    {
        ValidateSideLengths(width, height);

        this.baseVertex = baseVertex;
        this.rotationAngle = rotationAngle;
        this.width = width;
        this.height = height;
    }

    public int VertexCount => RectangleVertexCount;

    public double CalculateArea() => width * height;

    private static void ValidateSideLengths(double width, double height)
    {
        if (width < 0)
        {
            throw new ArgumentOutOfRangeException("width", "Rectangle width must be non-negative");
        }   
        if (height < 0)
        {
            throw new ArgumentOutOfRangeException("height", "Rectangle height must be non-negative");
        }
    }
}
