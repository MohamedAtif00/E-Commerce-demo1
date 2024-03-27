using E_Commerce.Domain.Model.Seller;
using E_Commerce.Domain.Model.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Domain.SellerRepo
{
    public class SellerEntityTypeConfiguration : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Id).HasConversion(x => x.value,value => SellerId.Create(value));

            builder.Property(x => x.UserId).HasConversion(x => x.value , value => UserId.Create(value));
        }
    }
}
