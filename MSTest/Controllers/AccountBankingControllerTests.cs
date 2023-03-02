using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Profiles;
using stone_webapi_breakeven.Services;
using System.Net;

namespace stone_webapi_breakeven.Controllers.Tests
{
    [TestClass()]
    public class AccountBankingControllerTests
    {

        private static AccountBankingController? _controller;
        private AccountBankingDto xx;

        [TestInitialize]
        public void Initialize()
        {
            var mockService = new Mock<IAccountBankingService>();

            mockService.Setup(service => service.GetAllAccountsBanking()).Returns(GetAllAccountsBankingTest());
            mockService.Setup(service => service.GetAccountBankingById(1)).Returns(GetByIdAccountsBankingTest());
            mockService.Setup(service => service.CreateAccountBanking(It.IsAny<AccountBanking>())).Verifiable();

            IMapper mapper = new MapperConfiguration(c => c.AddProfile(new AccountBankingProfile())).CreateMapper();

            _controller = new AccountBankingController(mapper, mockService.Object);


            xx = new AccountBankingDto { Document = "123123123" };
        }

        [TestMethod()]
        [Description("Criando uma AccountBanking a partir do DTO")]
        public void CreateAccountBanking()
        {
            var action = _controller.CreateAccountBanking(new AccountBankingDto());

            Assert.IsInstanceOfType(action, typeof(ActionResult));
        }

        [TestMethod()]
        [TestCategory("Unit")]
        [Description("Testando a busca por um valor passando ID")]
        public void GetById_AccountsBanking()
        {

            //Act
            var action = _controller.GetAccountBankingById(1);
            var okObjectResult = action as OkObjectResult;

            //Assert
            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(ActionResult));
            AccountBanking account = okObjectResult.Value as AccountBanking;
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual("11111111111", account.Document);
            Assert.AreEqual("Active", account.Status);
            Assert.AreEqual(1, account.WalletId);
        }

        [TestMethod()]
        [TestCategory("Unit")]
        [Description("Testando a busca por todos os valores na lista")]
        public void GetAll_AccountsBanking()
        {
            var action = _controller.GetAllAccountBanking();
            Assert.IsNotNull(action);
            Assert.AreEqual(3, action.Count());
            Assert.IsInstanceOfType(action, typeof(List<AccountBanking>));
        }

        [TestMethod()]
        public void DeleteAccountBanking()
        {
            var action = _controller.DeleteAccountBanking(1);
            var okObjectResult = action as NoContentResult;

            Assert.IsNotNull(action);
            Assert.IsInstanceOfType(action, typeof(ActionResult));
            Assert.AreEqual(204, okObjectResult.StatusCode);
        }


/*        [TestMethod()]
        public void UpdateAccountBanking()
        {
            var action = _controller.UpdateAccountBanking(1, xx);

            Assert.AreEqual(action, typeof(ActionResult));
            Assert.AreEqual("123123123", xx.Document);
        }*/


        //Mock
        private static IEnumerable<AccountBanking> GetAllAccountsBankingTest()
        {
            return new List<AccountBanking>()
            {
                new AccountBanking()
                {
                    AccountBankingId = 1,
                    Document = "11111111111",
                    Status = "Active",
                    OpentedIn = DateTime.Now,
                    WalletId = 1
                },

                new AccountBanking()
                {
                    AccountBankingId = 2,
                    Document = "22222222222",
                    Status = "Active",
                    OpentedIn = DateTime.Now,
                    WalletId = 2
                },

                new AccountBanking()
                {
                    AccountBankingId = 3,
                    Document = "33333333333",
                    Status = "Active",
                    OpentedIn = DateTime.Now,
                    WalletId = 3
                },
            };
        }

        private static AccountBanking GetByIdAccountsBankingTest()
        {
            return new AccountBanking
                {
                    AccountBankingId = 1,
                    Document = "11111111111",
                    Status = "Active",
                    OpentedIn = DateTime.Now,
                    WalletId = 1
                };
        }
    }

}