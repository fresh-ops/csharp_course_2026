using App.Lab1;

namespace AppTests.Lab1;

public class PaymentsTests
{
    [TestCase(PaymentsPlan.Annuity, 7, 3, 10_000, 10_116.90)]
    [TestCase(PaymentsPlan.Differentiated, 3, 5, 200_000, 201_500)]
    [TestCase(PaymentsPlan.Annuity, 18, 10, 180_000, 195_181.53)]
    [TestCase(PaymentsPlan.Differentiated, 22, 12, 300_000, 335_750)]
    [TestCase(PaymentsPlan.Annuity, 0, 70 * 12, 500, 500)]
    [TestCase(PaymentsPlan.Differentiated, 0, 70 * 12, 500, 500)]
    public void TestPasses_When_Result_Correct(PaymentsPlan plan, decimal rate, int monthsCount, decimal amount, decimal expected)
    {
        var actual = Payments.CalculateTotalPayments(plan, rate, monthsCount, amount);
        Assert.That(actual, Is.EqualTo(expected).Within(0.01m));
    }
}
