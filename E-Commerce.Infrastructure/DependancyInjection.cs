using E_Commerce.Application.Common;
using E_Commerce.Domain.Common.Persistent.UnitOfWork;
using E_Commerce.Domain.Model.Category;
using E_Commerce.Domain.Model.Product;
using E_Commerce.Domain.Model.Role;
using E_Commerce.Domain.Model.RootCategory;
using E_Commerce.Domain.Model.Token;
using E_Commerce.Domain.Model.User;
using E_Commerce.Infrastructure.Authentication;
using E_Commerce.Infrastructure.Domain;
using E_Commerce.Infrastructure.Domain.CategoryRepo;
using E_Commerce.Infrastructure.Domain.ProductRepo;
using E_Commerce.Infrastructure.Domain.RefreshTokenRepo;
using E_Commerce.Infrastructure.Domain.RootCategoryRepo;
using E_Commerce.Infrastructure.Domain.UserRepo;
using E_Commerce.Infrastructure.Interceptor;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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

            services.AddTransient<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IRootCategoryRepository,RootCategoryRepository>();
            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IRefreshTokenRepository,RefreshTokenRepository>();

            services.AddScoped<IAuthenticationService,AuthenticationService>();
            services.AddTransient<IEmailSender,EmailSender>();

            services.AddTransient<PublishDomainEventInterceptor>();

            var jwtSettings = configuration.GetSection("JwtSettings");

            var setting = services.Configure<JwtSettings>(jwtSettings);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.ClaimsIssuer = jwtSettings["Issuer"];
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SigningKey"])),
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audiance"]

                };
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            context.Response.Headers.Add("IS_TOKEN_EXPIRED", "Y");
                        return Task.CompletedTask;

                    }
                };

            });

            //using (var context = services.BuildServiceProvider().GetRequiredService<DbContextClass>())
            //{
            //    if (!context.Roles.Any())
            //    {
            //        context.Roles.AddRange(
            //            new IdentityRole<Guid> { Id = Guid.NewGuid(),Name=Role.CreateUser().role.GetRoleName()},
            //            new IdentityRole<Guid> { Id = Guid.NewGuid(),Name=Role.CreateSeller().role.GetRoleName() },
            //            new IdentityRole<Guid> { Id = Guid.NewGuid(),Name="admine" }
            //        );
            //    }
            //}


            return services;
        }
    }
}
