using Microsoft.AspNetCore.Mvc;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Enums;
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
            wallet.InvestedAmount = 0;
            wallet.TotalAmount = 0;
            wallet.FreeAmount = 0;
            _context.Wallets.Add(wallet); ;
            _context.SaveChanges();

            return wallet;
        }

        public int CreateWalletForAccountBanking(AccountBanking accountBanking)
        {

            Wallet wallet = new Wallet();
            wallet.Balance = 0;
            wallet.InvestedAmount = 0;
            wallet.TotalAmount = 0;
            wallet.FreeAmount = 0;
            _context.Wallets.Add(wallet); ;
            _context.SaveChanges();
            accountBanking.WalletId = wallet.WalletId;

            return wallet.WalletId;
        }

        public bool DepositOrWithdrawWallet(int id, WalletDto walletDto)
        {

            var formatText = char.ToUpper(walletDto.Action[0]) + walletDto.Action.Substring(1);

            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.WalletId == id);
            if (formatText == TransactionStatus.Deposit.ToString())
            {
                wallet.FreeAmount += walletDto.Balance;
                wallet.TotalAmount += walletDto.Balance;
                _context.SaveChanges();

                return true;
            } else if (formatText == TransactionStatus.Withdraw.ToString())
            {
                wallet.FreeAmount -= walletDto.Balance;
                wallet.TotalAmount -= walletDto.Balance;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public Wallet GetWalletById(int id)
        {
            var result =  _context.Wallets.FirstOrDefault(wallet => wallet.WalletId == id);

            return result;
        }

        public ICollection<AccountBankingProduct> GetWalletByIdAndProductsDetails(int id)
        {
            var result = _context.AccountBankingProducts.Where(x => x.WalletId == id).ToList();

            return result;
        }

        public bool OrderBuyOrSellProduct(int id, ProductDto productDto)
        {
            var product = _context.Products.FirstOrDefault(product => product.Id == productDto.Id);
            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.WalletId == id);

            var calcTotalPrice = CalculateTotalPriceBuyOrSell(product.Price, productDto.Quantify);

            if (wallet.FreeAmount >= calcTotalPrice && product.Quantify >= productDto.Quantify)
            {
                wallet.FreeAmount -= calcTotalPrice;
                wallet.InvestedAmount += calcTotalPrice;

                AccountBankingProduct accountBankingProduct = new AccountBankingProduct();
                accountBankingProduct.WalletId = wallet.WalletId;
                accountBankingProduct.ProductId = product.Id;
                accountBankingProduct.Quantify = productDto.Quantify;
                accountBankingProduct.TotalPrice = calcTotalPrice;
                accountBankingProduct.AverageTicket = product.Price;

                _context.AccountBankingProducts.Add(accountBankingProduct);

                _context.SaveChanges();
                return true;
            }

/*            if (productDto.Quantify > 0)
            {
                wallet.FreeAmount += calcTotalPrice;
                product.Quantify += productDto.Quantify;

            }*/

            return false;
        }

        private double CalculateTotalPriceBuyOrSell(double Price, int Quantify)
        {
            var calcTotalPrice = Price * Quantify;

            return calcTotalPrice;
        }
        
    }


}
