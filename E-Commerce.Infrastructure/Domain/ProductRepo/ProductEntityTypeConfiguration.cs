using E_Commerce.Domain.Model.Category;
using E_Commerce.Domain.Model.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Domain.ProductRepo
{
    internal class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(x =>x.value ,value =>ProductId.Create(value));
            builder.Property(x => x.CategoryId).HasConversion(x =>x.value,value => CategoryId.Create(value));

            builder.Property(x =>x.ProductName).HasMaxLength(128);
            builder.Property(x =>x.Description).HasMaxLength(200);


            builder.OwnsOne(x => x.Discount);

            builder.OwnsOne(x => x.Price).Property(x => x.Value).HasColumnType("decimal(18,2)");

        }
    }
}
