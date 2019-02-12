using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrderProduct.ViewModel
{
    public class CategoryVM
    {
        public IEnumerable<ProductVM> Products { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public int? TypesFilterApplied { get; set; }
        public PaginationVM PaginationInfo { get; set; }
    }
}
