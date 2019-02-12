using OrderProduct.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderProduct.Models.Cart
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
        public string UserId { get; set; }

        public decimal Total()
        {
            return Math.Round(Items.Sum(x => x.UnitPrice * x.UnitQuantity), 2);
        }
    }
}