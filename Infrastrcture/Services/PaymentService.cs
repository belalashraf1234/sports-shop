using Core.Entity;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastrcture.Services;

public class PaymentService(
    IConfiguration config,
    ICartService cartService,
    IGenericRepository<DeliveryMethod> dmRepo,
    IGenericRepository<DemoProduct> productRepo):IPaymentService
{
    public async Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId)
    {
       StripeConfiguration.ApiKey = config["StripeSettings:SecretKey"];
        var cart = await cartService.GetCartAsync(cartId);
        if(cart ==null) return null;
        var shippingPrice = 0m;
        if (cart.DeliveryMethodId.HasValue)
        {
            var devliveryMethod = await dmRepo.GetByIdAsync((int)cart.DeliveryMethodId);
            
            if (devliveryMethod == null) return null;
            shippingPrice = devliveryMethod.Price;
            
            
        }

        foreach (var item in cart.Items)
        {
            var productItem = await productRepo.GetByIdAsync(item.ProductId);
            if (productItem == null) return null;
            if (item.price != productItem.Price)
            {
                item.price = productItem.Price;
                
            }
        }

        var service = new PaymentIntentService();

        PaymentIntent? intent = null;
        if (string.IsNullOrEmpty(cart.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)cart.Items.Sum(x => x.Quantity * (x.price * 100)) + (long)shippingPrice * 100,
                Currency = "usd",
                PaymentMethodTypes = ["card"]
                
            };
            intent = await service.CreateAsync(options);
            cart.PaymentIntentId = intent.Id;
            cart.ClientSecret = intent.ClientSecret;

        }
        else
        {
            
            var options = new PaymentIntentUpdateOptions()
            {
                Amount = (long)cart.Items.Sum(x => x.Quantity * (x.price * 100)) + (long)shippingPrice * 100
                
            };
            intent = await service.UpdateAsync(cart.PaymentIntentId,options);
        }

        return cart;

    }
}