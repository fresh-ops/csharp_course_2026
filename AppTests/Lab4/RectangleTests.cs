using App.Lab4;

namespace AppTests.Lab4;

public class RectangleTests
{
    [Test]
    public void VertexCount_EqualsFour()
    {
        var rectangle = new Rectangle(new Simple2DVertex(0, 0), 0, 20, 50);

        Assert.That(rectangle.VertexCount, Is.EqualTo(4));
    }

    [Test]
    public void Rectangle_RejectsNegativeDimensions()
    {
        var baseVertex = new Simple2DVertex(0, 0);
        var rotationAngle = 0;

        Assert.That(
            () => new Rectangle(baseVertex, rotationAngle, -1, 0),
            Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That(
            () => new Rectangle(baseVertex, rotationAngle, 0, -1),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase(30.0, 20.0, ExpectedResult = 600.0, TestName = "{m} -> width greater than height")]
    [TestCase(20.0, 30.0, ExpectedResult = 600.0, TestName = "{m} -> height greater than width")]
    [TestCase(0.0, 20.0, ExpectedResult = 0.0, TestName = "{m} -> width of 0")]
    [TestCase(30.0, 0.0, ExpectedResult = 0.0, TestName = "{m} -> height of 0")]
    public double CalculateArea_ReturnsCorrectValue(double width, double height)
        => new Rectangle(new Simple2DVertex(0, 0), 0, width, height)
            .CalculateArea();

    [Test]
    public void CalculateArea_DoesNotDependOnBaseVertexAndRotationAngle()
    {
        var width = 30.0;
        var height = 20.0;
        var expected = width * height;

        Rectangle[] rectangles = 
        [
            new Rectangle(new Simple2DVertex(0, 0), 0, width, height),
            new Rectangle(new Simple2DVertex(0, 0), 100, width, height),
            new Rectangle(new Simple2DVertex(0, 0), -20, width, height),
            new Rectangle(new Simple2DVertex(256, 0), 0, width, height),
            new Rectangle(new Simple2DVertex(0, 72), 0, width, height),
            new Rectangle(new Simple2DVertex(112, 911), 101, width, height),
        ];

        foreach (var rectangle in rectangles)
        {
            Assert.That(rectangle.CalculateArea(), Is.EqualTo(expected).Within(1e-8));
        }
    }
}
