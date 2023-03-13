using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Profiles;
using stone_webapi_breakeven.Services;


namespace stone_webapi_breakeven.Controllers.Tests
{
    [TestClass()]
    public class ProductControllerTests
    {

        private ProductController _controller;
        private ProductDto updateProduct;

        [TestInitialize]
        public void Initialize()
        {
            var mockService = new Mock<IProductService>();

            mockService.Setup(service => service.CreateProduct(It.IsAny<Product>())).Verifiable();
            mockService.Setup(service => service.ConverterProduct(It.IsAny<Product>(), It.IsAny<ProductDto>())).Verifiable();
            mockService.Setup(service => service.GetProductById(1)).Returns(GetProductById());
            mockService.Setup(service => service.GetProductSkipAndTake(0, 1000)).Returns(GetAllProducts());

            IMapper mapper = new MapperConfiguration(mc => mc.AddProfile(new ProductProfile())).CreateMapper();

            _controller = new ProductController(null, mapper, mockService.Object);

            updateProduct = new ProductDto
            {
                Id = 1,
                Title = "STNE",
                Description = "Ação da empresa",
                Price = 77.0,
                Type = "Action",
                Quantify = 1000
            };
        }

        [TestMethod()]
        public void CreateProductTest()
        {
            var action = _controller.CreateProduct(new Product());
            var product = action as CreatedAtActionResult;

            Assert.IsInstanceOfType(action, typeof(ActionResult));
            Assert.AreEqual(201, product.StatusCode);
        }

        [TestMethod()]
        public void GetProductByIdTest()
        {
            var action = _controller.GetProductById(1);
            var okObjectResult = action as OkObjectResult;
            var product = okObjectResult.Value as Product;

            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual("STNE", product.Title);
            Assert.AreEqual(70, product.Price);
        }

        [TestMethod()]
        public void GetProductSkipAndTakeTest()
        {
            var action = _controller.GetProductSkipAndTake(0, 1000);
            var product = action.ElementAt(2);

            Assert.AreEqual(3, action.Count());
            Assert.AreEqual(3, product.Id);
            Assert.AreEqual("Fundo de investimento da XPTO", product.Description);

        }

        [TestMethod()]
        public void UpdateProductTest()
        {
            var action = _controller.UpdateProduct(1, updateProduct);

            var noContentResult = action as NoContentResult;

            Assert.IsNotNull(action);
            Assert.AreEqual(204, noContentResult.StatusCode);
        }


        //Mock
        private static Product GetProductById()
        {
            return new Product
            {
                Id = 1,
                Title = "STNE",
                Description = "",
                Price = 70.0,
                Type = "FII",
                Quantify = 1000
            };
        }

        private static IEnumerable<Product> GetAllProducts() 
        {
            return new List<Product>
            {
                new Product
                {
                Id = 1,
                Title = "STNE",
                Description = "Ação da empresa",
                Price = 70.0,
                Type = "Action",
                Quantify = 1000
                },

                new Product
                {
                Id = 2,
                Title = "XPTO",
                Description = "Renda Fixa (110%)",
                Price = 18.81,
                Type = "CDB",
                Quantify = 1000
                },

                new Product
                {
                Id = 3,
                Title = "XPTO11",
                Description = "Fundo de investimento da XPTO",
                Price = 21.79,
                Type = "FII",
                Quantify = 1000
                }
            };
        }
    }



   
}