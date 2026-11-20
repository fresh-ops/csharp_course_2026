using App.Lab4.Users;

namespace AppTests.Lab4.Users;

public class SHA256PasswordHasherTests
{
    [Test]
    public void Hash_ShouldChangePassword()
    {
        var passwordHasher = new SHA256PasswordHasher();
        var password = "password";
        var hash = passwordHasher.Hash(password);

        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        Assert.That(hash, Is.Not.EqualTo(bytes));
    }

    [Test]
    public void Hash_ShouldReturnSameHash_WhenCalledMultipleTimesWithSamePassword()
    {
        var passwordHasher = new SHA256PasswordHasher();
        var password = "password";
        var hash1 = passwordHasher.Hash(password);
        var hash2 = passwordHasher.Hash(password);

        Assert.That(hash1, Is.EqualTo(hash2));
    }

    [Test]
    public void Verify_ReturnTrue_WhenPasswordIsCorrect()
    {
        var passwordHasher = new SHA256PasswordHasher();
        var password = "password";
        var hash = passwordHasher.Hash(password);

        Assert.That(passwordHasher.Verify(password, hash), Is.True);
    }
}
