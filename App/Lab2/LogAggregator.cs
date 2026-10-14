namespace App.Lab2;

using System.Text;
using System.Text.RegularExpressions;

public class LogAggregator
{
    private const string s_LogFormatPattern = """
        ^                                                      # Начало строки
        (?<Timestamp>\[\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}\]) # Временная метка [YYYY-MM-dd HH:mm:ss]
        \s
        (?<TraceId>\[[^\]]+\])                                 # Идентификатор трассы
        \s
        (?<ServiceName>[^:]+)                                  # Имя сервиса
        :\s
        (?<Message>.+)                                         # Сообщение сервиса
        $                                                      # Конец строки
        """;
    private const int s_TimestampIndex = 0;
    private const int s_TraceIdIndex = 1;
    private const int s_ServiceNameIndex = 2;
    private const int s_MessageIndex = 3;

    public static string MergeLogs(params string[] logLines)
    {
        var parsedLogs = ParseLogs(logLines);
        var groupedLogs = GroupLogsByTraceId(parsedLogs);
        var sortedGroups = new List<KeyValuePair<string, List<string[]>>>();
        foreach (var group in groupedLogs)
        {
            group.Value.Sort(CompareLogs);
            sortedGroups.Add(group);
        }
        sortedGroups.Sort(CompareLogGroups);



        var builder = new StringBuilder();
        foreach (var (tag, logs) in sortedGroups)
        {
            builder.Append("--- TraceId: ").Append(tag).Append(" ---\n");
            foreach (var log in logs)
            {
                builder
                  .Append(log[s_TimestampIndex]).Append(" ")
                  .Append(log[s_ServiceNameIndex]).Append(": ")
                  .Append(log[s_MessageIndex]).Append("\n");
            }
            builder.Append("\n");
        }

        if (builder.Length >= 2)
        {
            builder.Length -= 2;
        }

        return builder.ToString();
    }

    private static List<string[]> ParseLogs(string[] logLines)
    {
        var format = new Regex(s_LogFormatPattern, RegexOptions.IgnorePatternWhitespace);
        var parsedLogs = new List<string[]>();

        foreach (var log in logLines)
        {
            if (log == null) continue;
            var match = format.Match(log);
            if (match.Success)
            {
                parsedLogs.Add(new[] {
                    match.Groups["Timestamp"].ToString(),
                    match.Groups["TraceId"].ToString(),
                    match.Groups["ServiceName"].ToString(),
                    match.Groups["Message"].ToString()
                });
            }
        }

        return parsedLogs;
    }

    private static Dictionary<string, List<string[]>> GroupLogsByTraceId(List<string[]> logs)
    {
        var groupedLogs = new Dictionary<string, List<string[]>>();

        foreach (var log in logs)
        {
            var traceId = log[s_TraceIdIndex];
            if (!groupedLogs.ContainsKey(traceId))
            {
                groupedLogs.Add(traceId, new List<string[]>());
            }

            groupedLogs[traceId].Add(log);
        }

        return groupedLogs;
    }

    private static int CompareLogs(string[] a, string[] b)
    {
        var timeComparison = string.CompareOrdinal(a[s_TimestampIndex], b[s_TimestampIndex]);
        return timeComparison == 0
          ? string.CompareOrdinal(a[s_ServiceNameIndex], b[s_ServiceNameIndex])
          : timeComparison;
    }

    private static int CompareLogGroups(
        KeyValuePair<string, List<string[]>> a,
        KeyValuePair<string, List<string[]>> b
    )
    {
        return string.CompareOrdinal(
            a.Value[0][s_TimestampIndex],
            b.Value[0][s_TimestampIndex]
        );
    }
}
