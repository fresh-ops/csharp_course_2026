namespace Marketplace.Core;

using System.Security.Cryptography;
using System.Text;

public static class UserCreator
{
    public static User CreateUser(
        string login,
        string password,
        string name,
        string surname,
        string inn,
        string phone
    )
    {
        using (var md5 = MD5.Create())
        {
            var passwordBytes = Encoding.ASCII.GetBytes(password);

            return new User(
                login, 
                md5.ComputeHash(passwordBytes), 
                name, 
                surname, 
                inn, 
                phone
            );
        }
    }
}