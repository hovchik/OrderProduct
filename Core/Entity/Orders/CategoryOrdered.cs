using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity.Orders
{
    public class CategoryOrdered
    {
        public CategoryOrdered(int catId, string prodName)
        {


            CategoryId = catId;
            ProductName = prodName;

        }

        private CategoryOrdered()
        {
            // required by EF
        }

        public int CategoryId { get; private set; }
        public string ProductName { get; private set; }
    }
}
