using Microsoft.AspNetCore.Mvc;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Services;

namespace stone_webapi_breakeven.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExtractController
    {

        private ReadContext _context;
        private IExtractService _service;
        public ExtractController(ReadContext context, IExtractService service)
        {
            _context = context;
            _service = service;
        }




        [HttpGet("{id}")]
        public IEnumerable<Extract> GetExtractByWalletId(int walletId)
        {
            IEnumerable<Extract> result;
            if (walletId == null)
            {
                result = _service.GetAll();
            } else
            {
                result = _service.GetExtractByWalletId(walletId);
            }

            return result;
        }
    }
}
