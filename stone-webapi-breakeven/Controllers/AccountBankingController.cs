using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Services;

namespace stone_webapi_breakeven.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountBankingController : Controller
    {

        private readonly ReadContext _context;
        private readonly IAccountBankingService _service;

        public AccountBankingController(ReadContext context)
        {

            _context = context;
            _service = new AccountBankingService(context);
        }

        [HttpPost]
        public IActionResult CreateAccountBanking([FromBody] AccountBanking accountBanking)
        {
            Console.WriteLine(accountBanking.Document);
            _service.CreateAccountBanking(accountBanking);

           return CreatedAtAction(nameof(GetAccountBankingById), new { id = accountBanking.Id }, accountBanking);

        }

        //[HttpGet]
        //public IEnumerable<AccountBanking> GetAccountBankingAll(AccountBanking accountBanking)
        //{
           // return accountsBanking;
       // }

        [HttpGet("{id}")]
        public IActionResult GetAccountBankingById(int id)
        {

            var result = _context.AccountsBanking.FirstOrDefault(accountBanking => accountBanking.Id == id);
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
