﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Models;
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

        [HttpGet]
        public IEnumerable<AccountBanking> GetAllAccountBanking()
        {
            return _service.GetAllAccountsBanking();
        }

        [HttpGet("{id}")]
        public IActionResult GetAccountBankingById(int id)
        {

            var accountBanking = _service.GetAccountBankingById(id);
            if (accountBanking is null)
            {
                return NotFound();
            }

            return Ok(accountBanking);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccountBanking(int id, [FromBody] AccountBankingDto accountBankingDto)
        {
            var accountBanking = _service.GetAccountBankingById(id);

            if (accountBanking is null)
            {
                return NotFound();
            }

            accountBanking = converterAccountBanking(accountBanking, accountBankingDto);

            _service.UpdateAcconuntBanking(accountBanking);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccountBanking(int id)
        {
            var accountBanking = _service.GetAccountBankingById(id);
            if (accountBanking is null) return NotFound();

            _service.DeleteAccountBanking(id);

            return NoContent();
        }


        [HttpGet("/status")]
        public IEnumerable<AccountBanking> Test([FromQuery] string status)
        {

            return _context.AccountsBanking.Where(accountBanking => accountBanking.Status == Enums.AccountBankingStatus.Active.ToString());
        }

        private AccountBanking converterAccountBanking(AccountBanking accountBanking, AccountBankingDto accountBankingDto)
        {
            if (accountBanking.Document != accountBankingDto.Document)
                accountBanking.Document = accountBankingDto.Document;

            return accountBanking;
        }
    }


}
