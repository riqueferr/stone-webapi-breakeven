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
    public class AccountBankingControllerTests
    {

        private static AccountBankingController _controller;
        private AccountBankingDto updateAccountBankingDto;

        const string GenericDocument = "123456790";

        [TestInitialize]
        public void Initialize()
        {
            var mockService = new Mock<IAccountBankingService>();

            mockService.Setup(service => service.GetAllAccountsBanking()).Returns(GetAllAccountsBankingTest());
            mockService.Setup(service => service.GetAccountBankingById(1)).Returns(GetByIdAccountsBankingTest());
            mockService.Setup(service => service.CreateAccountBanking(It.IsAny<AccountBanking>())).Verifiable();

            IMapper mapper = new MapperConfiguration(c => c.AddProfile(new AccountBankingProfile())).CreateMapper();

            _controller = new AccountBankingController(null, mapper, mockService.Object);

            updateAccountBankingDto = new AccountBankingDto { Document = "1234567904" };
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
            Assert.AreEqual(GenericDocument+"1", account.Document);
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


        [TestMethod()]
        public void UpdateAccountBanking()
        {
            var action = _controller.UpdateAccountBanking(1, updateAccountBankingDto);
            var xxx = action as NoContentResult;

            Assert.IsNotNull(action);
            Assert.AreEqual(204, xxx.StatusCode);
        }


        //Mock
        private static IEnumerable<AccountBanking> GetAllAccountsBankingTest()
        {
            return new List<AccountBanking>()
            {
                new AccountBanking()
                {
                    AccountBankingId = 1,
                    Document = "1234567901",
                    Status = "Active",
                    OpentedIn = DateTime.Now,
                    WalletId = 1
                },

                new AccountBanking()
                {
                    AccountBankingId = 2,
                    Document = "1234567902",
                    Status = "Active",
                    OpentedIn = DateTime.Now,
                    WalletId = 2
                },

                new AccountBanking()
                {
                    AccountBankingId = 3,
                    Document = "1234567903",
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
                    Document = "1234567901",
                    Status = "Active",
                    OpentedIn = DateTime.Now,
                    WalletId = 1
                };
        }
    }

}