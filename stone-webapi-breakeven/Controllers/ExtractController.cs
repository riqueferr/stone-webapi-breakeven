using Microsoft.AspNetCore.Mvc;
using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.Models;
using stone_webapi_breakeven.Services;

namespace stone_webapi_breakeven.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExtractController : ControllerBase
    {

        private readonly IExtractService _service;
        public ExtractController(IExtractService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IEnumerable<Extract> GetExtractByWalletId(int walletId)
        {
            return _service.GetExtractByWalletId(walletId);
        }
    }
}
