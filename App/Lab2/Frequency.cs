namespace App.Lab2;

public class Frequency
{
    public static Dictionary<string, string> GetMostFrequentNGrams(string inputString)
    {
        var tailedNGrams = GetTailedNGrams(inputString.ToLower());
        var comparer = Comparer<CountedString>.Create(
            (a, b) => a.Count == b.Count
                ? -string.CompareOrdinal(a.Value, b.Value)
                : a.Count - b.Count
        );

        return tailedNGrams
            .GroupBy(nGram => nGram.Head)
            .ToDictionary(
                nGram => nGram.Key,
                nGram => nGram
                  .GroupBy(tail => tail.Tail)
                  .MaxBy(
                      tail => new CountedString { Value = tail.Key, Count = tail.Count() },
                      comparer
                  )
                  .Key
            );
    }

    private static List<TailedString> GetTailedNGrams(string inputString)
    {
        var tailedNGrams = new List<TailedString>();
        var sentences = inputString.Split('.', StringSplitOptions.RemoveEmptyEntries);

        foreach (var sentence in sentences)
        {
            var words = sentence.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i + 1 < words.Length; i++)
            {
                tailedNGrams.Add(new TailedString
                {
                    Head = words[i],
                    Tail = words[i + 1]
                });

                if (i + 2 < words.Length)
                {
                    tailedNGrams.Add(new TailedString
                    {
                        Head = $"{words[i]} {words[i + 1]}",
                        Tail = words[i + 2]
                    });
                }
            }
        }

        return tailedNGrams;
    }

    private struct TailedString
    {
        public string Head { get; init; }
        public string Tail { get; init; }
    }

    private struct CountedString
    {
        public string Value { get; init; }
        public int Count { get; init; }
    }
}
