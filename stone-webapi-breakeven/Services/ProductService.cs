using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Exceptions;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Services
{
    public class ProductService : IProductService
    {
        private ReadContext _context;

        public ProductService(ReadContext context)
        {
            _context = context;
        }

        public void ConverterProduct(Product product, ProductDto productDto)
        {
            if (productDto.Price != null)
            {
                product.Price = productDto.Price;
            }
            _context.SaveChanges();
        }

        public int CreateProduct(Product product)
        {
            if(product != null)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return product.Id;
            } 

            throw new BreakevenException("O produto não foi criado");
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(product => product.Id == id);
        }

        public Product GetProductByTitle(string? title)
        {
            return _context.Products.FirstOrDefault(product => product.Title == title);
        }

        public IEnumerable<Product> GetProductSkipAndTake(int skip, int take)
        {
            return _context.Products.Skip(skip).Take(take);
        }

        public IEnumerable<Product> PriceOrderByDesc()
        {
            return _context.Products.OrderByDescending(x => x.Price);
        }
    }
}
