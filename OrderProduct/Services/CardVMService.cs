using Core.Entity;
using Core.Entity.Cart;
using Core.Interfaces;
using Core.Spec;
using OrderProduct.Interface;
using OrderProduct.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProduct.Services
{
    public class CardVMService:ICartViewModel
    {
        private readonly IAsyncRepos<Cart> _cartRepository;
        private readonly IRepository<Product> _productRepository;

        public CardVMService(IAsyncRepos<Cart> cartRepository,
            IRepository<Product> productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<CartViewModel> GetOrCreateCartForUser(string userName)
        {
            var cartSpec = new CartSpec(userName);
            var cart = (await _cartRepository.ListAsync(cartSpec)).FirstOrDefault();

            if (cart == null)
            {
                return await CreateCartForUser(userName);
            }
            return CreateViewModelFromCart(cart);
        }

        private CartViewModel CreateViewModelFromCart(Cart cart)
        {
            var viewModel = new CartViewModel();
            viewModel.Id = cart.Id;
            viewModel.UserId = cart.UserId;
            viewModel.Items = cart.Items.Select(i =>
            {
                var itemModel = new CartItemViewModel()
                {
                    Id = i.Id,
                    UnitPrice = i.UnitPrice,
                    UnitQuantity = i.UnitQuantity,
                    CategoryId = i.CategoryItemId

                };
                var item = _productRepository.GetById(i.CategoryItemId);
                itemModel.ProductName = item.Name;
                return itemModel;
            }).ToList();
            return viewModel;
        }

        private async Task<CartViewModel> CreateCartForUser(string userId)
        {
            var cart = new Cart() { UserId = userId };
            await _cartRepository.AddAsync(cart);

            return new CartViewModel()
            {
                UserId = cart.UserId,
                Id = cart.Id,
                Items = new List<CartItemViewModel>()
            };
        }

    }
}
