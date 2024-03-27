using E_Commerce.Application.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using E_Commerce.Application.Common;

namespace E_Commerce.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblies(typeof(DependancyInjection).Assembly);

                });

            


            //#region RootCategory
            //services.AddScoped<AddRootCategoryCommandHandler>();
            //services.AddScoped<ChangeRootCategoryNameCommandHandler>();
            //services.AddScoped<RemoveRootCategoryCommandHandler>();
            //services.AddTransient<GetAllRootCategoreisQueryHandler>();
            //services.AddTransient<GetSingleRootCategoryQueryHandler>();

            //#endregion
            //#region Category
            //services.AddScoped<AddCategoryForRootCommandHandler>();
            //services.AddScoped<AddCategoryForChildCommandHandler>();
            //services.AddScoped<UpdateCategoryCommandHandler>();
            //services.AddScoped<RemoveCategoryCommandHandler>();
            //services.AddTransient<GetAllCategoriesQueryHandler>();
            //services.AddTransient<GetSingleCategoryQueryHandler>();
            //#endregion
            //#region Product
            //services.AddScoped<AddProductCommandHandler>();
            //services.AddScoped<RemoveProductCommandHandler>();
            //services.AddScoped<UpdateProductCommandHandler>();
            //services.AddTransient<GetAllProductQueryHandler>();
            //services.AddTransient<GetProductByIdQueryHandler>();
            //#endregion


            return services;
        }
    }
}
