using E_Commerce.Domain.Model.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Domain.UserRepo
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(x => x.Id).HasConversion(x => x.value, value => UserId.Create(value));

            builder.Property(x => x.Username).IsRequired().HasMaxLength(20);

            builder.Property(x =>x.FirstName).IsRequired().HasMaxLength(10);

            builder.Property(x =>x.SecondName).IsRequired().HasMaxLength(10);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(40);
        }
    }
}
