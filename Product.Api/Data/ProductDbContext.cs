using Microsoft.EntityFrameworkCore;
using Warehouse.API.Data.Models;

namespace Warehouse.API.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Product> Products { get; set; }
}

