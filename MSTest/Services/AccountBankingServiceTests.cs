using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace stone_webapi_breakeven.Services.Tests
{
    [TestClass()]
    public class AccountBankingServiceTests
    {
        private ReadContext _context; 
        private IAccountBankingService _service;
        private IWalletService _walletService;


        [TestInitialize]
        public void Initialize()
        {
            var builder = new DbContextOptionsBuilder<ReadContext>();
            builder.UseInMemoryDatabase("BreakevenTest");
            var options = builder.Options;

            _context = new ReadContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var accountsBanking = new List<AccountBanking>
            {
                new AccountBanking {Document = "123", WalletId = 1, Status = "Active", OpentedIn = DateTime.Parse("2012-12-07")},
                new AccountBanking {Document = "456", WalletId = 2, Status = "Active", OpentedIn = DateTime.Parse("2012-01-30")},
                new AccountBanking {Document = "789", WalletId = 3, Status = "Inactive", OpentedIn = DateTime.Now}
            };

            _context.AddRange(accountsBanking);
            _context.SaveChanges();

            _walletService = new WalletService(_context, null, null);
            _service = new AccountBankingService(_context, _walletService);
        }

        [TestMethod()]
        public void CreateAccountBankingTest()
        {
            var action = _service.CreateAccountBanking(new AccountBanking { Document = "111" });

            Assert.AreEqual(4, action);
        }

        [TestMethod()]
        public void GetAccountBankingByIdTest()
        {
            var action = _service.GetAccountBankingById(3);

            Assert.AreEqual("Inactive", action.Status);
            Assert.AreEqual("789", action.Document);
        }

        [TestMethod()]
        public void GetAllAccountsBankingTest()
        {
            var action = _service.GetAllAccountsBanking();
            var list = action as List<AccountBanking>;

            Assert.AreEqual(3, list.Count);
        }

        [TestMethod()]
        public void DeleteAccountBankingTest()
        {
            
            _service.DeleteAccountBanking(1);
            var action = _service.GetAllAccountsBanking();

            var accountBanking = _service.GetAccountBankingById(1);

            Assert.AreEqual(2, action.ToList().Count());
            Assert.IsNull(accountBanking);
        }

        [TestMethod()]
        public void UpdateAcconuntBankingTest()
        {
            
            var action = _service.GetAccountBankingById(1);
            Assert.AreEqual("123", action.Document);
            Assert.AreEqual("Active", action.Status);
            Assert.AreEqual(1, action.WalletId);

            action.Document = "098";
            _service.UpdateAcconuntBanking(action);

            action = _service.GetAccountBankingById(1);

            Assert.AreEqual("098", action.Document);
            Assert.AreEqual("Active", action.Status);
            Assert.AreEqual(1, action.WalletId);
        }
    }
}