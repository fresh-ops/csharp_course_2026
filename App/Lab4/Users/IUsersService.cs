using Marketplace.Core;

namespace App.Lab4.Users;

public interface IUsersService
{
	User GetUser(Guid userId);
	Guid CreateUser(CreateUserDto createUserDto);
	void DeleteUser(Guid userId);
	void ChangePassword(Guid userId, string oldPassword, string newPassword);
	void UpdateUser(Guid userId, UpdateUserDto updateUserDto);
	
	Guid LogIn(string login, string password);
}