using stone_webapi_breakeven.Data;
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
            _context.AccountsBanking.Add(accountBanking);
            _context.SaveChanges();

            return accountBanking.Id;
        }
    }
}
