using Microsoft.AspNetCore.Mvc;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Services
{
    public interface IAccountBankingService
    {
        int CreateAccountBanking(AccountBanking accountBanking);
    }
}
