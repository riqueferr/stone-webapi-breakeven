using Microsoft.AspNetCore.Mvc;
using stone_webapi_breakeven.Data;
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
           Wallet wallet =  _walletService.CreateWallet();

            return CreatedAtAction(nameof(GetWalletById), new { id = wallet.WalletId }, wallet);
        }


        [HttpGet("{id}")]
        public IActionResult GetWalletById(int id)
        {

            var result = _context.Wallets.FirstOrDefault(wallet => wallet.WalletId == id);
            var fkResult = _context.AccountBankingProducts.Where(fkResult => fkResult.WalletId == id).ToList();
            if (result == null) return NotFound();

            if (fkResult != null)
            {
                foreach (var product in fkResult)
                {
                    result.Products.Add(product);
                }
            }

            return Ok(result);
        }
    }
}
