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

            var formatText = FormartAction(walletDto);
            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.WalletId == id);

            if (formatText == TransactionStatus.Deposit.ToString())
            {
                CalculateDepositOrWithDrawWallet(wallet, walletDto, formatText);
                _context.SaveChanges();

                return true;
            }
            else if (formatText == TransactionStatus.Withdraw.ToString())
            {
                CalculateDepositOrWithDrawWallet(wallet, walletDto, formatText);
                _context.SaveChanges();

                return true;
            }
            else
            {
                throw new Exception("Action não localizada.");
            }


        }

        public Wallet GetWalletById(int id)
        {
            var result = _context.Wallets.FirstOrDefault(wallet => wallet.WalletId == id);

            return result;
        }

        public ICollection<WalletProduct> GetWalletByIdAndProductsDetails(int id)
        {
            var result = _context.AccountBankingProducts.Where(x => x.WalletId == id).ToList();

            return result;
        }

        public bool OrderBuyOrSellProduct(int id, ProductDto productDto)
        {
            productDto.Action = char.ToUpper(productDto.Action[0]) + productDto.Action.Substring(1);
            var product = _context.Products.FirstOrDefault(product => product.Title == productDto.Title);
            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.WalletId == id);

            var calcTotalPrice = CalculateTotalPriceBuyOrSell(product.Price, productDto.Quantify);

            if (productDto.Action == TransactionStatus.Buy.ToString())
            {
                CreateBuyProductInWallet(wallet, product, productDto, calcTotalPrice);
            }
            else if (productDto.Action == TransactionStatus.Sell.ToString())
            {
                var productSell = _context.AccountBankingProducts.FirstOrDefault(x => x.ProductId == productDto.Id);

                var calculatePercentage = CalculatePercentage(product, productSell);

                wallet.InvestedAmount -= productSell.TotalPrice;


                wallet.FreeAmount += productSell.TotalPrice + (productSell.TotalPrice * calculatePercentage);
                wallet.TotalAmount = wallet.InvestedAmount + wallet.FreeAmount + (productSell.TotalPrice * calculatePercentage);


                RemoveQuantifyProduct(productSell);

                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public Wallet? CalculateProductToWallet(IEnumerable<WalletProduct> products, Wallet walletPersist)
        {
            if (products != null)
            {
                foreach (var product in products)
                {
                    var productPersist = _context.Products.FirstOrDefault(p => p.Id == product.ProductId);
                    product.Percentage = ((productPersist.Price - product.AverageTicket) / product.AverageTicket);
                    product.TotalPrice += product.TotalPrice * product.Percentage;

                    var diferent = product.TotalPrice - (product.AverageTicket * product.Quantify);

                    walletPersist.TotalAmount += diferent;

                    walletPersist.Products.Add(product);
                }
                walletPersist.InvestedAmount = walletPersist.TotalAmount - walletPersist.FreeAmount;
                return walletPersist;
            }
            else
            {
                return null;
            }
        }

        private double CalculateTotalPriceBuyOrSell(double Price, int Quantify)
        {
            var calcTotalPrice = Price * Quantify;

            return calcTotalPrice;
        }

        private void RemoveQuantifyProduct(WalletProduct productSell)
        {
            productSell.Quantify -= productSell.Quantify;
            if (productSell.Quantify == 0)
            {
                _context.AccountBankingProducts.Remove(productSell);
            }
        }

        private double CalculatePercentage(Product product, WalletProduct productSell)
        {
            var result = ((product.Price - productSell.AverageTicket) / productSell.AverageTicket);

            return result;
        }

        private string FormartAction(WalletDto walletDto)
        {
            var text = char.ToUpper(walletDto.Action[0]) + walletDto.Action.Substring(1);
            return text;
        }

        private void CalculateDepositOrWithDrawWallet(Wallet wallet, WalletDto walletDto, string text)
        {
            if (text == TransactionStatus.Deposit.ToString())
            {
                wallet.FreeAmount += walletDto.Balance;
                wallet.TotalAmount = wallet.FreeAmount + wallet.InvestedAmount;
            }

            if (text == TransactionStatus.Withdraw.ToString())
            {
                wallet.FreeAmount -= walletDto.Balance;
                wallet.TotalAmount = wallet.FreeAmount + wallet.InvestedAmount;
            }
        }

        private bool CreateBuyProductInWallet(Wallet wallet, Product product, ProductDto productDto, double calcTotalPrice)
        {
            if (wallet.FreeAmount >= calcTotalPrice && product.Quantify >= productDto.Quantify)
            {
                wallet.FreeAmount -= calcTotalPrice;
                wallet.InvestedAmount += calcTotalPrice;

                WalletProduct accountBankingProduct = new WalletProduct();
                accountBankingProduct.WalletId = wallet.WalletId;
                accountBankingProduct.ProductId = product.Id;
                accountBankingProduct.Quantify = productDto.Quantify;
                accountBankingProduct.TotalPrice = calcTotalPrice;
                accountBankingProduct.AverageTicket = product.Price;

                _context.AccountBankingProducts.Add(accountBankingProduct);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
