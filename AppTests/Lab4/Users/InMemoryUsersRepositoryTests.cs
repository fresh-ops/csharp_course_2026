using App.Lab4.Users;
using Marketplace.Core;

namespace AppTests.Lab4.Users;

public class InMemoryUsersRepositoryTests
{
    [Test]
    public void Put_ShouldAddUser_WhenThereIsNoUserWithSameIdOrLogin()
    {
        var repository = new InMemoryUsersRepository();
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Login = "User",
        };

        repository.Put(user);

        var userRetrievedById = repository.GetUserById(user.Id);
        Assert.That(userRetrievedById, Is.EqualTo(user));

        var userRetrievedByLogin = repository.GetUserByLogin(user.Login);
        Assert.That(userRetrievedByLogin, Is.EqualTo(user));
    }

    [Test]
    public void Delete_ShouldRemoveUser_WhenUserExists()
    {
        var repository = new InMemoryUsersRepository();
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Login = "User",
        };

        repository.Put(user);
        repository.Delete(user);

        Assert.That(
            () => repository.GetUserById(user.Id),
             Throws.TypeOf<UserNotFoundException>());
        Assert.That(
            () => repository.GetUserByLogin(user.Login),
            Throws.TypeOf<UserNotFoundException>());
    }

    [Test]
    public void Update_ShouldChangeUser_WhenUserExists()
    {
        var repository = new InMemoryUsersRepository();
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Login = "User",
            Name = "Name",
        };

        repository.Put(user);

        var updatedUser = new User()
        {
            Id = user.Id,
            Login = "User",
            Name = "Updated Name",
        };

        repository.Update(updatedUser);

        var userRetrievedById = repository.GetUserById(user.Id);
        Assert.That(userRetrievedById, Is.EqualTo(updatedUser));

        var userRetrievedByLogin = repository.GetUserByLogin(updatedUser.Login);
        Assert.That(userRetrievedByLogin, Is.EqualTo(updatedUser));
    }
}
