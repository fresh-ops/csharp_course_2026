namespace App.Lab2;

using System.Text;

public class StackMachine
{
    public static string CalculateString(string[] codeLines)
    {
        var builder = new StringBuilder();

        foreach (var line in codeLines)
        {
            if (line.StartsWith("push "))
            {
                builder.Append(line[5..]);
            }
            else
            {
                var charactersToRemove = int.Parse(line[4..]);
                builder.Length -= charactersToRemove;
            }
        }

        return builder.ToString();
    }
}
