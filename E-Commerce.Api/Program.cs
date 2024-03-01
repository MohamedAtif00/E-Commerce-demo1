using E_Commerce.Api.Middleware;
using E_Commerce.Application;
using E_Commerce.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configration = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddApplication();
builder.Services.AddInfratsructure(configration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseMiddleware<SwaggerBasicAuthMiddleware>();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.MapControllers(); // Map controllers to routes



app.Run();

