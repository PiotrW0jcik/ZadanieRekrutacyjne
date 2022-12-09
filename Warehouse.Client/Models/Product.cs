namespace Warehouse.Client.Models
{
    public record ProductDto(
     int Id,
     string Name,
     string Description, 
     string Category, 
     int Quantity, 
     double Price );
}
