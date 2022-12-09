using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.API.Data.Models
{
    [Table(name: "Product")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
