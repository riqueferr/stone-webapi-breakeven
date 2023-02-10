using Microsoft.AspNetCore.Mvc;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Controllers
{
    public class WalletController : ControllerBase
    {

        private ReadContext _context;


        public WalletController(ReadContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GeWalletById), new { id = wallet.Id }, wallet);
        }


        [HttpGet("{id}")]
        public IActionResult GeWalletById(int id)
        {

            var result = _context.AccountsBanking.FirstOrDefault(accountBanking => accountBanking.Id == id);
            if (result == null) return NotFound();

            return Ok(result);
        }
    }
}
