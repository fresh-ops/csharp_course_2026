namespace App.Lab2;

public class Frequency
{
    public static Dictionary<string, string> GetMostFrequentNGrams(string inputString)
    {
        var nGramTails = GetNGramTails(inputString.ToLower());
        var mostFrequentNGrams = new Dictionary<string, string>();

        foreach (var (head, tails) in nGramTails)
        {
            mostFrequentNGrams.Add(
                head,
                GetMostFrequentString(tails)
            );
        }

        return mostFrequentNGrams;
    }

    private static Dictionary<string, List<string>> GetNGramTails(string inputString)
    {
        var nGramTails = new Dictionary<string, List<string>>();

        var sentences = inputString.Split(".", StringSplitOptions.RemoveEmptyEntries);
        foreach (var sentence in sentences)
        {
            var words = sentence.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i + 1 < words.Length; i++)
            {
                if (!nGramTails.ContainsKey(words[i]))
                {
                    nGramTails.Add(words[i], new List<string>());
                }

                nGramTails[words[i]].Add(words[i + 1]);

                if (i + 2 < words.Length)
                {
                    var trigramHead = $"{words[i]} {words[i + 1]}";
                    if (!nGramTails.ContainsKey(trigramHead))
                    {
                        nGramTails.Add(trigramHead, new List<string>());
                    }

                    nGramTails[trigramHead].Add(words[i + 2]);
                }
            }
        }

        return nGramTails;
    }

    private static string GetMostFrequentString(List<string> strings)
    {
        var occurrenceCount = new Dictionary<string, int>();
        string mostFrequentString = null;
        var maxOccurrenceCount = -1;

        foreach (var str in strings)
        {
            occurrenceCount.TryAdd(str, 0);
            occurrenceCount[str]++;
            if (maxOccurrenceCount < occurrenceCount[str]
                || (maxOccurrenceCount == occurrenceCount[str]
                    && string.CompareOrdinal(str, mostFrequentString) < 0
                )
            )
            {
                mostFrequentString = str;
                maxOccurrenceCount = occurrenceCount[str];
            }
        }

        return mostFrequentString;
    }
}
