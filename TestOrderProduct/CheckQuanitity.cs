using Core.Entity.Cart;
using Core.Interfaces;
using Core.ServiceImplementations;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestOrderProduct
{
    public class CheckQuanitity
    {
        private int _invalidId = -1;
        private Mock<IAsyncRepos<Cart>> _mockCart;

        public CheckQuanitity()
        {
            _mockCart = new Mock<IAsyncRepos<Cart>>();
        }

        [Fact]
        public async void ThrowsGivenInvalidBasketId()
        {
            var cart_Service = new CartService(_mockCart.Object, null, null);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
                await cart_Service.SetQuantity(_invalidId, new System.Collections.Generic.Dictionary<string, int>()));
        }

        [Fact]
        public async void ThrowsGivenNullQuantities()
        {
            var cart_Service = new CartService(null, null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await cart_Service.SetQuantity(123, null));
        }
    }
}
