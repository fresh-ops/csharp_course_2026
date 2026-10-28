namespace App.Lab3;

public class User
{
    private readonly Guid _id;
    private readonly string _login;
    private readonly string _inn;
    private readonly DateTime _registerDate;

    private string _password;
    private string _name;
    private string _surname;
    private string _phone;

    public User(
        string login,
        string password,
        string name,
        string surname,
        string inn,
        string phone
    )
    {
        _login = login;
        _password = password;
        _name = name;
        _surname = surname;
        _inn = inn;
        _phone = phone;

        _id = Guid.NewGuid();
        _registerDate = DateTime.Now;
    }

    public Guid GetId() => _id;
    
    public string GetLogin() => _login;

    public string GetPassword() => _password;

    public void SetPassword(string newPassword)
    {
        _password = newPassword;
    }

    public string GetName() => _name;

    public void SetName(string newName)
    {
        _name = newName;
    }

    public string GetSurname() => _surname;

    public void SetSurname(string newSurname)
    {
        _surname = newSurname;
    }

    public string GetInn() => _inn;

    public string GetPhone() => _phone;

    public DateTime GetRegisterDate() => _registerDate;

    public string GetUserFullName()
    {
        return $"{_name} {_surname}";
    }

    public bool TryUpdatePhone(string phone)
    {
        if (!IsPhoneValid(phone))
        {
            return false;
        }

        _phone = phone;
        return true;
    }

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
