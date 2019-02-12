using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Spec;
using Microsoft.AspNetCore.Mvc;
using OrderProduct.ViewModel;

namespace OrderProduct.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> MyOrders()
        {
            var orders = await _orderRepository.ListAsync(new UserOrderSpec(GlobalConstants.DEFAULT_USERNAME));

            var viewModel = orders
                .Select(o => new OrderVM()
                {
                    OrderDate = o.OrderDate,
                    OrderItems = o.OrderItems?.Select(oi => new OrderItemVM()
                    {
                        Discount = 0,
                        ProductId = oi.Ordered.CategoryId,
                        ProductName = oi.Ordered.ProductName,
                        UnitPrice = oi.UnitPrice,
                        Units = oi.Unit
                    }).ToList(),
                    OrderNumber = o.Id,
                    ShippingAddress = o.ShippingAddress,
                    Status = "Pending",
                    Total = o.Total()

                });
            return View(viewModel);
        }

        public async Task<IActionResult> Detail(int orderId)
        {
            var customerOrders = await _orderRepository.ListAsync(new UserOrderSpec(GlobalConstants.DEFAULT_USERNAME));
            var order = customerOrders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return BadRequest("No such order found for this user.");
            }
            var viewModel = new OrderVM()
            {
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemVM()
                {
                    Discount = 0,
                    ProductId = oi.Ordered.CategoryId,
                    ProductName = oi.Ordered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Unit
                }).ToList(),
                OrderNumber = order.Id,
                ShippingAddress = order.ShippingAddress,
                Status = "Pending",
                Total = order.Total()
            };
            return View(viewModel);
        }
    }
}