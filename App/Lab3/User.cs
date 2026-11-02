namespace App.Lab3;

public class User
{
    private readonly Guid id;
    private readonly string login;
    private readonly DateTime registerDate;

    private string password;
    private string name;
    private string surname;
    private string inn;
    private string phone;

    public User(
        string login,
        string password,
        string name,
        string surname,
        string inn,
        string phone)
    {
        this.login = login;
        this.password = password;
        this.name = name;
        this.surname = surname;
        this.inn = inn;
        TryUpdatePhone(phone);

        id = Guid.NewGuid();
        registerDate = DateTime.Now;
    }

    public Guid GetId() => id;

    public string GetLogin() => login;

    public string GetPassword() => password;

    public void SetPassword(string newPassword)
    {
        password = newPassword;
    }

    public string GetName() => name;

    public void SetName(string newName)
    {
        name = newName;
    }

    public string GetSurname() => surname;

    public void SetSurname(string newSurname)
    {
        this.surname = newSurname;
    }

    public string GetInn() => inn;

    public void SetInn(string newInn)
    {
        this.inn = newInn;
    }

    public string GetPhone() => phone;

    public bool TryUpdatePhone(string newPhone)
    {
        if (!IsPhoneValid(newPhone))
        {
            return false;
        }

        phone = newPhone;
        return true;
    }

    public DateTime GetRegisterDate() => registerDate;

    public string GetUserFullName() => $"{name} {surname}";

    private static bool IsPhoneValid(string phone)
    {
        if (phone == null || phone.Length != 10)
        {
            return false;
        }

        foreach (var character in phone)
        {
            if (!char.IsDigit(character))
            {
                return false;
            }
        }

        return true;
    }
}
