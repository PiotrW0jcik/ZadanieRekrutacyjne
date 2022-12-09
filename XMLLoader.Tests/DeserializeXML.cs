using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using XMLLoader.Data;
using XMLLoader.Models;
using XMLLoader.Services;

namespace XMLLoader.Tests
{
    public class DeserializeXML
    {
        private ILogger<XMLService> _logger;
        private IConfiguration _configuration;
        private ProductDbContext _productDbContext;

        [SetUp]
        public void Setup()
        {
            _logger = Mock.Of<ILogger<XMLService>>();
            _configuration = Mock.Of<IConfiguration>();
            _productDbContext = Mock.Of<ProductDbContext>();
        }

        [TestCase("products.xml")]
        public void Deserialize_Object_Should_Deserialize_Correctly(string filename)
        {
            XMLService xMLService = new XMLService(_logger, _configuration, _productDbContext);
            var products = xMLService.DeserializeObject(filename);

            products.Product[0].Name.Should().BeEquivalentTo("product1");
            products.Product[0].Category.Should().BeEquivalentTo("category1");
            products.Product[1].Name.Should().BeEquivalentTo("product2");
            products.Product[0].Description.Should().BeEquivalentTo("descritpion1");
            products.Product[1].Description.Should().BeEquivalentTo("descritpion2");
        }
    }
}