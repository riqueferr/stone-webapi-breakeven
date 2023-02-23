using Microsoft.AspNetCore.Mvc;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Services
{
    public interface IAccountBankingService
    {
        int CreateAccountBanking(AccountBanking accountBanking);
        IEnumerable<AccountBanking> GetAllAccountsBanking();
        AccountBanking GetAccountBankingById(int id);
        IEnumerable<AccountBanking> GetAccountBankingSkipAndTake(int skip, int take);
    }
}
