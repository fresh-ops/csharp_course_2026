namespace Marketplace.CoreTests;

using Marketplace.Core;

public class MarketplaceServiceTests
{
    [Test]
    public void ApplySecretDiscount_AddsDiscountToCartPrice()
    {
        var service = new MarketingService();
        var cart = new ShoppingCart();
        cart.AddProduct(
            new Product(Guid.NewGuid(), "Item", 100m, ProductCategory.None)
        );

        var discount = 80m;
        var withoutDiscount = cart.TotalPrice;
        service.ApplySecretDiscount(cart, discount);
        var withDiscount = cart.TotalPrice;

        Assert.That(withDiscount, Is.Not.EqualTo(withoutDiscount));
        Assert.That(withoutDiscount - discount, Is.EqualTo(withDiscount));
    }
}