using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OrderProduct.Interface;
using OrderProduct.Models.Cart;
using OrderProduct.Models.Category;

namespace OrderProduct.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart _cartService;
        private readonly ICartViewModel _cartViewModelService;
        public CartViewModel CartModel { get; set; } = new CartViewModel();
        public CartController(ICart cartService, ICartViewModel cartViewModelService)
        {
            _cartService = cartService;
            _cartViewModelService = cartViewModelService;
        }
        public async Task<IActionResult> OnGet()
        {
            await SetCartModelAsync();
            return View("Index",CartModel);
        }
        public async Task<IActionResult> OnPost(CategoryItemViewModel productDetails)
        {
            if (productDetails?.Id == null)
            {
                return View("Index", CartModel);
            }
            await SetCartModelAsync();

            await _cartService.AddToCart(CartModel.Id, productDetails.Id, productDetails.Price, 1);

            await SetCartModelAsync();

            return View("Index", CartModel);
        }
        public async Task<IActionResult> OnPostUpdate(Dictionary<string, int> items)
        {
            await SetCartModelAsync();
            await _cartService.SetQuantity(CartModel.Id, items);

            await SetCartModelAsync();
            return View("Index", CartModel);
        }
        private async Task SetCartModelAsync() => CartModel = await _cartViewModelService.GetOrCreateCartForUser(GlobalConstants.DEFAULT_USERNAME);

        public IActionResult Index()
        {
            return View();
        }
    }
}