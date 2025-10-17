
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OKA.Application.IService;
using OKA.Application.Services;
using OKA.Domain.IRepositories;
using OKA.Infrastructure.Data;
using OKA.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<OKAStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("constr")));
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
