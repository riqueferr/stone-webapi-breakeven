using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Services
{
    public interface IExtractService
    {
        IEnumerable<Extract> GetAll();
        IEnumerable<Extract> GetExtractByWalletId(int walletId);
    }
}
