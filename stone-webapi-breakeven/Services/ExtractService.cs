using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Models;
using System.Transactions;

namespace stone_webapi_breakeven.Services
{
    public class ExtractService : IExtractService
    {

        private ReadContext _context;

        public ExtractService(ReadContext context)
        {
            _context = context;
        }

        public IEnumerable<Extract> GetAll()
        {
            var result = _context.Extracts.ToList();
            return result;
        }

        public IEnumerable<Extract> GetExtractByWalletId(int walletId)
        {
            var result = _context.Extracts.OrderByDescending(x => x.WalletId).ToList();
            return result;
        }

        public void Register(int walletId, int productId, TransactionStatus status, int quantify, double totalPrice)
        {
            Extract extract = new Extract();

            extract.WalletId = walletId;
            extract.ProductId = productId;
            extract.TransactionStatus = (Enums.TransactionStatus) status;
            extract.DateTime = DateTime.UtcNow;
            extract.Quantify = quantify;
            extract.TotalPrice = totalPrice;


            _context.Extracts.Add(extract);
            _context.SaveChanges();
        }
    }
}
