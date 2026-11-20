using Marketplace.Core;

namespace App.Lab4.Users.Models;

public class Seller : User
{
    private List<Product> products;
    private List<Order> orders;

    public List<Product> Products
    {
        get => products;
        init => products = value;
    }
    public List<Order> Orders
    {
        get => orders;
        init => orders = value;
    }
    public string LegalINN { get; set; }

    public override User Clone()
    {
        var clone = MemberwiseClone() as Seller;
        if (products != null)
        {
            clone.products = [.. Products];
        }
        if (orders != null)
        {
            clone.orders = [.. Orders];
        }
    
        return clone;
    }
}
