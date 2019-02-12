using Core.Entity.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProduct.ViewModel
{
    public class OrderVM
    {
        public int OrderNumber { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }

        public Address ShippingAddress { get; set; }

        public List<OrderItemVM> OrderItems { get; set; } = new List<OrderItemVM>();
    }
}
