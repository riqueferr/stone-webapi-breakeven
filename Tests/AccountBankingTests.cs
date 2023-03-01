using Microsoft.AspNetCore.Hosting;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Services;

namespace Tests
{
    public class AccountBankingTests
    {

        private readonly IAccountBankingService _accountBankingService;
        private readonly IWalletService _walletService;
        private readonly ReadContext _readContext;

        [Fact]
        public void ConnectDataBase()
        {
            ReadContext connect = new ReadContext();
            
/*            try
            {
                connect.Database.CanConnect();
            }
            catch (Exception ex)
            {
                throw new Exception("Fail: " + ex.Message);
            }*/
        }
    }
}