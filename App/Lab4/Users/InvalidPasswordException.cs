namespace App.Lab4.Users;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException(string login) 
        : base($"Invalid password for {login}")
    {
    }
}