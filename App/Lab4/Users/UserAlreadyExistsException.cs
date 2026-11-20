namespace App.Lab4.Users;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(Guid id)
        : base($"User with id {id} already exists")
    {
        
    }

    public UserAlreadyExistsException(string login)
        : base($"User with login {login} already exists")
    {
        
    }
}
