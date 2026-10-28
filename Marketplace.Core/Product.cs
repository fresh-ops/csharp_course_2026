namespace Marketplace.Core;

public class Product
{
    public Guid Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public ProductCategory Categories { get; }

    public Product(
        Guid id,
        string name,
        decimal price,
        ProductCategory categories
    )
    {
        Id = id;
        Name = name;
        Price = price;
        Categories = categories;
    }
}
