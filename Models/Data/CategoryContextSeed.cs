using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CategoryContextSeed
    {
        public static async Task SeedAsync(CategoryContext catalogContext,int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                if (!catalogContext.Categories.Any())
                {
                    catalogContext.Categories.AddRange(
                        GetPreconfiguredCatalogTypes());

                    await catalogContext.SaveChangesAsync();
                }

                if (!catalogContext.Products.Any())
                {
                    catalogContext.Products.AddRange(
                        GetPreconfiguredItems());

                    await catalogContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                   
                    await SeedAsync(catalogContext, retryForAvailability);
                }
            }
        }

       

        static IEnumerable<Category> GetPreconfiguredCatalogTypes()
        {
            return new List<Category>()
            {
                new Category() { Type = "TV"},
                new Category() { Type = "Console" },
                new Category() { Type = "Notebook" },
                new Category() { Type = "SmartPhone" }
            };
        }

        static IEnumerable<Product> GetPreconfiguredItems()
        {
            return new List<Product>()
            {
                new Product() { CategoryId=2, Description = "1TB 2 joystick", Name = "xbox", Price = 350.99M},
                new Product() { CategoryId=1, Description = "Wide 43 inch", Name = "Samsung", Price= 1990.50M},
                new Product() { CategoryId=4, Description = "Prism White", Name = "Nokia", Price = 360M},
                new Product() { CategoryId=1, Description = "Old , GoldStar", Name = "GoldStar",Price =60M},
                new Product() { CategoryId=3, Description = "Roslyn Red Sheet", Name = "Roslyn Red Sheet", Price = 8.5M},
                new Product() { CategoryId=3, Description = "15 inch 256 SSD", Name = "Acer",Price = 400},
                new Product() { CategoryId=4, Description = "Amoled 4K", Name = "Samsung Note 9", Price = 1200}
            };
        }
    }
}
