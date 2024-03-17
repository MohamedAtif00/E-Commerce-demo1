using E_Commerce.Domain.Model.Category;
using E_Commerce.Domain.Model.RootCategory;
using E_Commerce.Domain.Model.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Domain.CategoryRepo
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasConversion(x => x.value, value => CategoryId.Create(value));
            builder.Property(c => c.ParentCategoryId).HasConversion(x => x.value, value => CategoryId.Create(value));
            builder.Property(c => c.RootCategoryId).HasConversion(x => x.value, value => RootCategoryId.Create(value));
            builder.Property(c => c.Name).HasMaxLength(30);

            builder.OwnsMany<Visitor>("Visitors", visitors =>
            {
                visitors.HasKey(x => x.Id);
                visitors.Property(x => x.Id)
                        .HasConversion(x => x.value, value => UserId.Create(value));
                visitors.WithOwner().HasForeignKey(c => c.categoryId); // Assuming there's a foreign key property named CategoryId in the Visitor entity
                visitors.Property(x => x.categoryId).HasConversion(x => x.value, value => CategoryId.Create(value));
                visitors.ToTable("Visitor"); // Optionally specify the table name for the Visitors collection
                
            });
        }
    }
}
