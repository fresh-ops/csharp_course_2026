using Marketplace.Core;

namespace App.Lab4.Users;

public class UsersService : IUsersService
{
    private readonly IUsersRepository users;
    private readonly IPasswordHasher passwordHasher;

    public UsersService(IUsersRepository users, IPasswordHasher passwordHasher)
    {
        this.users = users;
        this.passwordHasher = passwordHasher;
    }

    public void ChangePassword(Guid userId, string oldPassword, string newPassword)
    {
        if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
        {
            throw new ArgumentException("Passwords cannot be null or empty.");
        }

        var user = users.GetUserById(userId);
        if (!passwordHasher.Verify(oldPassword, user.PasswordHash))
        {
            throw new InvalidPasswordException(user.Login);
        }

        user.PasswordHash = passwordHasher.Hash(newPassword);
        users.Update(user);
    }

    public Guid CreateUser(CreateUserDto createUserDto)
    {
        if (string.IsNullOrEmpty(createUserDto.Login))
        {
            throw new ArgumentException("Login cannot be null or empty.");
        }
        if (string.IsNullOrEmpty(createUserDto.Password))
        {
            throw new ArgumentException("Password cannot be null or empty.");
        }
        if (createUserDto.Phone == null)
        {
            throw new ArgumentException("Phone cannot be null.");
        }

        var userId = Guid.NewGuid();
        while (true)
        {
            try
            {
                users.GetUserById(userId);
                userId = Guid.NewGuid();
            }
            catch (UserNotFoundException)
            {
                break;
            }
        }
        
        var user = new User()
        {
            Id = userId,
            Login = createUserDto.Login,
            PasswordHash = passwordHasher.Hash(createUserDto.Password),
            Name = createUserDto.Name,
            Surname = createUserDto.Surname,
            Inn = createUserDto.Inn,
            Phone = createUserDto.Phone,
        };

        users.Put(user);
        return user.Id;
    }

    public void DeleteUser(Guid userId)
    {
        users.Delete(users.GetUserById(userId));
    }

    public User GetUser(Guid userId)
    {
        return users.GetUserById(userId);
    }

    public Guid LogIn(string login, string password)
    {
        var user = users.GetUserByLogin(login);
        if (!passwordHasher.Verify(password, user.PasswordHash))
        {
            throw new InvalidPasswordException(login);
        }

        return user.Id;
    }

    public void UpdateUser(Guid userId, UpdateUserDto updateUserDto)
    {
        var user = users.GetUserById(userId);
        user.Name = updateUserDto.Name ?? user.Name;
        user.Surname = updateUserDto.Surname ?? user.Surname;
        user.Inn = updateUserDto.Inn ?? user.Inn;
        user.Phone = updateUserDto.Phone ?? user.Phone;
        users.Update(user);
    }
}
