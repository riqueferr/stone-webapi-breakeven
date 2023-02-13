using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Services
{
    public interface IWalletService
    {
        int CreateAccountBanking(Wallet wallet);
    }
}
