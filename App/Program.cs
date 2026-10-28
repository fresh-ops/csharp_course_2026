namespace App;

using App.Lab3;

public static class Program
{
    public static void Main()
    {
        var user1 = UserCreator.CreateUser(
            "petya",
            "veryStrongPassword",
            "Petr",
            "Petrov",
            "520205004556",
            "8005553535"
        );
        var user2 = UserCreator.CreateUser(
            "vanya",
            "weakPassword",
            "Ivan",
            "Ivanov",
            "611901997515",
            "5055034455"
        );

        user1.Phone = "9996669966";

        Console.WriteLine(user1.GetUserFullName());
        Console.WriteLine(user1.Phone);
        Console.WriteLine(user2.GetUserFullName());
    }
}