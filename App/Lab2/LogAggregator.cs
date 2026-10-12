namespace App.Lab2;

using System.Text;
using System.Text.RegularExpressions;

public class LogAggregator
{
    private const string LOG_REGEX_PATTERN = @"^(\[\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}\])\s(\[.+\])\s(.+):\s(.+)$";

    public static string MergeLogs(params string[] logLines)
    {
        var format = new Regex(LOG_REGEX_PATTERN);
        var parsedLogs = logLines
            .Where(line => !string.IsNullOrEmpty(line))
            .Select(line => format.Match(line))
            .Where(match => match.Success)
            .Select(match => new Log
            {
                Time = match.Groups[1].ToString(),
                Tag = match.Groups[2].ToString(),
                ServiceName = match.Groups[3].ToString(),
                Message = match.Groups[4].ToString(),
            });

        var logsByTag = parsedLogs
            .GroupBy(log => log.Tag)
            .OrderBy(logGroup => logGroup.Min(log => log.Time))
            .Select(log => (
                log.Key,
                log.OrderBy(log => log.Time)
                    .ThenBy(log => log.ServiceName)
            ));

        var builder = new StringBuilder();
        foreach (var (tag, logs) in logsByTag)
        {
            builder.Append("--- TraceId: ").Append(tag).Append(" ---\n");
            foreach (var log in logs)
            {
                builder
                  .Append(log.Time).Append(" ")
                  .Append(log.ServiceName).Append(": ")
                  .Append(log.Message).Append("\n");
            }
            builder.Append("\n");
        }

        if (builder.Length > 2)
        {
            builder.Remove(builder.Length - 2, 2);
        }

        return builder.ToString();
    }

    private struct Log
    {
        public string Time { get; init; }
        public string Tag { get; init; }
        public string ServiceName { get; init; }
        public string Message { get; init; }
    }
}
