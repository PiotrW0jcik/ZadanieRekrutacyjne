using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Warehouse.API.Data;
using Warehouse.API.Data.DTOs;

namespace Warehouse.API.Routes;

public static class ProductsApi
{
    public static RouteGroupBuilder MapProductsApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllProducts).WithName("GetAllProducts");
        group.MapGet("/{id:int}", GetProductById).WithName("GetProductById");
        group.MapGet("/{name}", GetProductByName).WithName("GetProductByName");
        return group;
    }

    public static async ValueTask<Ok<List<ProductDto>>> GetAllProducts(ProductDbContext ProductDbContext, CancellationToken cancellationToken)
    {
        var products = await ProductDbContext.Products
            .Select(product => new ProductDto(product))
            .ToListAsync(cancellationToken);
        return TypedResults.Ok(products);
    }

    public static async ValueTask<Ok<ProductDto>> GetProductById(ProductDbContext ProductDbContext, CancellationToken cancellationToken, int id)
    {
        var product = await ProductDbContext.Products
            .Where(product => product.Id == id)
            .Select(product => new ProductDto(product))
            .FirstAsync(cancellationToken);
        return TypedResults.Ok(product);
    }

    public static async ValueTask<Ok<ProductDto>> GetProductByName(ProductDbContext ProductDbContext, CancellationToken cancellationToken, string name)
    {
        var product = await ProductDbContext.Products
            .Where(product => product.Name == name)
            .Select(product => new ProductDto(product))
            .FirstAsync(cancellationToken);
        return TypedResults.Ok(product);
    }
}
