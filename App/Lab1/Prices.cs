namespace App.Lab1;

public static class Prices
{
    public static string GetCurrencyAlias(int price, bool isShortNotation, bool isFirstCapital)
    {
        var currency = isShortNotation ? "руб." : GetCurrencyDeclension(price);
        return isFirstCapital
            ? char.ToUpper(currency[0]) + currency[1..]
            : currency;
    }

    private static string GetCurrencyDeclension(int amount)
    {
        return (amount % 100, amount % 10) switch
        {
            ( >= 10 and <= 20, _) => "рублей",
            (_, 1) => "рубль",
            (_, >= 2 and <= 4) => "рубля",
            _ => "рублей",
        };
    }
}
