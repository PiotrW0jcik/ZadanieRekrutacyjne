namespace XMLLoader.Data;

public class ProductDbContext : DbContext
{
    protected ProductDbContext(){}
    public ProductDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Product> Products { get; set; }
}

