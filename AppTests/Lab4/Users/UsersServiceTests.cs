using App.Lab4.Users;
using Marketplace.Core;

namespace AppTests.Lab4.Users;

public class UsersServiceTests
{
    [Test]
    public void GetUser_ShouldReturnUser_WhenUserExists()
    {
        var usersRepository = new InMemoryUsersRepository();
        var passwordHasher = new SHA256PasswordHasher();
        var usersService = new UsersService(usersRepository, passwordHasher);

        var user = new User()
        {
            Id = Guid.NewGuid(),
            Login = "testuser",
            Name = "Test",
        };

        usersRepository.Put(user);
        var retrievedUser = usersService.GetUser(user.Id);

        Assert.That(retrievedUser, Is.Not.Null);
        Assert.That(retrievedUser.Id, Is.EqualTo(user.Id));
        Assert.That(retrievedUser.Login, Is.EqualTo(user.Login));
        Assert.That(retrievedUser.Name, Is.EqualTo(user.Name));
    }

    [Test]
    public void CreateUser_ShouldAddUser_WhenDtoIsValidAndLoginIsUnique()
    {
        var usersRepository = new InMemoryUsersRepository();
        var passwordHasher = new SHA256PasswordHasher();
        var usersService = new UsersService(usersRepository, passwordHasher);

        var userDto = new CreateUserDto()
        {
            Login = "testuser",
            Password = "password",
            Name = "Test",
            Surname = "User",
            Inn = "1234567890",
            Phone = "1234567890",
        };

        var userId = usersService.CreateUser(userDto);
        var createdUser = usersService.GetUser(userId);

        Assert.That(createdUser, Is.Not.Null);
        Assert.That(createdUser.Id, Is.EqualTo(userId));
        Assert.That(createdUser.Login, Is.EqualTo(userDto.Login));
        Assert.That(createdUser.Name, Is.EqualTo(userDto.Name));
        Assert.That(createdUser.Surname, Is.EqualTo(userDto.Surname));
        Assert.That(createdUser.Inn, Is.EqualTo(userDto.Inn));
        Assert.That(createdUser.Phone, Is.EqualTo(userDto.Phone));
    }

    [Test]
    public void DeleteUser_ShouldRemoveUser_WhenUserExists()
    {
        var usersRepository = new InMemoryUsersRepository();
        var passwordHasher = new SHA256PasswordHasher();
        var usersService = new UsersService(usersRepository, passwordHasher);

        var userDto = new CreateUserDto()
        {
            Login = "testuser",
            Password = "password",
            Name = "Test",
            Phone = "1234567890",
        };

        var userId = usersService.CreateUser(userDto);
        usersService.DeleteUser(userId);

        Assert.That(
            () => usersRepository.GetUserById(userId),
            Throws.Exception.TypeOf<UserNotFoundException>());
    }

    [Test]
    public void ChangePassword_ShouldUpdatePassword_WhenOldPasswordIsCorrect()
    {
        var usersRepository = new InMemoryUsersRepository();
        var passwordHasher = new SHA256PasswordHasher();
        var usersService = new UsersService(usersRepository, passwordHasher);

        var userDto = new CreateUserDto()
        {
            Login = "testuser",
            Password = "oldpassword",
            Name = "Test",
            Phone = "1234567890",
        };

        var userId = usersService.CreateUser(userDto);
        usersService.ChangePassword(userId, "oldpassword", "newpassword");

        var updatedUser = usersService.GetUser(userId);
        Assert.That(
            passwordHasher.Verify("newpassword", updatedUser.PasswordHash),
            Is.True);
    }

    public void UpdateUser_ShouldUpdateUser_WhenUserExists()
    {
        var usersRepository = new InMemoryUsersRepository();
        var passwordHasher = new SHA256PasswordHasher();
        var usersService = new UsersService(usersRepository, passwordHasher);

        var userDto = new CreateUserDto()
        {
            Login = "testuser",
            Password = "password",
            Name = "Test",
            Surname = "User",
            Inn = "1234567890",
            Phone = "1234567890",
        };

        var userId = usersService.CreateUser(userDto);

        var updateUserDto = new UpdateUserDto()
        {
            Name = "UpdatedName",
            Surname = "UpdatedSurname",
            Inn = "0987654321",
            Phone = "0987654321",
        };

        usersService.UpdateUser(userId, updateUserDto);
        var updatedUser = usersService.GetUser(userId);

        Assert.That(updatedUser.Name, Is.EqualTo(updateUserDto.Name));
        Assert.That(updatedUser.Surname, Is.EqualTo(updateUserDto.Surname));
        Assert.That(updatedUser.Inn, Is.EqualTo(updateUserDto.Inn));
        Assert.That(updatedUser.Phone, Is.EqualTo(updateUserDto.Phone));
    }
}
