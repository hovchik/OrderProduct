using Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Spec
{
    public class CategorySpec:BaseSpec<Product>
    {
        public CategorySpec(int? typeId) : base(i =>!typeId.HasValue || i.CategoryId == typeId)
        {
        }
    }
}
