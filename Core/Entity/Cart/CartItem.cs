using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity.Cart
{
    public class CartItem : SuperEntity
    {
        public decimal UnitPrice { get; set; }
        public int UnitQuantity { get; set; }
        public int CategoryItemId { get; set; }
    }
}
