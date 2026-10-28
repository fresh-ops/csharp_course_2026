using Marketplace.Core;

var cart = new ShoppingCart();
var marketingService = new MarketingService();

cart.AddProduct(
    new Product(
        Guid.NewGuid(), 
        "Electric Oven", 
        2500m, 
        ProductCategory.Electronics | ProductCategory.Food
    )
);

cart.AddProduct(
    new Product(
        Guid.NewGuid(), 
        "Chees Sandwich", 
        200m, 
        ProductCategory.Food
    )
);

cart.AddProduct(
    new Product(
        Guid.NewGuid(), 
        "White t-shirt", 
        800m, 
        ProductCategory.Clothing
    )
);

Console.WriteLine("Shopping cart:");
foreach (var product in cart.GetAllProducts())
{
    Console.WriteLine($"{product.Name}\t{product.Price} rub");
}

Console.WriteLine($"Total:\t{cart.TotalPrice}");
marketingService.ApplySecretDiscount(cart, 300);
Console.WriteLine($"Total with discount:\t{cart.TotalPrice}");
