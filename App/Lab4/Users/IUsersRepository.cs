using Marketplace.Core;

namespace App.Lab4.Users;

public interface IUsersRepository
{
    User GetUserById(Guid id);

    User GetUserByLogin(string login);

    void Put(User user);

    void Delete(User user);

    void Update(User user);
}
