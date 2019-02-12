using Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Spec
{
    public class CategoryFilterSpec : BaseSpec<Product>
    {
        public CategoryFilterSpec(int skip, int take, int? typeId)
            : base(i => !typeId.HasValue || i.CategoryId == typeId)
        {
            ApplyPaging(skip, take);
        }
    }
}
