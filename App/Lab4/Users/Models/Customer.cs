using Marketplace.Core;

namespace App.Lab4.Users.Models;

public class Customer : User
{
    private List<Order> orders;

    public List<Order> Orders
    {
        get => orders;
        init => orders = value;
    }
    public Cart Cart { get; init; }

    public override User Clone()
    {
        var clone = MemberwiseClone() as Customer;
        if (orders != null)
        {
            clone.orders = [.. Orders];
        }
    
        return clone;
    }
}
