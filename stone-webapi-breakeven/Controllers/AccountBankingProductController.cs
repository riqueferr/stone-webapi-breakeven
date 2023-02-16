using Microsoft.AspNetCore.Mvc;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Enums;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Services;

namespace stone_webapi_breakeven.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountBankingProductController : ControllerBase
    {
        private ReadContext _context;

        public AccountBankingProductController(ReadContext context)
        {
            _context = context;
        }

        [HttpPut("{id}/deposit")]
        public IActionResult DepositWallet(int id, [FromBody] WalletDto walletDto, TransactionStatus order)
        {
           var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.WalletId == id);
            TransactionStatus status = TransactionStatus.Deposit;
            if (status == TransactionStatus.Deposit)
            {
                wallet.FreeAmount += walletDto.Balance;
                wallet.TotalAmount += walletDto.Balance;
                _context.SaveChanges();
                return Ok();
            }
            
            if (walletDto == null) { return NotFound(); }

            return Ok();
        }

        [HttpPut("{id}/withdraw")]
        public IActionResult WithDrawWallet(int id, [FromBody] WalletDto walletDto)
        {
            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.WalletId == id);



            if (walletDto == null) { return NotFound(); }

            wallet.Balance -= walletDto.Balance;
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("Wallet/{id}/OrderBuyOrSellProduct")]
        public IActionResult OrderBuyOrSellProduct(int id, [FromBody] ProductDto productDto)
        {
            var product = _context.Products.FirstOrDefault(product => product.Id == productDto.Id);
            var wallet = _context.Wallets.FirstOrDefault(wallet => wallet.WalletId == id);
            
            if (product == null || wallet == null) { return NotFound(); }


            var calcTotalPrice = product.Price * productDto.Quantify;
            if (wallet.FreeAmount >= calcTotalPrice)
            {
                wallet.FreeAmount -= calcTotalPrice;
                wallet.InvestedAmount += calcTotalPrice;
                
                AccountBankingProduct accountBankingProduct = new AccountBankingProduct();
                accountBankingProduct.WalletId = wallet.WalletId;
                accountBankingProduct.ProductId = product.Id;
                accountBankingProduct.Quantify = productDto.Quantify;
                accountBankingProduct.TotalPrice = calcTotalPrice;

                // accountBankingProduct.ActualPrice = product.Price;

                _context.AccountBankingProducts.Add(accountBankingProduct);

                
                _context.SaveChanges();
            }




            return Ok();
        }
    }
}
