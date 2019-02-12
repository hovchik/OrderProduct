using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity.Orders
{
    public class OrderItem:SuperEntity
    {
        public CategoryOrdered Ordered { get; set; }
        public decimal  UnitPrice { get; set; }
        public int Unit { get; set; }
        public OrderItem()
        {

        }
        public OrderItem(CategoryOrdered ordered, decimal unitPrice, int unit)
        {
            Ordered = ordered;
            UnitPrice = unitPrice;
            Unit = unit;
        }
    }
}
