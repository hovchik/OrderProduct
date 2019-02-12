using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Entity.Cart
{
    public class Cart : SuperEntity
    {
        public string UserId { get; set; }
        private readonly List<CartItem> _items = new List<CartItem>();
        public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

        public void AddItem(int categoryId, decimal unitPrice, int quantity = 1)
        {
            if (!Items.Any(i => i.CategoryItemId == categoryId))
            {
                _items.Add(new CartItem()
                {
                    CategoryItemId = categoryId,
                    UnitQuantity = quantity,
                    UnitPrice = unitPrice
                });
                return;
            }
            var existingItem = Items.FirstOrDefault(i => i.CategoryItemId == categoryId);
            existingItem.UnitQuantity += quantity;
        }
    }
}
