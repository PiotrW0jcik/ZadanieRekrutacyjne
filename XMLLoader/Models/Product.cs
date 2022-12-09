using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XMLLoader.Models
{

    [Table(name: "Product")]
    [XmlRoot(ElementName = "product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }
        [XmlAttribute(AttributeName = "category")]
        public string Category { get; set; }
        [XmlAttribute(AttributeName = "quantity")]
        public int Quantity { get; set; }
        [XmlAttribute(AttributeName = "price")]
        public double Price { get; set; }
    }

    [XmlRoot(ElementName = "products")]
    public class Products
    {
        [XmlElement(ElementName = "product")]
        public List<Product> Product { get; set; }
    }
}
