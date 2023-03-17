using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Services;

namespace stone_webapi_breakeven.Controllers.Tests
{
    [TestClass()]
    public class WalletControllerTests
    {

        private WalletController _controller;


        [TestInitialize]
        public void Initialize()
        {
            var mockService = new Mock<IWalletService>();
            mockService.Setup(service => service.GetWalletById(1)).Returns(GetByIdWalletTest());
            mockService.Setup(service => service.CreateWallet()).Returns(new Wallet());
            mockService.Setup(service => service.GetWalletByIdAndProductsDetails(1)).Returns(GetWalletByIdAndProductsDetailsTest());

            _controller = new WalletController(null, mockService.Object);

        }

        [TestMethod()]
        public void CreateWalletTest()
        {
           var action = _controller.CreateWallet();
            var createdAtActionResult = action as CreatedAtActionResult;
            var wallet = createdAtActionResult.Value as Wallet;

            Assert.IsNotNull(action);
            Assert.AreEqual(201, createdAtActionResult.StatusCode);
            Assert.IsNotNull(wallet);

        }

        [TestMethod()]
        public void GetWalletByIdTest()
        {
            var action = _controller.GetWalletById(1);
            var okObjectResult = action as OkObjectResult;
            var wallet = okObjectResult.Value as Wallet;

            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(OkObjectResult));
            Assert.IsNotNull(wallet);
            Assert.AreEqual(1, wallet.WalletId);
            Assert.AreEqual(200, okObjectResult.StatusCode);
        }

        [TestMethod()]
        public void DepositOrWithdrawWalletTest()
        {
            var action = _controller.DepositOrWithdrawWallet(1,
                new WalletDto() { Action = "Withdraw", Balance = 10 });
            var okObjectResult = action as OkResult;


            Assert.IsInstanceOfType(action, typeof(OkResult));
            Assert.AreEqual(200, okObjectResult.StatusCode);

        }

        [TestMethod()]
        public void OrderBuyOrSellProductTest()
        {
            var action = _controller.OrderBuyOrSellProduct(1,
                new ProductDto() { Action = "Buy", Quantify = 1, Title = "STNE" });
            var okObjectResult = action as OkResult;

            Assert.IsInstanceOfType(action, typeof(OkResult));
            Assert.AreEqual(200, okObjectResult.StatusCode);
        }


        //Mock

        private Wallet GetByIdWalletTest()
        {
            return new Wallet
            {
                WalletId = 1,
                FreeAmount= 0,
                InvestedAmount= 0,
                TotalAmount = 0,
                WalletProducts = null                
            };
        }

        private IEnumerable<WalletProduct> GetWalletByIdAndProductsDetailsTest()
        {;

            return new List<WalletProduct>
            {
                new WalletProduct
                {
                    WalletId = 1,
                    WalletProductId = 1,
                    Percentage = 0,
                    AverageTicket = 70.0,
                    Quantify = 1,
                    TotalPrice = 70,
                    ProductTitle = "STNE"
                }
            };
        }
    }
}