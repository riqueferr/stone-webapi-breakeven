using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using stone_webapi_breakeven.Enums;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Services;


namespace stone_webapi_breakeven.Controllers.Tests
{
    [TestClass()]
    public class ExtractControllerTests
    {
        private static ExtractController? _controller;

        [TestInitialize]
        public void Initialize()
        {
            var mockService = new Mock<IExtractService>();

            mockService.Setup(service => service.GetExtractByWalletId(17)).Returns(GetByIdExtractTest());

            _controller = new ExtractController(mockService.Object);
        }
        

        [TestMethod()]
        public void GetExtractByWalletIdTest()
        {
            var action = _controller.GetExtractByWalletId(17);
            Extract extract = action.ElementAt(0);

            Assert.IsNotNull(action);
            Assert.AreEqual(1, action.Count());
            Assert.AreEqual(177.71, extract.TotalPrice);
            Assert.AreEqual(TransactionStatus.Deposit.ToString(), extract.TransactionStatus);
            Assert.IsTrue(DateTime.Now > extract.DateTime);
        }


        //Mock
        private static IEnumerable<Extract> GetByIdExtractTest()
        {

            return new List<Extract>
            {
                new Extract 
                {
                    ExtractId = 1,
                    WalletId = 17,
                    ProductId = 7,
                    TransactionStatus = "Deposit",
                    Quantify = 0,
                    TotalPrice = 177.71,
                    DateTime = DateTime.Now
                }
            };
        }
    }
}