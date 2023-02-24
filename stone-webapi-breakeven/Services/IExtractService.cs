using stone_webapi_breakeven.Enums;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Services
{
    public interface IExtractService
    {
        IEnumerable<Extract> GetAll();
        IEnumerable<Extract> GetExtractByWalletId(int walletId);

        void RegisterTransaction(int walletId, int? productId, TransactionStatus status, int? quantify, double? totalPrice);
    }
}
