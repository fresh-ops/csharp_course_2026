using App.Lab4;

namespace AppTests.Lab4;

public class ClockwiseComparerTests
{
    [TestCase(0, 1, -1, 0, TestName = "{m} -> top before left")]
    [TestCase(-1, 0, 0, -1, TestName = "{m} -> left before down")]
    [TestCase(0, -1, 1, 0, TestName = "{m} -> down before right")]
    [TestCase(0, 1, 0, -1, TestName = "{m} -> top before down")]
    public void Compare_ReturnsNegative_WhenXIsEarlierClockwise(
        double x1,
        double y1,
        double x2,
        double y2)
    {
        var comparer = new ClockwiseComparer(
            new Simple2DVertex(0, 0));

        var x = new Simple2DVertex(x1, y1);
        var y = new Simple2DVertex(x2, y2);
    
        Assert.That(comparer.Compare(x, y), Is.Negative);
    }

    [TestCase(0, 1, 0, 1, TestName = "{m} -> both on 12 o'clock")]
    [TestCase(0, 0, 0, 0, TestName = "{m} -> same vertex")]
    public void Compare_ReturnsZero_WhenVerticesOnOneLine(
        double x1,
        double y1,
        double x2,
        double y2)
    {
        var comparer = new ClockwiseComparer(
            new Simple2DVertex(0, 0));

        var x = new Simple2DVertex(x1, y1);
        var y = new Simple2DVertex(x2, y2);
    
        Assert.That(comparer.Compare(x, y), Is.Zero);
    }

    [Test]
    public void Compare_ChangesSign_WhenSwapsArguments()
    {
        var comparer = new ClockwiseComparer(
            new Simple2DVertex(0, 0));

        var x = new Simple2DVertex(0, 1);
        var y = new Simple2DVertex(2, 2);
    
        Assert.That(comparer.Compare(x, y), Is.Negative);
        Assert.That(comparer.Compare(y, x), Is.Positive);
    }
}