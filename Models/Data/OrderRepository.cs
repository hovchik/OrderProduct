using Core.Entity.Orders;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class OrderRepository:FrameworkRepository<Order>,IOrderRepository
    {
        public OrderRepository(CategoryContext dbContext) : base(dbContext)
        {
        }

        public Task<Order> GetByIdWithItemsAsync(int id)
        {
            return _dbContext.Orders
                .Include(o => o.OrderItems)
                .Include($"{nameof(Order.OrderItems)}.{nameof(OrderItem.Ordered)}")
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
