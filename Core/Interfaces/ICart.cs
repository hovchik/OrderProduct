using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICart
    {
        Task<int> GetCartProductCountAsync(string userName);
        Task AddToCart(int basketId, int catalogItemId, decimal price, int quantity);
        Task SetQuantity(int basketId, Dictionary<string, int> quantities);
        Task DeleteFromCartAsync(int basketId);
    }
}
