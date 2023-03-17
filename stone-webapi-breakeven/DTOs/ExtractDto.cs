using stone_webapi_breakeven.Enums;

namespace stone_webapi_breakeven.DTOs
{
    public class ExtractDto
    {
        public int AccountBankingId { get; set; }
        public int WalletId { get; set; }
        public int ProductId { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public int? Quantify { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime DateTime { get; set; }
    }
}
