using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity.Orders
{
    public class Order:SuperEntity
    {
        public Order()
        {

        }
        public Order(string userId, Address shippingAddress, List<OrderItem> items)
        {

            UserId = userId;
            ShippingAddress = shippingAddress;
            _orderItems = items;
        }
        public string UserId { get; private set; }
        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
        public Address ShippingAddress { get; private set; }
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        
        public decimal Total()
        {
            var total = 0m;
            foreach (var item in _orderItems)
            {
                total += item.UnitPrice * item.Unit;
            }
            return total;
        }
    }
}
