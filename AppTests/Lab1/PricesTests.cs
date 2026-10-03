using App.Lab1;

namespace AppTests.Lab1;

public class PricesTests
{
    // Базовые склонения
    [TestCase(0, "рублей")]
    [TestCase(1, "рубль")]
    [TestCase(3, "рубля")]
    [TestCase(7, "рублей")]

    // Исключения
    [TestCase(15, "рублей")]
    [TestCase(11, "рублей")]
    [TestCase(12, "рублей")]
    [TestCase(10, "рублей")]

    // Большие числа
    [TestCase(12419, "рублей")]
    [TestCase(144, "рубля")]
    [TestCase(25478, "рублей")]
    [TestCase(4321, "рубль")]
    public void CurrencyHasCorrectDeclension_When_IsShortNotation_False(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, false, false);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(101, "руб.")]
    [TestCase(0, "руб.")]
    public void CurrencyIsShorten_When_IsShortNotation_True(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, true, false);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(11, false, "Рублей")]
    [TestCase(478, false, "Рублей")]
    [TestCase(22, true, "Руб.")]
    [TestCase(46, true, "Руб.")]
    public void CurrencyIsCapitalized_When_IsFirstCapital_True(int price, bool isShortNotation, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, isShortNotation, true);
        Assert.That(actual, Is.EqualTo(expected));
    }
}
