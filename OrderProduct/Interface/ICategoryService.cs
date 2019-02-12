using Microsoft.AspNetCore.Mvc.Rendering;
using OrderProduct.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProduct.Interface
{
    public interface ICategoryService
    {
        Task<CategoryViewModel> GetCategoryItems(int pageIndex, int itemsPage, int? typeId);
        Task<IEnumerable<SelectListItem>> GetTypes();
    }
}
