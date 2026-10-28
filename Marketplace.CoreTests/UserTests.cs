namespace Marketplace.CoreTests;

using Marketplace.Core;

public class UserTests
{
    [Test]
    public void GetUserFullName_ReturnsCombinedName_WhenUserHasBothNameAndSurname()
    {
        var name = "Petr";
        var surname = "Petrov";
        var user = new User()
        {
            Name = name,
            Surname = surname,
        };

        Assert.That(user.GetUserFullName(), Is.EqualTo($"{name} {surname}"));
    }

    [Test]
    public void Phone_AcceptsValidFormat()
    {
        var phone = "8005553535";
        var user = new User();
        user.Phone = phone;

        Assert.That(user.Phone, Is.EqualTo(phone));
    }

    [TestCase("123abc+-()", TestName = "{m} -> Non-digit characters")]
    [TestCase("123456789", TestName = "{m} -> Too short phone")]
    [TestCase("12345678901", TestName = "{m} -> Too long phone")]
    [TestCase(null, TestName = "{m} -> Null")]
    public void TryUpdatePhone_RejectsInvalidFormat(string? newPhone)
    {
        var user = new User();

        try
        {
            user.Phone = newPhone;
            Assert.Fail("Expected to throw System.ArgumentException");
        }
        catch (ArgumentException ex)
        {
            Assert.That(ex.Message, Does.StartWith("Invalid phone format"));
        }
    }
}
