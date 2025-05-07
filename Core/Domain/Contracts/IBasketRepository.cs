using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Basket;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> getBasketAsync(string key);
        Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket customerBasket, TimeSpan? timeSpan = null);
        Task<bool> DeleteBasketAsync(string key);


    }
}
