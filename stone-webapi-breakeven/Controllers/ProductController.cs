using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private ReadContext _context;
        private WalletController _walletController;

        public ProductController(ReadContext context, WalletController walletController)
        {
            _context = context;
            _walletController = walletController;
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {

            _context.Products.Add(product);
            _context.SaveChanges();

           return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        //[HttpGet]
        //public IEnumerable<AccountBanking> GetAccountBankingAll(AccountBanking accountBanking)
        //{
           // return accountsBanking;
       // }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {

            var result = _context.Products.FirstOrDefault(product => product.Id == id);
            if (result == null) return NotFound();
            
            return Ok(result);
        }


        [HttpGet]
        public IEnumerable<AccountBanking> GetAccountBankingSkipAndTake([FromQuery] int skipe = 0, [FromQuery] int take = 700)
        {
            return _context.AccountsBanking.Skip(skipe).Take(take);
        }

    }
}
