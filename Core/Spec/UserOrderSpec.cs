using Core.Entity.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Spec
{
    public class UserOrderSpec : BaseSpec<Order>
    {
        public UserOrderSpec(string userId)
           : base(o => o.UserId == userId)
        {
            AddInclude(o => o.OrderItems);
            AddInclude($"{nameof(Order.OrderItems)}.{nameof(OrderItem.Ordered)}");
        }
    }
}
