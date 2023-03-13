using stone_webapi_breakeven.Data;
using stone_webapi_breakeven.DTOs;
using stone_webapi_breakeven.Enums;
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

        public IEnumerable<Extract> GetAllExtract()
        {
            return _context.Extracts.ToList();
        }

        public IEnumerable<Extract> GetExtractByWalletId(int walletId)
        {
            return _context.Extracts.Where(x => x.WalletId == walletId);
        }

        public void RegisterTransaction(int walletId, int? productId, Enums.TransactionStatus status, int? quantify, double? totalPrice)
        {
            Extract extract = new Extract();

            extract.WalletId = walletId;
            extract.ProductId = productId;
            extract.TransactionStatus =  status.ToString();
            extract.DateTime = DateTime.UtcNow;
            extract.Quantify = quantify;
            extract.TotalPrice = totalPrice;


            _context.Extracts.Add(extract);
            _context.SaveChanges();
        }

    }
}
