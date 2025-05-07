using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models.Basket;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> getBasketAsync(string key)
        {
            var Basket = await _database.StringGetAsync(key);

            if (string.IsNullOrEmpty(Basket))

                return null;

            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
        }

        public async Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket customerBasket, TimeSpan? timeSpan = null)
        {
            var JsonBasket = JsonSerializer.Serialize(customerBasket);
            var IsCreatedOrUpdated = _database.StringSetAsync(customerBasket.Id, JsonBasket, timeSpan);
            if (await IsCreatedOrUpdated)

                return await getBasketAsync(customerBasket.Id);
            else
                return null;

        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

    }
}
