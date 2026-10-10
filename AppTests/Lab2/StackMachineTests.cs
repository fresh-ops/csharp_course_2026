namespace AppTests.Lab2;

using App.Lab2;

public class StackMachineTests
{
    [TestCase(new[] { "push Hello,", "push World!" }, "Hello,World!")]
    [TestCase(new[] { "push Ночь,", "push Улица,", "push Фонарь,", "push Аптека" }, "Ночь,Улица,Фонарь,Аптека")]
    public void StringsConcated_When_CommandIsPush(string[] codeLines, string expected)
    {
        var actual = StackMachine.CalculateString(codeLines);
        Assert.AreEqual(actual, expected);
    }

    [TestCase(new[] { "push Hello", "pop 2", "push p" }, "Help")]
    public void CharactersRemoved_When_CommandIsPop(string[] codeLines, string expected)
    {
        var actual = StackMachine.CalculateString(codeLines);
        Assert.AreEqual(actual, expected);
    }

    [TestCase(new object[] { new string[0] })]
    [TestCase(new object[] { new string[] { "push hello", "pop 5" } })]
    public void ResultIsEmpty_When_NoCharactersRemaining(string[] codeLines)
    {
        var actual = StackMachine.CalculateString(codeLines);
        Assert.IsEmpty(actual);
    }
}
