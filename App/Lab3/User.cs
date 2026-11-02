using System.Text;

namespace App.Lab3;

public class User
{
    private const int CityPhoneNumberLength = 7;
    private const int FullPhoneNumberLength = 10;
    private const int CountryCodeMinLength = 1;

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
        var sanitizedPhone = RemoveDelimiters(newPhone);

        if (!IsPhoneValid(sanitizedPhone))
        {
            return false;
        }

        phone = sanitizedPhone;
        return true;
    }

    public DateTime GetRegisterDate() => registerDate;

    public string GetUserFullName() => $"{name} {surname}";

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
}
