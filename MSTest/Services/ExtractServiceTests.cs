using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Enums;
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
    public class ExtractServiceTests
    {
        private ReadContext _context;
        private IExtractService _service;


        [TestInitialize]
        public void Initialize()
        {
            var builder = new DbContextOptionsBuilder<ReadContext>();
            builder.UseInMemoryDatabase("BreakevenTest");
            var options = builder.Options;

            _context = new ReadContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var accountsBanking = new List<Extract>
            {
                new Extract {ExtractId = 1, ProductId = 10, WalletId = 100, Quantify = 17, TotalPrice = 320, DateTime = DateTime.Parse("2023-02-01"), TransactionStatus = TransactionStatus.Buy.ToString()},
                new Extract {ExtractId = 2, ProductId = null, WalletId = 200, Quantify = null, TotalPrice = 1250, DateTime = DateTime.Parse("2023-01-07"), TransactionStatus = TransactionStatus.Deposit.ToString()},
                new Extract {ExtractId = 3, ProductId = null, WalletId = 100, Quantify = null, TotalPrice = 150, DateTime =  DateTime.Parse("2023-02-08"), TransactionStatus = TransactionStatus.Withdraw.ToString()},
                new Extract {ExtractId = 4, ProductId = 10, WalletId = 100, Quantify = 11, TotalPrice = 120, DateTime = DateTime.Now, TransactionStatus = TransactionStatus.Sell.ToString() }
            };

            _context.AddRange(accountsBanking);
            _context.SaveChanges();

            _service = new ExtractService(_context);
        }

        [TestMethod()]
        public void GetAllExtract()
        {
            var action = _service.GetAllExtract();
            var list = action as List<Extract>;

            Assert.AreEqual(4, action.ToList().Count());
            Assert.AreEqual(null, list[2].ProductId);
            Assert.AreEqual(TransactionStatus.Sell.ToString(), list[3].TransactionStatus);
        }

        [TestMethod()]
        public void GetExtractByWalletIdTest()
        {
            var action = _service.GetExtractByWalletId(100);
            var list = action.ToList();

            Assert.AreEqual(3, action.ToList().Count());
            Assert.AreEqual(1, list[0].ExtractId);
            Assert.AreEqual(TransactionStatus.Buy.ToString(), list[0].TransactionStatus);
        }

        [TestMethod()]
        public void RegisterTransactionTest()
        {
            _service.RegisterTransaction(200, 17, TransactionStatus.Buy, 10, 100);

            var actionGetAll = _service.GetAllExtract();
            var actionGetById = _service.GetExtractByWalletId(200);

/*            var hehe = _service.GetType().GetMethod("SEILA", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            hehe.Invoke(_service, new object[] { 10, 2 });*/

            Assert.AreEqual(5, actionGetAll.Count());
            Assert.AreEqual(2, actionGetById.Count());
        }
    }
}