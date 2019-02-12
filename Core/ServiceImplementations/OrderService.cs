using Core.Entity;
using Core.Entity.Cart;
using Core.Entity.Orders;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceImplementations
{
    public class OrderService : IOrderServ
    {
        private readonly IAsyncRepos<Order> _orderRepository;
        private readonly IAsyncRepos<Cart> _cartRepository;
        private readonly IAsyncRepos<Product> _productRepository;

        public OrderService(IAsyncRepos<Cart> cartRepository,
            IAsyncRepos<Product> prodRepository,
            IAsyncRepos<Order> orderRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _productRepository = prodRepository;
        }

        public async Task CreateOrderAsync(int cartId, Address shipAddress)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            var items = new List<OrderItem>();
            foreach (var item in cart.Items)
            {
                var catalogItem = await _productRepository.GetByIdAsync(item.CategoryItemId);
                var itemOrdered = new CategoryOrdered(catalogItem.Id, catalogItem.Name);
                var orderItem = new OrderItem(itemOrdered, item.UnitPrice, item.UnitQuantity);
                items.Add(orderItem);
            }
            var order = new Order(cart.UserId, shipAddress, items);

            await _orderRepository.AddAsync(order);
        }
    }
}
