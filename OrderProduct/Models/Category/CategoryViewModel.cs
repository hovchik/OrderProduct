using Microsoft.AspNetCore.Mvc.Rendering;
using OrderProduct.ViewModel;
using System.Collections.Generic;

namespace OrderProduct.Models.Category
{
    public class CategoryViewModel
    {
        public IEnumerable<CategoryItemViewModel> CategoryItems { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public int? TypesFilterApplied { get; set; }
        public PaginationVM PaginationInfo { get; set; }
    }
}