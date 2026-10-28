namespace Marketplace.Core;

public class User
{
    private string _phone;

    public Guid Id { get; init; }
    public string Login { get; init; }
    public string Inn { get; init; }
    public DateTime RegisterDate { get; init; }

    public byte[] PasswordHash { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone 
    { 
        get => _phone;
        set
        {
            if (!TryUpdatePhone(value))
            {
                throw new ArgumentException("Invalid phone format");
            }
        } 
    }

    public User()
    {
        Id = Guid.NewGuid();
        RegisterDate = DateTime.Now;
    }

    public User(
        string login,
        byte[] passwordHash,
        string name,
        string surname,
        string inn,
        string phone
    ) : this()
    {
        Login = login;
        PasswordHash = passwordHash;
        Name = name;
        Surname = surname;
        Inn = inn;
        Phone = phone;
    }


    public string GetUserFullName()
    {
        return $"{Name} {Surname}";
    }

    private bool TryUpdatePhone(string phone)
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
