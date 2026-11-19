using Marketplace.Core;

namespace App.Lab4.Users;

public class InMemoryUsersRepository : IUsersRepository
{
    private Dictionary<Guid, User> usersById = [];
    private Dictionary<string, User> usersByLogin = [];

    public void Delete(User user)
    {
        if (!usersById.ContainsKey(user.Id))
        {
            throw new UserNotFoundException(user.Id);
        }
        if (!usersByLogin.ContainsKey(user.Login))
        {
            throw new UserNotFoundException(user.Login);
        }

        usersById.Remove(user.Id);
        usersByLogin.Remove(user.Login);
    }

    public User GetUserById(Guid id)
    {
        if (usersById.TryGetValue(id, out var user))
        {
            return user;
        }

        throw new UserNotFoundException(id);
    }

    public User GetUserByLogin(string login)
    {
        if (usersByLogin.TryGetValue(login, out var user))
        {
            return user;
        }

        throw new UserNotFoundException(login);
    }

    public void Put(User user)
    {
        if (usersById.ContainsKey(user.Id))
        {
            throw new UserAlreadyExistsException(user.Id);
        }
        if (usersByLogin.ContainsKey(user.Login))
        {
            throw new UserAlreadyExistsException(user.Login);
        }
    
        usersById[user.Id] = user;
        usersByLogin[user.Login] = user;
    }

    public void Update(User user)
    {
        if (!usersById.ContainsKey(user.Id))
        {
            throw new UserNotFoundException(user.Id);
        }
        if (!usersByLogin.ContainsKey(user.Login))
        {
            throw new UserNotFoundException(user.Login);
        }
    
        usersById[user.Id] = user;
        usersByLogin[user.Login] = user;
    }
}
