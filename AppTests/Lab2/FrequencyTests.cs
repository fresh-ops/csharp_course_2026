namespace AppTests.Lab2;

using App.Lab2;

public class FrequencyTests
{
    // Базовые случаи
    [TestCase("She stood up. Then she left.", new[] { "she left", "stood up", "then she", "she stood up", "then she left" })]
    [TestCase("a b c d. b c d. e b c a d.", new[] { "a b", "b c", "c d", "e b", "a b c", "b c d", "e b c", "c a d" })]
    [TestCase("a c a. a a d. a b.", new[] { "a a", "c a", "a c a", "a a d" })]
    // Только биграммы
    [TestCase("a b. a c. a b", new[] { "a b" })]
    // Нет ни биграмм, ни триграмм
    [TestCase("a. b. c.", new string[0])]
    [TestCase("   . .  . ", new string[0])]
    [TestCase("", new string[0])]
    public void FindsMostFreuentNGrams_When_StringPassed(string inputText, string[] dictionaryLines)
    {
        var result = Frequency
            .GetMostFrequentNGrams(inputText)
            .Select(nGram => $"{nGram.Key} {nGram.Value}");

        Assert.That(result, Is.SubsetOf(dictionaryLines));
        Assert.That(dictionaryLines, Is.SubsetOf(result));
    }
}
