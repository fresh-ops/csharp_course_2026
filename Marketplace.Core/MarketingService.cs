namespace Marketplace.Core;

public class MarketingService
{
    public void ApplySecretDiscount(ShoppingCart cart, decimal amount)
    {
        cart.SetDiscount(amount);
    }
}
