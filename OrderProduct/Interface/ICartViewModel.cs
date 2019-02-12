using OrderProduct.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProduct.Interface
{
    public interface ICartViewModel
    {
        Task<CartViewModel> GetOrCreateCartForUser(string userName);
    }
}
