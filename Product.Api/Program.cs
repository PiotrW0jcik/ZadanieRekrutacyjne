using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Warehouse.API.Data;
using Warehouse.API.Routes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbConnectionString = builder.Configuration.GetConnectionString("ProductDb") ?? throw new InvalidOperationException("Missing connection string configuration");
builder.Services.AddSqlServer<ProductDbContext>(dbConnectionString);

builder.Services.AddOutputCache();

builder.Services.AddCors(setup =>
{
    setup.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
// OpenAPI and versioning-related services
builder.Services.AddSwaggerGen();
builder.Services.Configure<SwaggerGeneratorOptions>(opts =>
{
    opts.InferSecuritySchemes = true;
});
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var products = app.MapGroup("/products");
products
    .MapProductsApi();
app.Run();
