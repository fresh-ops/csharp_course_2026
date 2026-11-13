using App.Lab4;

namespace AppTests.Lab4;

public class Simple2DVertexTests
{
    [Test]
    public void ToString_FormatsCoordinatesWithThreeNumbersAfterDot()
    {
        var vertex = new Simple2DVertex(0, 1.0 / 3.0);

        Assert.That(vertex.ToString(), Is.EqualTo("(x = 0.000, y = 0.333)"));
    }    
}
