using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Enums;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Services
{
    public class AccountBankingService : IAccountBankingService
    {
        private ReadContext _context;
        private IWalletService _walletService;

        public AccountBankingService(ReadContext context, IWalletService service)
        {
            _context = context;
            _walletService = service;
        }

        public int CreateAccountBanking(AccountBanking accountBanking)
        {
            accountBanking.Status = AccountBankingStatus.Active.ToString();
            accountBanking.OpentedIn = DateTime.Now;
            accountBanking.WalletId = _walletService.CreateWalletForAccountBanking(accountBanking);
            _context.AccountsBanking.Add(accountBanking);
            _context.SaveChanges();

            return accountBanking.AccountBankingId;
        }

        public AccountBanking GetAccountBankingById(int id)
        {
            return _context.AccountsBanking.FirstOrDefault(accountBanking => accountBanking.AccountBankingId == id);
        }

        public IEnumerable<AccountBanking> GetAllAccountsBanking() => _context.AccountsBanking.ToList();

        public IEnumerable<AccountBanking> GetAccountBankingSkipAndTake(int skip, int take)
        {
            return _context.AccountsBanking.Skip(skip).Take(take);
        }

        public void DeleteAccountBanking(int id)
        {
            var accountBanking = _context.AccountsBanking.Find(id);
            _context.Remove(accountBanking);
            _context.SaveChanges();
        }

        public void UpdateAcconuntBanking(AccountBanking accountBanking)
        {

            _context.Update(accountBanking);
            _context.SaveChanges();
        }
    }
}
