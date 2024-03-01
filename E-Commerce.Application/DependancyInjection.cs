using E_Commerce.Application.Product.AddProduct;
using E_Commerce.Application.Product.GetAllProducts;
using E_Commerce.Application.Product.GetProductById;
using E_Commerce.Application.RootCategory.AddRootCategory;
using E_Commerce.Application.RootCategory.GetAllRootCategories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DependancyInjection).Assembly));


            #region RootCategory
            services.AddScoped<AddRootCategoryCommandHandler>();
            services.AddTransient<GetAllRootCategoreisQueryHandler>();

            #endregion
            #region Category

            #endregion
            #region Product
            services.AddScoped<AddProductCommandHandler>();
            services.AddTransient<GetAllProductQueryHandler>();
            services.AddTransient<GetProductByIdQueryHandler>();
            #endregion


            return services;
        }
    }
}
