using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Profiles;
using stone_webapi_breakeven.Services;

namespace stone_webapi_breakeven.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountBankingController : ControllerBase
    {

        private ReadContext _context;
        private IAccountBankingService _service;
        private IMapper _mapper;

        public AccountBankingController(ReadContext context, IMapper mapper, IAccountBankingService service)
        {
            _context = context;
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateAccountBanking([FromBody] AccountBankingDto accountBankingDto)
        {
            AccountBanking accountBanking = _mapper.Map<AccountBanking>(accountBankingDto); 
            _service.CreateAccountBanking(accountBanking);

           return CreatedAtAction(nameof(GetAccountBankingById), new { id = accountBanking.AccountBankingId }, accountBanking);

        }


        [HttpGet("{id}")]
        public IActionResult GetAccountBankingById(int id)
        {

            var result = _context.AccountsBanking.FirstOrDefault(accountBanking => accountBanking.AccountBankingId == id);
            if (result == null) return NotFound();
            
            return Ok(result);
        }


        [HttpGet]
        public IEnumerable<AccountBanking> GetAccountBankingSkipAndTake([FromQuery] int skipe = 0, [FromQuery] int take = 700)
        {
            return _context.AccountsBanking.Skip(skipe).Take(take);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccountBanking(int id, [FromBody] AccountBankingDto accountBankingDto)
        {
            var accountBanking = _context.AccountsBanking.FirstOrDefault(
                accountBanking => accountBanking.AccountBankingId == id);

            if(accountBanking == null) return NotFound();

            _mapper.Map(accountBankingDto, accountBanking);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccountBanking(int id)
        {
            var accountBanking = _context.AccountsBanking.FirstOrDefault(accountBanking => accountBanking.AccountBankingId == id);
            if (accountBanking== null) return NotFound();

            _context.Remove(accountBanking);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpGet("test")]
        public IEnumerable<AccountBanking> Test([FromQuery] string status)
        {
            return _context.AccountsBanking.Where(accountBanking => accountBanking.Status == Enums.AccountBankingStatus.Active);
        }

    }
}
