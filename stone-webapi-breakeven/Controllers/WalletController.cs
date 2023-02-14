using Microsoft.AspNetCore.Mvc;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Services;

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

            return CreatedAtAction(nameof(GetWalletById), new { id = wallet.Id }, wallet);
        }


        [HttpGet("{id}")]
        public IActionResult GetWalletById(int id)
        {

            var result = _context.Wallets.FirstOrDefault(wallet => wallet.Id == id);
            if (result == null) return NotFound();

            return Ok(result);
        }
    }
}
