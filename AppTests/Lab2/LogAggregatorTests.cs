using App.Lab2;

namespace AppTests.Lab2;

public class LogAggregatorTests
{
    [Test]
    public void MergeLogs_ReturnEmptyString_When_OnlyGarbagePassed()
    {
        var result = LogAggregator.MergeLogs("garbage", "[invalid]", null);
        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void MergeLogs_SortsLogsByTimeInSingleGroup()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:02] [trace-A] AuthService: Second",
            "[2023-10-25 14:05:01] [trace-A] AuthService: First"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:01] AuthService: First",
            "[2023-10-25 14:05:02] AuthService: Second"
        });

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MergeLogs_SortsLogsByServiceInSingleGroup_When_LogTimeIsEqual()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:02] [trace-A] I18nService: Third",
            "[2023-10-25 14:05:01] [trace-A] AuthService: First",
            "[2023-10-25 14:05:02] [trace-A] AuthService: Second"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:01] AuthService: First",
            "[2023-10-25 14:05:02] AuthService: Second",
            "[2023-10-25 14:05:02] I18nService: Third"
        });

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MergeLogs_SortsGroupsByFirstLogTime()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:02] [trace-A] AuthService: Second",
            "[2023-10-25 14:05:04] [trace-A] AuthService: Fourth",
            "[2023-10-25 14:05:03] [trace-B] AuthService: Third",
            "[2023-10-25 14:05:01] [trace-C] AuthService: First"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-C] ---",
            "[2023-10-25 14:05:01] AuthService: First",
            "",
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:02] AuthService: Second",
            "[2023-10-25 14:05:04] AuthService: Fourth",
            "",
            "--- TraceId: [trace-B] ---",
            "[2023-10-25 14:05:03] AuthService: Third",
        });

        Assert.That(result, Is.EqualTo(expected));
    }
}
