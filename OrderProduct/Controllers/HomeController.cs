using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderProduct.Interface;
using OrderProduct.Models;
using OrderProduct.Models.Category;
using OrderProduct.ViewModel;

namespace OrderProduct.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _catService;
        public CategoryViewModel CatModel { get; set; } = new CategoryViewModel();
        public HomeController(ICategoryService catService)
        {
            _catService = catService;
        }
        public async Task<IActionResult> Index(int? pageId)
        {
            CatModel = await _catService.GetCategoryItems(4, GlobalConstants.ITEMS_PER_PAGE, 1);

            return View(CatModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
