﻿using StackExchange.Redis;
using System.ComponentModel;
using System.Text.Json;
using Talabat.Core.Entites.Basket;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository.Basket_Repository
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database =redis.GetDatabase();


        }
       
       public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var basket = await _database.StringGetAsync(BasketId);
            return basket.IsNullOrEmpty? null : JsonSerializer.Deserialize<CustomerBasket?>(basket);
        } 

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var createdorupdated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

            if (!createdorupdated) return null;

            return await GetBasketAsync(basket.Id);


         }

        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
          return await _database.KeyDeleteAsync(BasketId);
                }
    }
}
