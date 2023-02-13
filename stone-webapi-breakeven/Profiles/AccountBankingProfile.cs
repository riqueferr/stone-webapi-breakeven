using AutoMapper;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Profiles
{
    public class AccountBankingProfile : Profile
    {
        public AccountBankingProfile()
        {
            CreateMap<AccountBankingDto, AccountBanking>();
        }
    }
}
