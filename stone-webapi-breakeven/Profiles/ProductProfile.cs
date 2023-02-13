using AutoMapper;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, Product>();
        }
    }
}
