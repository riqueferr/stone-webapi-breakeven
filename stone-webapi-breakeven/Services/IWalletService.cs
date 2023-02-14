using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Services
{
    public interface IWalletService
    {
        Wallet CreateWallet();
        int CreateWalletForAccountBanking(AccountBanking accountBanking);
    }
}
