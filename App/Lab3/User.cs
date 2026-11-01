using System.Text;

namespace App.Lab3;

public class User
{
    private const int CityPhoneNumberLength = 7;
    private const int FullPhoneNumberLength = 10;
    private const int CountryCodeMinLength = 1;

    private string phone;

    public Guid Id { get; init; }
    public string Login { get; init; }
    public DateTime RegisterDate { get; init; }

    public byte[] PasswordHash { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Inn { get; set; }
    public string Phone
    {
        get => phone;
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
        string phone) : this()
    {
        Login = login;
        PasswordHash = passwordHash;
        Name = name;
        Surname = surname;
        Inn = inn;
        Phone = phone;
    }

    public string GetUserFullName() => $"{Name} {Surname}";

    private static bool IsPhoneValid(string phone)
    {
        if (phone == null)
        {
            return false;
        }
        
        var hasPlus = false;

        foreach (var character in phone)
        {
            if (character is '+')
            {
                if (hasPlus) // Защита от двух плюсов(по стандарту не более одного)
                {
                    return false;
                }
                hasPlus = true;
            } 
            else if (!char.IsDigit(character))
            {
                return false;
            }
        }

        if (hasPlus)
        {
            // Добавляем 1 для плюса
            return phone[0] is '+'
                && phone.Length >= FullPhoneNumberLength + CountryCodeMinLength + 1;
        }

        return phone.Length is >= FullPhoneNumberLength or CityPhoneNumberLength;
    }

    private static string RemoveDelimiters(string line)
    {
        if (line == null)
        {
            return null;
        }
        var builder = new StringBuilder();

        foreach (var character in line)
        {
            if (IsDelimiter(character))
            {
                continue;
            }

            builder.Append(character);
        }

        return builder.ToString();
    }

    private static bool IsDelimiter(char character) 
        => char.IsWhiteSpace(character)
            || character is '-' or '(' or ')';

    private bool TryUpdatePhone(string newPhone)
    {
        var sanitizedPhone = RemoveDelimiters(newPhone);

        if (!IsPhoneValid(sanitizedPhone))
        {
            return false;
        }

        phone = sanitizedPhone;
        return true;
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
}
