using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderProduct.Interface;

namespace OrderProduct.Controllers.API
{
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _catService;

        public CategoryController(ICategoryService catalogService) => _catService = catalogService;

        [HttpGet]
        public async Task<IActionResult> List( int? typesFilterApplied, int? page)
        {
            var itemsPage = 10;
            var catalogModel = await _catService.GetCategoryItems(page ?? 0, itemsPage, typesFilterApplied);
            return Ok(catalogModel);
        }
    }
}