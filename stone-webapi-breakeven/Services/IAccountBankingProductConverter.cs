using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Services
{
    public interface IAccountBankingProductConverter
    {
        AccountBankingProductDto toJson(AccountBankingProduct accountBankingProduct);
    }
}
