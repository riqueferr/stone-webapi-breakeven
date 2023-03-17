using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Services.Tests
{
    [TestClass()]
    public class ProductServiceTests
    {
        private ReadContext _context;
        private IProductService _service;

        [TestInitialize()]
        public void Initialize()
        {
            var builder = new DbContextOptionsBuilder<ReadContext>();
            builder.UseInMemoryDatabase("BreakevenTest");
            var options = builder.Options;

            _context = new ReadContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var products = new List<Product>
            {
                new Product {Id =1, Title = "STNE", Description = "Melhor verdinha", Price = 10.10, Quantify = 1000, Type = "Action"},
                new Product {Id =2,Title = "XPTO4", Description = "Descrição da XPTO4", Price = 1.20, Quantify = 1000, Type = "Action"},
                new Product {Id =3,Title = "XPTO11", Description = "Descrição da XPTO11", Price = 1.00, Quantify = 1000, Type = "FII"}
            };

            _context.AddRange(products);
            _context.SaveChanges();

            _service = new ProductService(_context);
        }

        [TestMethod()]
        public void CreateProductTest()
        {
            var action = _service.CreateProduct(new Product { Title = "STNE11", Description = "Melhor verdinha", Price = 10.10, Quantify = 1000, Type = "FII" });
            var actionGetById = _service.GetProductById(4);

            Assert.AreEqual(4, action);
            Assert.IsNotNull(actionGetById);
            Assert.AreEqual("Melhor verdinha", actionGetById.Description);
            Assert.AreEqual("STNE11", actionGetById.Title);
        }

        [TestMethod()]
        public void GetProductByIdTest()
        {
            var actionGetById = _service.GetProductById(1);

            Assert.IsNotNull(actionGetById);
            Assert.AreEqual("Melhor verdinha", actionGetById.Description);
            Assert.AreEqual("STNE", actionGetById.Title);
            Assert.AreEqual(10.10, actionGetById.Price);
            Assert.AreEqual("Action", actionGetById.Type);
        }
    }
}