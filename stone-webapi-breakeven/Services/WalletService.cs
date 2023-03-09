using Microsoft.AspNetCore.Mvc;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Enums;
using stone_webapi_breakeven.Exceptions;
using stone_webapi_breakeven.Models;
using System.Runtime.Serialization;

namespace stone_webapi_breakeven.Services
{
    public class WalletService : IWalletService
    {

        private readonly ReadContext _context;
        private IExtractService _extractService;
        private IProductService _productService;

        public WalletService(ReadContext context, IExtractService extractService, IProductService productService)
        {
            _context = context;
            _extractService = extractService;
            _productService = productService;
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

        public void DepositOrWithdrawWallet(int id, WalletDto walletDto)
        {
            var transactionStatus = ConverterStringFromTransactionEnum(walletDto.Action);
            var wallet = GetWalletById(id);


            switch (transactionStatus)
            {
                case TransactionStatus.Deposit:
                    CalculateDepositOrWithDrawWallet(wallet, walletDto, transactionStatus);
                    _extractService.RegisterTransaction(wallet.WalletId, null, TransactionStatus.Deposit, null, walletDto.Balance);
                    _context.SaveChanges();
                    break;

                case TransactionStatus.Withdraw:
                    CalculateDepositOrWithDrawWallet(wallet, walletDto, transactionStatus);
                    _extractService.RegisterTransaction(wallet.WalletId, null, TransactionStatus.Withdraw, null, walletDto.Balance);
                    _context.SaveChanges();
                    break;

                default:
                    throw new BreakevenException("Não é possível converter a Action informada para TransationEnum");
            }
        }

        public Wallet GetWalletById(int id)
        {
            return _context.Wallets.FirstOrDefault(wallet => wallet.WalletId == id); ;
        }

        public IEnumerable<WalletProduct> GetWalletByIdAndProductsDetails(int id)
        {
            return _context.WalletProducts.Where(x => x.WalletId == id).ToList(); ;
        }

        public void OrderBuyOrSellProduct(int id, ProductDto productDto)
        {
            TransactionStatus status = ConverterStringFromTransactionEnum(productDto.Action);
            var product = _productService.GetProductByTitle(productDto.Title);
            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.WalletId == id);

            var calcTotalPrice = CalculateTotalPriceBuyOrSell(product.Price, productDto.Quantify);


            switch (status)
            {
                case TransactionStatus.Buy:
                    _extractService.RegisterTransaction(wallet.WalletId, product.Id, TransactionStatus.Buy, productDto.Quantify, calcTotalPrice);
                    CreateBuyProductInWallet(wallet, product, productDto, calcTotalPrice);
                    break;

                case TransactionStatus.Sell:
                    var productSell = _context.WalletProducts.FirstOrDefault(x => x.ProductTitle == productDto.Title);

                    if (productSell == null)
                    {
                        throw new BreakevenException("Não é possível vender um produto que não consta em sua carteira");
                    }

                    var calculatePercentage = CalculatePercentage(product, productSell);

                    var priceSell = CalculateOperationSell(productSell, productDto);

                    productSell.TotalPrice -= priceSell;

                    wallet.InvestedAmount -= priceSell;

                    wallet.FreeAmount += (priceSell) + ((priceSell) * calculatePercentage);
                    wallet.TotalAmount = wallet.InvestedAmount + wallet.FreeAmount;

                    _extractService.RegisterTransaction(wallet.WalletId, product.Id, TransactionStatus.Sell, productDto.Quantify, ((priceSell) * calculatePercentage));

                    RemoveQuantifyProduct(productSell, productDto);

                    _context.SaveChanges();
                    break;
            }

        }

        public Wallet? CalculateProductToWallet(IEnumerable<WalletProduct> products, Wallet walletPersist)
        {
            if (products != null)
            {
                foreach (var product in products)
                {
                    var productPersist = _context.Products.FirstOrDefault(p => p.Title == product.ProductTitle);
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

        private void RemoveQuantifyProduct(WalletProduct productSell, ProductDto productDto)
        {
            productSell.Quantify -= productDto.Quantify;

            if (productSell.Quantify == 0)
            {
                _context.WalletProducts.Remove(productSell);
            }
        }

        private double CalculatePercentage(Product product, WalletProduct productSell)
        {
            var result = ((product.Price - productSell.AverageTicket) / productSell.AverageTicket);

            return result;
        }

        private string FormartAction(string status)
        {
            var text = char.ToUpper(status[0]).ToString();

            for (var i = 1; i < status.Length; i++) {
                text += char.ToLower(status[i]).ToString();
            }

            return text;
        }

        private void CalculateDepositOrWithDrawWallet(Wallet wallet, WalletDto walletDto, TransactionStatus status)
        {
            if (walletDto.Balance <= 0 )
            {
                throw new BreakevenException("Não é possível depositar e/ou sacar valores menores ou iguais a 0 (zero).");
            }

            switch (status) 
            {
                case TransactionStatus.Deposit:
                    wallet.FreeAmount += walletDto.Balance;
                    wallet.TotalAmount = wallet.FreeAmount + wallet.InvestedAmount;
                    break;

                case TransactionStatus.Withdraw:
                    if (wallet.FreeAmount < walletDto.Balance)
                    {
                        throw new BreakevenException("Valor informado para saque é maior do que está disponível.");
                    }

                    wallet.FreeAmount -= walletDto.Balance;
                    wallet.TotalAmount = wallet.FreeAmount + wallet.InvestedAmount;
                    break;
            }
        }

        private void CreateBuyProductInWallet(Wallet wallet, Product product, ProductDto productDto, double calcTotalPrice)
        {
            if (wallet.FreeAmount >= calcTotalPrice && product.Quantify >= productDto.Quantify)
            {
                wallet.FreeAmount -= calcTotalPrice;
                wallet.InvestedAmount += calcTotalPrice;

                WalletProduct walletProducts = new WalletProduct();
                walletProducts.WalletId = wallet.WalletId;
                walletProducts.ProductTitle = product.Title;
                walletProducts.Quantify = productDto.Quantify;
                walletProducts.TotalPrice = calcTotalPrice;
                walletProducts.AverageTicket = product.Price;

                _context.WalletProducts.Add(walletProducts);
                _context.SaveChanges();
            }  
        }
        private double CalculateOperationSell(WalletProduct productSell, ProductDto productDto)
        {
            var result = (double)(productSell.AverageTicket * productDto.Quantify);
            return result;
        }

        private TransactionStatus ConverterStringFromTransactionEnum(string status)
        {
            var formatText = FormartAction(status);

            if (Enum.TryParse(formatText, out TransactionStatus statusEnum))
            {
                return (TransactionStatus)Enum.Parse(typeof(TransactionStatus), formatText);
            }

            throw new BreakevenException("Não é possível converter a Action informada para TransationEnum");
        }
    }
}
