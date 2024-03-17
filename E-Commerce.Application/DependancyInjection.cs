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
                            context.Response.Headers.Add("IS_TOKEN_EXPIRED","Y");
                        return Task.CompletedTask;
                        
                    }
                };
                
            });

            services.AddScoped<Common.IAuthenticationService, Authentication.AuthenticationService>();
            services.AddTransient<IEmailSender,EmailSender>();


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
