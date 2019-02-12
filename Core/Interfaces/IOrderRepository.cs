using Core.Entity.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrderRepository: IAsyncRepos<Order>
    {
        Task<Order> GetByIdWithItemsAsync(int id);
    }
}
