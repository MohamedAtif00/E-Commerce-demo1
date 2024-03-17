using E_Commerce.Api.Mapping;
using E_Commerce.Api.Middleware;
using E_Commerce.Application;
using E_Commerce.Domain.Model.User;
using E_Commerce.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
var configration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => 
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
    {
        In = ParameterLocation.Header,
        Name = "Authentication",
        Type = SecuritySchemeType.Http,
        Description = "Please enter valid jwt",
        BearerFormat = "jwt",
        Scheme = "Bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement 
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[]{ }
        }
    });
});


builder.Services.AddApplication(configration);
builder.Services.AddInfratsructure(configration);
builder.Services.AddDefaultIdentity<IdentityUser<Guid>>(opt => 
{
    opt.SignIn.RequireConfirmedAccount = true;
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<DbContextClass>()
    .AddDefaultTokenProviders();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();



builder.Services.AddAutoMapper(x =>x.AddProfile<MappingProfile>());

//builder.Services.AddSingleton<IPasswordHasher<IdentityUser>, PasswordHasher<IdentityUser>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseMiddleware<SwaggerBasicAuthMiddleware>();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.DefaultModelExpandDepth(-1);
    });
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // Map controllers to routes


app.Run();

