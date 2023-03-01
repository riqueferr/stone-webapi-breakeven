using Microsoft.AspNetCore.Mvc;
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


        public WalletController(ReadContext context, IWalletService walletService)
        {
            _context = context;
            _walletService = walletService;
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
            var walletPersist = _walletService.GetWalletById(id);
            var products = _walletService.GetWalletByIdAndProductsDetails(id);

            if (walletPersist is null)
            {
                return NotFound();
            }

            _walletService.CalculateProductToWallet(products, walletPersist);

            return Ok(walletPersist);
        }

        [HttpPut("{id}/DepositOrWithdraw")]
        public IActionResult DepositOrWithdrawWallet(int id, [FromBody] WalletDto walletDto)
        {
            _walletService.DepositOrWithdrawWallet(id, walletDto);
            if (walletDto is null) 
            {
                return NotFound(); 
            }

            return Ok();

            
        }

        [HttpPost("{id}/OrderBuyOrSellProduct")]
        public IActionResult OrderBuyOrSellProduct(int id, [FromBody] ProductDto productDto)
        {

            _walletService.OrderBuyOrSellProduct(id, productDto);

            return Ok();
        }
    }

}
