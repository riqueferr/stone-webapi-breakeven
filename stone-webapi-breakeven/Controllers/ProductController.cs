using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private ReadContext _context;

        public ProductController(ReadContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {

            _context.Products.Add(product);
            _context.SaveChanges();

           return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }


        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {

            var result = _context.Products.FirstOrDefault(product => product.Id == id);
            if (result == null) return NotFound();
            
            return Ok(result);
        }


        [HttpGet]
        public IEnumerable<Product> GetProductSkipAndTake([FromQuery] int skipe = 0, [FromQuery] int take = 700)
        {
            return _context.Products.Skip(skipe).Take(take);
        }

        [HttpGet("/teste")]
        public IEnumerable<Product> Teste([FromQuery] int skipe = 0, [FromQuery] int take = 700)
        {
            return _context.Products.Where(c => c.Price > 1000);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
        {

            var product = _context.Products.FirstOrDefault(
             product => product.Id == id);

            if (product == null) return NotFound();
            _context.SaveChanges();
            return NoContent();
        }

    }
}
