using Marketplace.Core;

namespace Marketplace.CoreTests;

public class UserTests
{
    [Test]
    public void GetUserFullName_ReturnsCombinedName()
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

    [TestCase("8005553535", TestName = "{m} -> Accepts number without country code")]
    [TestCase("+111 (999) 666-99-66", TestName = "{m} -> Accepts international phone number")]
    [TestCase("111 999 666 99 66", TestName = "{m} -> Accepts local phone number")]
    [TestCase("123 45 67", TestName = "{m} -> Accepts city number")]
    public void Phone_AcceptsValidFormat(string? newPhone)
    {
        var user = new User();
        user.Phone = newPhone;

        Assert.That(user.Phone, Is.Not.Null);
    }

    [TestCase("123abc+-()", TestName = "{m} -> Rejects non-digit characters")]
    [TestCase("12345678", TestName = "{m} -> Rejects non-standard length")]
    [TestCase(null, TestName = "{m} -> Rejects null")]
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
