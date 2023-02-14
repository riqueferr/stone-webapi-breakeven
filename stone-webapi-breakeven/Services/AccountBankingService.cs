using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Enums;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Services
{
    public class AccountBankingService : IAccountBankingService
    {
        private ReadContext _context;
        private WalletService _walletService;

        public AccountBankingService(ReadContext context)
        {
            _context = context;
            _walletService = new WalletService(context);
        }

        public int CreateAccountBanking(AccountBanking accountBanking)
        {

            //Validators

            //Context
            accountBanking.Status = AccountBankingStatus.Active;
            accountBanking.OpentedIn = DateTime.Now;
            accountBanking.WalletId = _walletService.CreateWalletForAccountBanking(accountBanking);
            _context.AccountsBanking.Add(accountBanking);
            _context.SaveChanges();

            return accountBanking.Id;
        }
    }
}
