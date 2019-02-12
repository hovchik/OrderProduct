using Core.Entity.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrderServ
    {
        Task CreateOrderAsync(int CartId, Address shipAddress);
    }
}
