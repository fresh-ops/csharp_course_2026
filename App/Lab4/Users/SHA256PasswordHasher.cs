using System.Security.Cryptography;

namespace App.Lab4.Users;

public class SHA256PasswordHasher : IPasswordHasher
{
    public byte[] Hash(string password)
    {
        using var sha256 = SHA256.Create();
        var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
        return sha256.ComputeHash(passwordBytes);
    }

    public bool Verify(string password, byte[] hash)
    {
        var computedHash = Hash(password);
        if (computedHash.Length != hash.Length)
        {
            return false;
        }
        for (var i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != hash[i])
            {
                return false;
            }
        }
        return true;
    }
}
