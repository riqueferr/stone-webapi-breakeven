using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Exceptions;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stone_webapi_breakeven.Services.Tests
{
    [TestClass()]
    public class WalletServiceTests
    {
        private ReadContext _context;
        private IWalletService _service;
        private IExtractService _extractService;
        private IProductService _productService;

        [TestInitialize()]
        public void Initialize()
        {
            var builder = new DbContextOptionsBuilder<ReadContext>();
            builder.UseInMemoryDatabase("BreakevenTest");
            var options = builder.Options;

            _context = new ReadContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var product = new Product() { Id = 1, Title = "STNE", Price = 10, Description = "Melhor verdinha", Quantify = 1000, Type = "Action" };

            var walletProducts = new List<WalletProduct>
            {
                new WalletProduct {WalletProductId = 1, WalletId = 2, AverageTicket= 10, Percentage = 0, ProductTitle = product.Title, Quantify = 1, TotalPrice= 10},
            };

            var wallets = new List<Wallet>
            {
                new Wallet {WalletId = 1, FreeAmount = 500, InvestedAmount = 0, TotalAmount = 500, Products = null},
                new Wallet {WalletId = 2, FreeAmount = 500, InvestedAmount = 100, TotalAmount = 600, Products = walletProducts},
            };

            _context.AddRange(wallets);
            _context.AddRange(product);
            _context.AddRange(walletProducts);
            _context.SaveChanges();

            _extractService = new ExtractService(_context);
            _productService = new ProductService(_context);
            _service = new WalletService(_context, _extractService, _productService);
        }

        [TestMethod()]
        public void CreateWalletTest()
        {
            var action = _service.CreateWallet();

            Assert.IsNotNull(action);
            Assert.AreEqual(3, action.WalletId);
            Assert.AreEqual(0, action.TotalAmount);
        }

        [TestMethod()]
        public void DepositOrWithdrawWalletTest()
        {
            _service.DepositOrWithdrawWallet(2, new WalletDto { Action = "Deposit", Balance = 50});

            var action = _service.GetWalletById(2);

            Assert.AreEqual(550, action.FreeAmount);
            Assert.AreEqual(650, action.TotalAmount);

            _service.DepositOrWithdrawWallet(2, new WalletDto { Action = "Withdraw", Balance = 550 });

            Assert.AreEqual(0, action.FreeAmount);
            Assert.AreEqual(100, action.TotalAmount);

        }

        [TestMethod()]
        public void GetWalletByIdTest()
        {
            var action = _service.GetWalletById(1);

            Assert.IsNotNull(action);
            Assert.AreEqual(1, action.WalletId);
            Assert.IsNull(action.Products);
        }

        [TestMethod()]
        public void GetWalletByIdAndProductsDetailsTest()
        {
            var action = _service.GetWalletByIdAndProductsDetails(2);
            var walletProduct = action as List<WalletProduct>;

            Assert.AreEqual(1, walletProduct.Count());
        }

        [TestMethod()]
        public void OrderBuyOrSellProductTest()
        {
            _service.OrderBuyOrSellProduct(1, new ProductDto { Action = "Buy", Title = "STNE", Quantify = 1 });

            var action = _service.GetWalletById(1);

            Assert.IsNotNull(action);
            Assert.AreEqual(1, action.Products.Count());
            Assert.AreEqual("STNE", action.Products.First().ProductTitle);
        }

        [TestMethod()]
        public void CalculateProductToWalletTest()
        {
            _service.OrderBuyOrSellProduct(1, new ProductDto { Action = "Buy", Title = "STNE", Quantify = 1 });

            var product = _productService.GetProductById(1);
            var wallet = _service.GetWalletById(1);
            _productService.ConverterProduct(product, new ProductDto { Price = 20.55 });

            var products = _service.GetWalletByIdAndProductsDetails(1);

            var action = _service.CalculateProductToWallet(products, wallet);

            Assert.IsNotNull(action);
            Assert.AreEqual(20.55000000000001, action.InvestedAmount);
            Assert.AreEqual(490, action.FreeAmount);
            Assert.AreEqual(510.55, action.TotalAmount);
            Assert.AreEqual(1, action.Products.Count());
            Assert.AreEqual("STNE", action.Products.First().ProductTitle);
            Assert.AreEqual(1.0550000000000002, action.Products.First().Percentage);

        }
        [TestMethod()]
        public void DepositOrWithdrawWalletBalanceErrorTest()
        {
        Assert.ThrowsException<BreakevenException>(() => _service.DepositOrWithdrawWallet(2, new WalletDto { Action = "Deposit", Balance = -40 }));
        }

        [TestMethod()]
        public void DepositOrWithdrawWalletActionErrorTest()
        {
            var exception = Assert.ThrowsException<BreakevenException>(() => _service.DepositOrWithdrawWallet(2, new WalletDto { Action = "Buy", Balance = 10 }));

            Assert.AreEqual("Não é possível converter a Action informada para TransationEnum", exception);
        }

    }
}