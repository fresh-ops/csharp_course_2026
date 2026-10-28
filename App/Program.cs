namespace App;

using App.Lab3;

public static class Program
{
    public static void Main()
    {
        var user1 = new User(
            "petya",
            "veryStrongPassword",
            "Petr",
            "Petrov",
            "520205004556",
            "8 800 555 35 35"
        );
        var user2 = new User(
            "vanya",
            "weakPassword",
            "Ivan",
            "Ivanov",
            "611901997515",
            "+1-505-503-4455"
        );

        Console.WriteLine(user1.GetUserFullName());
        Console.WriteLine(user2.GetUserFullName());
    }
}