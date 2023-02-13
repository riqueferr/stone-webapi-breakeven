using Microsoft.AspNetCore.Mvc;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
