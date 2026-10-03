using App.Lab1;

namespace AppTests.Lab1;

public class LeapYearTests
{
    [TestCase(2100, false)]
    [TestCase(2400, true)]
    [TestCase(2024, true)]
    [TestCase(2025, false)]
    [TestCase(2026, false)]
    [TestCase(2027, false)]
    public void TestPasses_When_Result_Correct(int year, bool expected)
    {
        var actual = LeapYear.IsLeapYear(year);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
