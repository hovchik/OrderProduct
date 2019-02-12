using Core.Entity.Cart;
using System;
using System.Linq;
using Xunit;

namespace TestOrderProduct
{
    public class CartAddItem
    {
        private int _testCartId = 2;
        private decimal _testUnitPrice = 1.23m;
        private int _testQuantity = 2;
        //Not have enough time to set real function names
        [Fact]
        public void Test1()
        {
            var cart = new Cart();
            cart.AddItem(_testCartId, _testUnitPrice, _testQuantity);

            var firstItem = cart.Items.Single();
            Assert.Equal(_testCartId, firstItem.CategoryItemId);
            Assert.Equal(_testUnitPrice, firstItem.UnitPrice);
            Assert.Equal(_testQuantity, firstItem.UnitQuantity);
        }

        [Fact]
        public void Test2()
        {
            var cart = new Cart();
            cart.AddItem(_testCartId, _testUnitPrice, _testQuantity);
            cart.AddItem(_testCartId, _testUnitPrice, _testQuantity);

            var firstItem = cart.Items.Single();
            Assert.Equal(_testQuantity * 2, firstItem.UnitQuantity);
        }

        [Fact]
        public void Test3()
        {
            var cart = new Cart();
            cart.AddItem(_testCartId, _testUnitPrice, _testQuantity);
            cart.AddItem(_testCartId, _testUnitPrice * 2, _testQuantity);

            var firstItem = cart.Items.Single();
            Assert.Equal(_testUnitPrice, firstItem.UnitPrice);
        }

        [Fact]
        public void Test4()
        {
            var cart = new Cart();
            cart.AddItem(_testCartId, _testUnitPrice);

            var firstItem = cart.Items.Single();
            Assert.Equal(1, firstItem.UnitQuantity);
        }
    }
}
