using Marketplace.Core;

namespace App.Lab4.Users.Models;

public class Employee : User
{
    private List<Order> orders;

    public List<Order> Orders
    {
        get => orders;
        init => orders = value;
    }
    public string LegalINN { get; set; }

    public override User Clone()
    {
        var clone = MemberwiseClone() as Employee;
        if (orders != null)
        {
            clone.orders = [.. Orders];
        }
    
        return clone;
    }
}
