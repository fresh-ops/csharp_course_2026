namespace Marketplace.CoreTests;

using Marketplace.Core;

public class ShoppingCartTest
{
    [Test]
    public void TotalPrice_SumsAllProductPrices()
    {
        var cart = new ShoppingCart();
        var products = new Product[]
        {
            new Product( Guid.NewGuid(), "Item 1", 100m, ProductCategory.None),
            new Product( Guid.NewGuid(), "Item 2", 250m, ProductCategory.None),
            new Product( Guid.NewGuid(), "Item 3", 810m, ProductCategory.None),
            new Product( Guid.NewGuid(), "Item 4", 470m, ProductCategory.None),
        };
    
        var totalPrice = 0m;
        foreach (var product in products)
        {
            cart.AddProduct(product);
            totalPrice += product.Price;
            Assert.That(cart.TotalPrice, Is.EqualTo(totalPrice));
        }
    }

    [Test]
    public void TotalPrice_EqualsZero_WhenCartIsEmpty()
    {
        var cart = new ShoppingCart();
        Assert.That(cart.TotalPrice, Is.Zero);
    }

    [Test]
    public void Clear_RemovesAllProducts()
    {
        var cart = new ShoppingCart();
        var products = new Product[]
        {
            new Product( Guid.NewGuid(), "Item 1", 100m, ProductCategory.None),
            new Product( Guid.NewGuid(), "Item 2", 250m, ProductCategory.None),
            new Product( Guid.NewGuid(), "Item 3", 810m, ProductCategory.None),
            new Product( Guid.NewGuid(), "Item 4", 470m, ProductCategory.None),
        };
    
        foreach (var product in products)
        {
            cart.AddProduct(product);
        }

        cart.Clear();
        Assert.That(cart.GetAllProducts, Is.Empty);
        Assert.That(cart.TotalPrice, Is.Zero);
    }

    [Test]
    public void GetAllProducts_ReturnsAllContainingProducts()
    {
        var cart = new ShoppingCart();
        var products = new Product[]
        {
            new Product( Guid.NewGuid(), "Item 1", 100m, ProductCategory.None),
            new Product( Guid.NewGuid(), "Item 2", 250m, ProductCategory.None),
            new Product( Guid.NewGuid(), "Item 3", 810m, ProductCategory.None),
            new Product( Guid.NewGuid(), "Item 4", 470m, ProductCategory.None),
        };
    
        foreach (var product in products)
        {
            cart.AddProduct(product);
        }

        Assert.That(cart.GetAllProducts(), Is.SubsetOf(products));
        Assert.That(products, Is.SubsetOf(cart.GetAllProducts()));
    }
}