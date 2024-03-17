using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Model.Category;
using E_Commerce.Domain.Model.Product;
using E_Commerce.Domain.Model.RootCategory;
using E_Commerce.Domain.Model.User;
using E_Commerce.Infrastructure.Interceptor;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure
{
    public class DbContextClass : IdentityDbContext<IdentityUser<Guid>,IdentityRole<Guid>,Guid>
    {
        private readonly PublishDomainEventInterceptor _interceptor;
        public DbContextClass(DbContextOptions<DbContextClass> option, PublishDomainEventInterceptor interceptor) : base(option)
        {
            _interceptor = interceptor;
        }

        public DbSet<RootCategory> rootCategories { get; set; }
        public DbSet<Product>  Products {get;set;}
        public DbSet<Category> categories { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_interceptor);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<List<IDomainEvent>>().ApplyConfigurationsFromAssembly(typeof(DbContextClass).Assembly);


            base.OnModelCreating(builder);
        }
    }

    
}
