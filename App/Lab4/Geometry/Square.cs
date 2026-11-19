namespace App.Lab4.Geometry;

public class Square : Rectangle
{
    public Square(I2DVertex baseVertex, double rotationAngle, double sideLength) 
        : base(baseVertex, rotationAngle, sideLength, sideLength)
    {

    }
}
