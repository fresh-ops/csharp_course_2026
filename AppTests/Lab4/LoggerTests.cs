using App.Lab4;

namespace AppTests.Lab4;

public class LoggerTests
{
    [Test]
    public void LogGeometry_ReturnsValidLog()
    {
        Assert.That(
            Logger.LogGeometry(
                new Triangle(
                    new Simple2DVertex(0, 0),
                    new Simple2DVertex(0, 1),
                    new Simple2DVertex(1, 2))),
            Is.EqualTo("треугольник"));
        Assert.That(
            Logger.LogGeometry(
                new Rectangle(
                    new Simple2DVertex(0, 0),
                    0,
                    0,
                    0)),
            Is.EqualTo("прямоугольник"));
        Assert.That(
            Logger.LogGeometry(
                new Square(
                    new Simple2DVertex(0, 0),
                    0,
                    0)),
            Is.EqualTo("квадрат"));
    }
}
