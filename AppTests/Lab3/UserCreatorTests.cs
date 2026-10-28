namespace AppTests.Lab3;

using System.Text;
using App.Lab3;

public class UserCreatorTests
{
    [Test]
    public void CreateUser_HashesPassword()
    {
        var password = "strongPassword";
        var user = UserCreator.CreateUser(
            "login",
            password,
            "name",
            "surname",
            "611901997515",
            "9996669966"
        );

        var passwordBytes = Encoding.ASCII.GetBytes(password);
        Assert.That(user.PasswordHash, Is.Not.EqualTo(passwordBytes));
    }
}