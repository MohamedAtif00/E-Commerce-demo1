using E_Commerce.Domain.Model.RootCategory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Domain.RootCategoryRepo
{
    public class RootCategoryEntityTypeConfiguration : IEntityTypeConfiguration<RootCategory>
    {
        public void Configure(EntityTypeBuilder<RootCategory> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasConversion(x => x.value, value => RootCategoryId.Create(value));

            builder.Property(c => c.Name).HasMaxLength(30);
        }
    }
}
