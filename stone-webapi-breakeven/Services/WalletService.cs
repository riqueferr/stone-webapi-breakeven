using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Services
{
    public class WalletService : IWalletService
    {

        private readonly ReadContext _context;

        public WalletService(ReadContext context)
        {
            _context = context;
        }

        public Wallet CreateWallet()
        {
            Wallet wallet = new Wallet();
            wallet.Balance = 0;
            _context.Wallets.Add(wallet); ;
            _context.SaveChanges();

            return wallet;
        }

        public int CreateWalletForAccountBanking(AccountBanking accountBanking)
        {

            Wallet wallet = new Wallet();
            wallet.Balance = 0;
            _context.Wallets.Add(wallet); ;
            _context.SaveChanges();
            accountBanking.WalletId = wallet.Id;

            return wallet.Id;
        }
    }
}
