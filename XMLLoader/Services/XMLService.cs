using XMLLoader.Models;

namespace XMLLoader.Services
{
    public interface IXMLService
    {
       Task AddOrUpdateProducts();
    }

    public class XMLService : IXMLService
    {
        private readonly ILogger<XMLService> _logger;
        private readonly IConfiguration _configuration;
        private readonly ProductDbContext _ProductDbContext;


        public XMLService(ILogger<XMLService> logger, IConfiguration configuration, ProductDbContext ProductDbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _ProductDbContext = ProductDbContext;
        }
        public async Task AddOrUpdateProducts() {

            string productsFilesPath = _configuration.GetValue<string>("XMLFilesPath");
            DirectoryInfo fileDirectory = new DirectoryInfo(productsFilesPath);

            _logger.LogInformation("Loading files from : {path}", productsFilesPath);
            try
            {
                foreach (var file in fileDirectory.GetFiles("*.xml"))
                {
                    _logger.LogInformation("Deserializing products from : {fileName}", file.FullName);
                    try
                    {
                        Products products = DeserializeObject(file.FullName);
                        
                        _logger.LogInformation("Saving products from {fileName} to database", file.FullName);

                        try
                        {
                            await AddOrUpdateProductsToDb(products);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Error while saving products: {error}", ex.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Error deserializing products: {error}", ex.Message);
                    }
                }
            }
             catch (Exception ex)
            {
                _logger.LogError("Error loading files: {error}", ex.Message);
            }
        }

        public async Task AddOrUpdateProductsToDb(Products products)
        {
            foreach (var product in products.Product)
            {
                var exist = _ProductDbContext.Products.SingleOrDefault(p => p.Name == product.Name);
                if (exist != null)
                {
                    exist.Price = product.Price;
                    exist.Description = product.Description;
                    exist.Category = product.Category;
                    exist.Quantity = product.Quantity;
                }
                else
                {
                    _ProductDbContext.Products.Add(product);
                }

            }

            await _ProductDbContext.SaveChangesAsync();   
        }

        public Products DeserializeObject(string filename)
        {
            XmlSerializer serializer =new XmlSerializer(typeof(Products));
            Products products;
            using (Stream reader = new FileStream(filename, FileMode.Open))
            {
                products = (Products)serializer.Deserialize(reader);
            }

            return products;
        }
    }
}
