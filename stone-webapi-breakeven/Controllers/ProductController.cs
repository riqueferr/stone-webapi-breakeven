using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Services;

namespace stone_webapi_breakeven.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private ReadContext _context;
        private IMapper _mapper;
        private IProductService _service;

        public ProductController(ReadContext context, IMapper mapper, IProductService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {

           _service.CreateProduct(product);

           return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }


        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {

            var result = _service.GetProductById(id);
            if (result == null) return NotFound();
            
            return Ok(result);
        }


        [HttpGet]
        public IEnumerable<Product> GetProductSkipAndTake([FromQuery] int skipe = 0, [FromQuery] int take = 700)
        {
            return _service.GetProductSkipAndTake(skipe, take);
        }

        [HttpGet("/PriceOrderByDesc")]
        public IEnumerable<Product> PriceOrderByDesc()
        {
            return _service.PriceOrderByDesc();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
        {

            var product = _service.GetProductById(id);

            product.Price = productDto.Price;

            if (product == null) return NotFound();
            _context.SaveChanges();
            return NoContent();
        }

    }
}
