using Warehouse.API.Data.Models;

namespace Warehouse.API.Data.DTOs   
{
    public record ProductDto
    {
        public ProductDto(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Category = product.Category;
            Quantity = product.Quantity;
            Price = product.Price;
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Category { get; }
        public int Quantity { get; }
        public double Price { get; }
    }
}

