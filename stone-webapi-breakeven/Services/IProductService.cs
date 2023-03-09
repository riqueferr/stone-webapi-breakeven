using Microsoft.AspNetCore.Mvc;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Services
{
    public interface IProductService
    {
        int CreateProduct(Product product);
        Product GetProductById(int id);
        IEnumerable<Product> GetProductSkipAndTake(int skip, int take);
        IEnumerable<Product> PriceOrderByDesc();
        void ConverterProduct(Product product, ProductDto productDto);
        Product GetProductByTitle(string? title);
    }
}
