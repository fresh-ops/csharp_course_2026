using App.Lab4;

namespace AppTests.Lab4;

public class TriangleTests
{
    [Test]
    public void VertexCount_EqualsThree()
    {
        var triangle = new Triangle(
            new Simple2DVertex(0, 0),
            new Simple2DVertex(0, 0),
            new Simple2DVertex(0, 0)
        );

        Assert.That(triangle.VertexCount, Is.EqualTo(3));
    }

    /// <summary>
    /// Test cases for triangle area calculation.
    /// </summary>
    /// <remarks>
    ///   <para>Expected area is calculated dynamically.</para>
    ///   <para>Formulas references:</para>
    ///   <list type="bullet">
    ///     <item>
    ///       <description>
    ///         <see href="https://ru.wikipedia.org/wiki/Треугольник#Площадь_треугольника">
    ///           Википедия: Треугольник - Площадь треугольника
    ///         </see>
    ///       </description>
    ///     </item>
    ///     <item>
    ///       <description>
    ///         <see href="https://en.wikipedia.org/wiki/Area_of_a_triangle">
    ///           Wikipedia: Area of a triangle
    ///         </see>
    ///       </description>
    ///     </item>
    ///   </list>
    /// </remarks>
    /// <returns>Test cases with positional arguments as follows: side A, side B, side C, expected area</returns>
    public static IEnumerable<TestCaseData> AreaTestCases()
    {
        return [
            new TestCaseData(
                new Simple2DVertex(0, 0),
                new Simple2DVertex(5, 3),
                new Simple2DVertex(0, 4),
                5.0 * 4.0 / 2)
                .SetName("{m} -> scalene triangle with a base of 4.0 and a height of 5.0"),
            new TestCaseData(
                new Simple2DVertex(0, 0),
                new Simple2DVertex(5, 0),
                new Simple2DVertex(0, 4),
                5.0 * 4.0 / 2)
                .SetName("{m} -> right angle triangle with legs of 5.0 and 4.0"),
            new TestCaseData(
                new Simple2DVertex(0, 0),
                new Simple2DVertex(2.5 * Math.Sqrt(3), 2.5),
                new Simple2DVertex(0, 5),
                5.0 * 5.0 * Math.Sqrt(3) / 4)
                .SetName("{m} -> equilateral triangle with a side length of 5.0"),
            new TestCaseData(
                new Simple2DVertex(0, 0),
                new Simple2DVertex(5, 5),
                new Simple2DVertex(3, 3),
                0.0)
                .SetName("{m} -> degerate triangle"),
            new TestCaseData(
                new Simple2DVertex(0, 0),
                new Simple2DVertex(0, 0),
                new Simple2DVertex(0, 0),
                0.0)
                .SetName("{m} -> degerate triangle with coincident vertices"),
        ];
    }


    [TestCaseSource(nameof(AreaTestCases))]
    public void CalculateArea_DoesNotDependOnVertexOrder(I2DVertex a, I2DVertex b, I2DVertex c, double expected)
    {
        Triangle[] triangles = 
        [
            new Triangle(a, b, c),
            new Triangle(a, c, b),
            new Triangle(b, a, c),
            new Triangle(b, c, a),
            new Triangle(c, a, b),
            new Triangle(c, b, a)
        ];

        foreach (var triangle in triangles)
        {
            Assert.That(triangle.CalculateArea(), Is.EqualTo(expected).Within(1e-8));
        }
    }
}
