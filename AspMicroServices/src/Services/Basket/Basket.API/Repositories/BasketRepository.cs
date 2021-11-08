using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCaches;
        public BasketRepository(IDistributedCache redisCashe)
        {
            this._redisCaches = redisCashe;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCaches.GetStringAsync(userName);
            if (string.IsNullOrWhiteSpace(basket))            
                return new ShoppingCart(userName);

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);            
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCaches
                  .SetStringAsync(basket.Username, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.Username);
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCaches.RemoveAsync(userName);
        }

    }
}
