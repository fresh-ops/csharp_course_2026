using App.Lab3;

namespace AppTests.Lab3;

public class UserTests
{
    [Test]
    public void GetUserFullName_ReturnsCombinedName(
    )
    {
        var name = "Petr";
        var surname = "Petrov";
        var user = new User(
            "user",
            "password",
            name,
            surname,
            "111111111111",
            "9996669966"
        );

        Assert.That(user.GetUserFullName(), Is.EqualTo($"{name} {surname}"));
    }

    [TestCase("8005553535", ExpectedResult = true, TestName = "{m} -> Accepts number without country code")]
    [TestCase("+111 (999) 666-99-66", ExpectedResult = true, TestName = "{m} -> Accepts international phone number")]
    [TestCase("111 999 666 99 66", ExpectedResult = true, TestName = "{m} -> Accepts local phone number")]
    [TestCase("123 45 67", ExpectedResult = true, TestName = "{m} -> Accepts city number")]
    [TestCase("123abc+-()", ExpectedResult = false, TestName = "{m} -> Rejects non-digit characters")]
    [TestCase("12345678", ExpectedResult = false, TestName = "{m} -> Rejects non-standard length")]
    [TestCase(null, ExpectedResult = false, TestName = "{m} -> Rejects null")]
    public bool TryUpdatePhone_ValidatesPhoneFormat(string? newPhone)
    {
        var user = new User(
            "user",
            "password",
            "name",
            "surname",
            "111111111111",
            "9996669966"
        );

        return user.TryUpdatePhone(newPhone);
    }
}
