using Microsoft.AspNetCore.Mvc;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Enums;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Services
{
    public interface IWalletService
    {
        Wallet CreateWallet();
        int CreateWalletForAccountBanking(AccountBanking accountBanking);
        ICollection<WalletProduct> GetWalletByIdAndProductsDetails(int id);
        Wallet GetWalletById(int id);
        void DepositOrWithdrawWallet(int id, WalletDto walletDto);
        void OrderBuyOrSellProduct(int id, ProductDto productDto);
        Wallet CalculateProductToWallet(IEnumerable<WalletProduct> products, Wallet wallet);
    }
}
