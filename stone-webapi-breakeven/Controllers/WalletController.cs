using Microsoft.AspNetCore.Mvc;
using stone_webapi_breakeven.Conveters;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Enums;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Services;
using System.Xml.Linq;

namespace stone_webapi_breakeven.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {

        private ReadContext _context;
        private IWalletService _walletService;
        //private AccountBankingProductConverter _accountBankingProductConverter;


        public WalletController(ReadContext context, IWalletService walletService)
        {
            _context = context;
            _walletService = walletService;
            //_accountBankingProductConverter = accountBankingProductConverter;
        }

        [HttpPost]
        public IActionResult CreateWallet()
        {
            Wallet wallet = _walletService.CreateWallet();

            return CreatedAtAction(nameof(GetWalletById), new { id = wallet.WalletId }, wallet);
        }


        [HttpGet("{id}")]
        public IActionResult GetWalletById(int id)
        {
            var result = _walletService.GetWalletById(id);
            var products = _walletService.GetWalletByIdAndProductsDetails(id);

            if (result == null) return NotFound();

            if (products != null)
            {
                foreach (var product in products)
                {
                    var p = _context.Products.FirstOrDefault(p => p.Id == product.ProductId);
                    product.Percentage = ((p.Price - product.AverageTicket) / p.Price);
                    result.Products.Add(product);
                }
            }

            return Ok(result);
        }

        [HttpPut("{id}/DepositOrWithdraw")]
        public IActionResult DepositOrWithdrawWallet(int id, [FromBody] WalletDto walletDto)
        {
            var result = _walletService.DepositOrWithdrawWallet(id, walletDto);
            if (walletDto == null || !result) { return NotFound(); }

            return Ok();

            
        }

        [HttpPost("{id}/OrderBuyOrSellProduct")]
        public IActionResult OrderBuyOrSellProduct(int id, [FromBody] ProductDto productDto)
        {

            _walletService.OrderBuyOrSellProduct(id, productDto);
            /*var product = _context.Products.FirstOrDefault(product => product.Id == productDto.Id);
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
            }*/

            return Ok();
        }
    }

}
