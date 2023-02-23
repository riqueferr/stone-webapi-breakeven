using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Services;

namespace Tests
{
    public class AccountBankingTests
    {

        private readonly IAccountBankingService _accountBankingService;

        public AccountBankingTests()
        {
            var service = new ServiceCollection();
            service.AddTransient<IAccountBankingService, AccountBankingService>();
            var provider = service.BuildServiceProvider();
            _accountBankingService = provider.GetService<IAccountBankingService>();
        }

        /*[Fact]
        public void ConnectDataBase()
        {
            var connect = new ReadContext();
            

            try
            {
                connect.Database.CanConnect();
            }
            catch (Exception ex)
            {
                throw new Exception("Fail: " + ex.Message);
            }
        }*/

        [Fact]
        public void ListAccountBankingService()
        {

            //Act
            ICollection<AccountBanking> list; //= _accountBankingService.


            //Assert;
            //Assert.NotNull(list);
        }

    }
}