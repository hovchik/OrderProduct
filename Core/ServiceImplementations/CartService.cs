using Core.Entity;
using Core.Entity.Cart;
using Core.Interfaces;
using Core.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceImplementations
{
    public class CartService : ICart
    {
        private readonly IAsyncRepos<Cart> _CartRepository;
        private readonly IAsyncRepos<CartItem> _CartItemRepository;

        private readonly IRepository<Product> _ProductRepository;
        public CartService(IAsyncRepos<Cart> cartRepository,
            IRepository<Product> prodRepository,
            IAsyncRepos<CartItem> cartItemRepository)
        {
            _CartRepository = cartRepository;
            _ProductRepository = prodRepository;
            _CartItemRepository = cartItemRepository;
        }
        public async Task AddToCart(int cartId, int categoryId, decimal price, int quantity)
        {
            var cart = await _CartRepository.GetByIdAsync(cartId);

            cart.AddItem(categoryId, price, quantity);

            await _CartRepository.UpdateAsync(cart);
        }

        public async Task DeleteFromCartAsync(int cartId)
        {
            var cart = await _CartRepository.GetByIdAsync(cartId);

            foreach (var item in cart.Items.ToList())
            {
                await _CartItemRepository.DeleteAsync(item);
            }

            await _CartRepository.DeleteAsync(cart);
        }

        public async Task<int> GetCartProductCountAsync(string userName)
        {
           
            var cartSpec = new CartSpec(userName);
            var cart = (await _CartRepository.ListAsync(cartSpec)).FirstOrDefault();
            if (cart == null)
            {
                return 0;
            }
            int count = cart.Items.Sum(i => i.UnitQuantity);

            return count;
        }

        public async Task SetQuantity(int cartId, Dictionary<string, int> quantities)
        {
            var basket = await _CartRepository.GetByIdAsync(cartId);
            foreach (var item in basket.Items)
            {
                if (quantities.TryGetValue(item.Id.ToString(), out var quantity))
                {
                    item.UnitQuantity = quantity;
                }
            }
            await _CartRepository.UpdateAsync(basket);
        }
    }
}
