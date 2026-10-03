namespace App.Lab1;

public enum PaymentsPlan
{
    Differentiated,
    Annuity
}

public static class Payments
{
    public static decimal CalculateTotalPayments(PaymentsPlan plan, decimal rate, int monthsCount, decimal amount)
    {
        if (rate == 0)
        {
            return amount;
        }

        var monthRate = rate / 12 / 100;
        return plan is PaymentsPlan.Annuity
            ? CalculateAnnuityPayments(monthRate, monthsCount, amount)
            : CalculateDifferentiatedPayments(monthRate, monthsCount, amount);
    }

    private static decimal CalculateDifferentiatedPayments(decimal monthRate, int monthsCount, decimal amount)
    {
        return amount * (1 + monthRate * (monthsCount + 1) / 2);
    }

    private static decimal CalculateAnnuityPayments(decimal monthRate, int monthsCount, decimal amount)
    {
        decimal monthCoefficient = 1;
        for (var i = 0; i < monthsCount; i++)
        {
            monthCoefficient *= 1 + monthRate;
        }
        return amount * monthRate * monthCoefficient / (monthCoefficient - 1) * monthsCount;
    }
}
