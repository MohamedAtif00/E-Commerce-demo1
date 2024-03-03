using E_Commerce.Application.Category.AddCategoryForChild;
using E_Commerce.Application.Category.AddCategoryForRoot;
using E_Commerce.Application.Category.GetAllCategories;
using E_Commerce.Application.Category.GetSingleCategory;
using E_Commerce.Application.Category.RemoveCategory;
using E_Commerce.Application.Category.UpdateCategory;
using E_Commerce.Application.Product.AddProduct;
using E_Commerce.Application.Product.GetAllProducts;
using E_Commerce.Application.Product.GetProductById;
using E_Commerce.Application.Product.RemoveProduct;
using E_Commerce.Application.Product.UpdateProduct;
using E_Commerce.Application.RootCategory.AddRootCategory;
using E_Commerce.Application.RootCategory.ChangeRootCategoryName;
using E_Commerce.Application.RootCategory.GetAllRootCategories;
using E_Commerce.Application.RootCategory.GetSingleRootCategoryQuery;
using E_Commerce.Application.RootCategory.RemoveRootCategory;
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
            services.AddScoped<ChangeRootCategoryNameCommand>();
            services.AddScoped<RemoveRootCategoryCommandHandler>();
            services.AddTransient<GetAllRootCategoreisQueryHandler>();
            services.AddTransient<GetSingleRootCategoryQueryHandler>();

            #endregion
            #region Category
            services.AddScoped<AddCategoryForRootCommandHandler>();
            services.AddScoped<AddCategoryForChildCommandHandler>();
            services.AddScoped<UpdateCategoryCommandHandler>();
            services.AddScoped<RemoveCategoryCommandHandler>();
            services.AddTransient<GetAllCategoriesQueryHandler>();
            services.AddTransient<GetSingleCategoryQueryHandler>();
            #endregion
            #region Product
            services.AddScoped<AddProductCommandHandler>();
            services.AddScoped<RemoveProductCommandHandler>();
            services.AddScoped<UpdateProductCommandHandler>();
            services.AddTransient<GetAllProductQueryHandler>();
            services.AddTransient<GetProductByIdQueryHandler>();
            #endregion


            return services;
        }
    }
}
