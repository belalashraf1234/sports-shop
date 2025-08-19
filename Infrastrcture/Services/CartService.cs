﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Entity;
using System.Text.Json;

namespace Infrastrcture.Services
{
   public class CartService(IConnectionMultiplexer redis) : ICartService
    {   private readonly IDatabase _database=redis.GetDatabase();
        public async Task<bool> DeleteCartAsync(string cartId)
        {
           return await _database.KeyDeleteAsync(cartId);
        }

        public async Task<ShoppingCart?> GetCartAsync(string cartId)
        {
             var data = await _database.StringGetAsync(cartId);
           
            return data.IsNullOrEmpty?null:  JsonSerializer.Deserialize<ShoppingCart>(data!);
        }

        public async Task<ShoppingCart?> SetCartAsync(ShoppingCart cart)
        {
           var created =await _database.StringSetAsync(cart.Id, JsonSerializer.Serialize(cart), TimeSpan.FromDays(30));
            if (!created)
            {
                return null;
            }
            return await GetCartAsync(cart.Id);
        }
    }
}
