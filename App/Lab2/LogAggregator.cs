namespace App.Lab2;

using System.Globalization;
using System.Text;

public class LogAggregator
{
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
                  .Append("[").Append(log[s_TimestampIndex]).Append("] ")
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
        var parsedLogs = new List<string[]>();

        foreach (var log in logLines)
        {
            if (TryParseLogLine(log, out var parsed))
            {
                parsedLogs.Add(parsed);
            }
        }

        return parsedLogs;
    }

    public static bool TryParseLogLine(string line, out string[] parsedLog)
    {
        parsedLog = new string[4];
        if (line == null)
        {
            return false;
        }

        if (line.Length < 21 || line[0] != '[' || line[20] != ']')
        {
            return false;
        }

        parsedLog[s_TimestampIndex] = line.Substring(1, 19);
        if (!DateTime.TryParseExact(
              parsedLog[s_TimestampIndex],
              "yyyy-MM-dd HH:mm:ss",
              CultureInfo.InvariantCulture,
              DateTimeStyles.None,
              out var parsedDateTime
        ))
        {
            return false;
        }

        if (line.Length < 23)
        {
            return false;
        }
        var traceIdEndIndex = line.IndexOf(']', 21);
        if (traceIdEndIndex == -1 || line[21] != ' ' || line[22] != '[')
        {
            return false;
        }
        parsedLog[s_TraceIdIndex] = line.Substring(22, traceIdEndIndex - 22 + 1);

        var serviceNameEndIndex = line.IndexOf(':', traceIdEndIndex + 1);
        if (serviceNameEndIndex == -1 || line[traceIdEndIndex + 1] != ' ')
        {
            return false;
        }

        parsedLog[s_ServiceNameIndex] = line.Substring(
            traceIdEndIndex + 2,
            serviceNameEndIndex - traceIdEndIndex - 2
        );
        if (line.Length < serviceNameEndIndex + 2 || line[serviceNameEndIndex + 1] != ' ')
        {
            return false;
        }
        parsedLog[s_MessageIndex] = line.Substring(serviceNameEndIndex + 2);

        return true;
    }

    private static bool TrySkipCharacter(ReadOnlySpan<char> span, char skipping, out ReadOnlySpan<char> newSpan)
    {
        newSpan = span;
        if (span.Length == 0 || span[0] != skipping)
        {
            return false;
        }

        newSpan = span.Slice(1);
        return true;
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
