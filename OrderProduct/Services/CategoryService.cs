using Core.Entity;
using Core.Interfaces;
using Core.Spec;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderProduct.Interface;
using OrderProduct.Models.Category;
using OrderProduct.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProduct.Services
{
    public class CategoryService:ICategoryService
    {

        private readonly IRepository<Product> _prodRepository;
        private readonly IAsyncRepos<Category> _catRepository;

        public CategoryService( IRepository<Product> prodRepository,IAsyncRepos<Category> catRepository)
        {
            _prodRepository = prodRepository;
            _catRepository = catRepository;
        }

        public async Task<CategoryViewModel> GetCategoryItems(int pageIndex, int itemsPage, int? typeId)
        {
            var filterSpecification = new CategorySpec(typeId);
            var filterPaginatedSpecification =
                new CategoryFilterSpec(itemsPage * pageIndex, itemsPage, typeId);

            var itemsOnPage = _prodRepository.ListAll();//.List(filterPaginatedSpecification).ToList();
            var totalItems = _prodRepository.Count(filterSpecification);

            var vm = new CategoryViewModel()
            {
                CategoryItems = itemsOnPage.Select(i => new CategoryItemViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Price = i.Price
                }),
                Types = await GetTypes(),
                TypesFilterApplied = typeId ?? 0,
                PaginationInfo = new PaginationVM()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = itemsOnPage.ToList().Count(),
                    TotalItems = totalItems,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return vm;
        }

      

        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            var types = await _catRepository.ListAllAsync();
            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            foreach (Category type in types)
            {
                items.Add(new SelectListItem() { Value = type.Id.ToString(), Text = type.Type });
            }

            return items;
        }
    }
}
