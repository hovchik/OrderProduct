using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.ServiceImplementations;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderProduct.Interface;
using OrderProduct.Services;
using OrderProduct.ViewModel;

namespace OrderProduct
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCookieSettings(services);
            ConfigureProductionServices(services);
            services.AddScoped(typeof(IRepository<>), typeof(FrameworkRepository<>));
            services.AddScoped(typeof(IAsyncRepos<>), typeof(FrameworkRepository<>));

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICart, CartService>();
            services.AddScoped<ICartViewModel, CardVMService>();
            services.AddScoped<IOrderServ, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddScoped<CategoryService>();
            services.AddHttpContextAccessor();

            services.AddMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private void ConfigureCookieSettings(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }
        
        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<CategoryContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
