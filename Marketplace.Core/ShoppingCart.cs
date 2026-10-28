namespace Marketplace.Core;

public class ShoppingCart
{
    private List<Product> _products = [];
    private decimal _discount = 0m;

    public Product this[int index]
    {
        get => _products[index];
    }

    public decimal TotalPrice
    {
        get
        {
            var total = 0m;

            foreach (var product in _products)
            {
                total += product.Price;
            }

            return total - _discount;
        }
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public void Clear()
    {
        _products.Clear();
        _discount = 0m;
    }

    internal void SetDiscount(decimal amount)
    {
        _discount = amount;
    }
}
