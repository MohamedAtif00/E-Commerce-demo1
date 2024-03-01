using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.Category;
using E_Commerce.Domain.Model.Product;
using E_Commerce.Domain.Model.RootCategory;
using E_Commerce.Infrastructure.Domain;
using E_Commerce.Infrastructure.Domain.CategoryRepo;
using E_Commerce.Infrastructure.Domain.ProductRepo;
using E_Commerce.Infrastructure.Domain.RootCategoryRepo;
using E_Commerce.Infrastructure.Interceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfratsructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<DbContextClass>(option => option.UseSqlServer(configuration.GetConnectionString("default")));

            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IRootCategoryRepository,RootCategoryRepository>();
            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<PublishDomainEventInterceptor>();

            return services;
        }
    }
}
