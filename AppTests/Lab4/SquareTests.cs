using App.Lab4;

namespace AppTests.Lab4;

public class SquareTests
{
    [Test]
    public void Square_IsRectangle()
    {
        var square = new Square(new Simple2DVertex(0, 0), 0, 50);

        Assert.That(square, Is.InstanceOf<Rectangle>());
    }

    [Test]
    public void VertexCount_EqualsFour()
    {
        var square = new Square(new Simple2DVertex(0, 0), 0, 50);

        Assert.That(square.VertexCount, Is.EqualTo(4));
    }

    [Test]
    public void Square_RejectsNegativeDimensions()
    {
        Assert.That(
            () => new Square(new Simple2DVertex(0, 0), 0, -1),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase(30.0, ExpectedResult = 900.0, TestName = "{m} -> arbitrary square")]
    [TestCase(0.0, ExpectedResult = 0.0, TestName = "{m} -> side length of 0")]
    public double CalculateArea_ReturnsCorrectValue(double sideLength)
        => new Square(new Simple2DVertex(0, 0), 0, sideLength)
            .CalculateArea();

    [Test]
    public void CalculateArea_DoesNotDependOnBaseVertexAndRotationAngle()
    {
        var sideLength = 20.0;
        var expected = sideLength * sideLength;

        Square[] squares = 
        [
            new Square(new Simple2DVertex(0, 0), 100, sideLength),
            new Square(new Simple2DVertex(0, 0), -20, sideLength),
            new Square(new Simple2DVertex(256, 0), 0, sideLength),
            new Square(new Simple2DVertex(0, 72), 0, sideLength),
            new Square(new Simple2DVertex(112, 911), 101, sideLength),
        ];

        foreach (var square in squares)
        {
            Assert.That(square.CalculateArea(), Is.EqualTo(expected).Within(1e-8));
        }
    }
}
