using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Services;

namespace stone_webapi_breakeven.Conveters
{
    public class AccountBankingProductConverter : IAccountBankingProductConverter
    {
        public AccountBankingProductDto toJson(AccountBankingProduct accountBankingProduct)
        {

            AccountBankingProductDto accountBankingProductDto = new AccountBankingProductDto();
            accountBankingProductDto.ProductId = accountBankingProduct.ProductId;
            accountBankingProductDto.Quantify = accountBankingProduct.Quantify;
            accountBankingProductDto.TotalPrice = accountBankingProduct.TotalPrice;
            accountBankingProductDto.Percentage = accountBankingProduct.Percentage;

            return accountBankingProductDto;
        }
    }
}
