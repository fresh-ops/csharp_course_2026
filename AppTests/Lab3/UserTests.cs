namespace AppTests.Lab3;

using App.Lab3;

public class UserTests
{
    [TestCase("Petr", "Petrov")]
    public void GetUserFullName_ReturnsCombinedName_WhenUserHasBothNameAndSurname(
        string name,
        string surname
    )
    {
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

    [TestCase("8005553535", ExpectedResult = true, TestName = "{m} -> Accepts valid format")]
    [TestCase("123abc+-()", ExpectedResult = false, TestName = "{m} -> Rejects non-digit characters")]
    [TestCase("123456789", ExpectedResult = false, TestName = "{m} -> Rejects too short phone")]
    [TestCase("12345678901", ExpectedResult = false, TestName = "{m} -> Rejects too long phone")]
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
