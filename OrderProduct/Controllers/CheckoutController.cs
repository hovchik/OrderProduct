using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity.Orders;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OrderProduct.Interface;
using OrderProduct.Models.Cart;

namespace OrderProduct.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICart _cartService;
        private readonly IOrderServ _orderService;
        private readonly ICartViewModel _cartViewModelService;

        public CheckoutController(ICart cartService,
            ICartViewModel cartViewModelService,
            IOrderServ orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
            _cartViewModelService = cartViewModelService;
        }

        public CartViewModel CartModel { get; set; } = new CartViewModel();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(Dictionary<string, int> items)
        {
            await SetBasketModelAsync();

            await _cartService.SetQuantity(CartModel.Id, items);

            await _orderService.CreateOrderAsync(CartModel.Id, new Address("Heraci 18A 35apt", "Yerevan", "Center", "Armenia", "12120"));

            await _cartService.DeleteFromCartAsync(CartModel.Id);

            return View("Index", CartModel);
        }

        private async Task SetBasketModelAsync()
        {
            CartModel = await _cartViewModelService.GetOrCreateCartForUser(GlobalConstants.DEFAULT_USERNAME);
        }
    }
}