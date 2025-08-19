using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastrcture.Data
{
   public class DataSeed
    {

        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.DemoProducts.Any())
            {
                var demoProducts = await File.ReadAllTextAsync("E:\\Projects\\AutoEcommerce\\products.json");
                if (demoProducts != null)
                {
                    var products = JsonSerializer.Deserialize<List<DemoProduct>>(demoProducts);
                   
                        context.DemoProducts.AddRange(products);
                   
                    await context.SaveChangesAsync();
                }
            }
            if (!context.DeliveryMethods.Any())
            {
                var delivery = await File.ReadAllTextAsync("E:\\Projects\\AutoEcommerce\\seed\\delivery.json");
                if (delivery != null)
                {
                    var deliveryData = JsonSerializer.Deserialize<List<DeliveryMethod>>(delivery);
                   
                    context.DeliveryMethods.AddRange(deliveryData);
                   
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
