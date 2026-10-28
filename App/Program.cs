namespace App;

using App.Lab3;

public static class Program
{
    public static void Main()
    {
        var user1 = new User()
        {
            Login = "petya",
            Password = "veryStrongPassword",
            Name = "Petr",
            Surname = "Petrov",
            Inn = "520205004556",
            Phone = "8005553535",
        };
        var user2 = new User()
        {
            Login = "vanya",
            Password = "weakPassword",
            Name = "Ivan",
            Surname = "Ivanov",
            Inn = "611901997515",
            Phone = "5055034455"
        };

        user1.Phone = "9996669966";

        Console.WriteLine(user1.GetUserFullName());
        Console.WriteLine(user1.Phone);
        Console.WriteLine(user2.GetUserFullName());
    }
}