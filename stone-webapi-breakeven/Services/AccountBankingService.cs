using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Enums;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Services
{
    public class AccountBankingService : IAccountBankingService
    {
        private ReadContext _context;

        public AccountBankingService(ReadContext context)
        {
            _context = context;
        }

        public int CreateAccountBanking(AccountBanking accountBanking)
        {

            //Validators

            //Context
            accountBanking.Status = AccountBankingStatus.Active;
            accountBanking.OpentedIn = DateTime.Now;
            _context.AccountsBanking.Add(accountBanking);
            _context.SaveChanges();

            return accountBanking.Id;
        }
    }
}
