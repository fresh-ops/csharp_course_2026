using System;

namespace App.Lab4.Users;

public interface IPasswordHasher
{
    byte[] Hash(string password);

    bool Verify(string password, byte[] hash);
}
