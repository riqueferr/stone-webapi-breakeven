using stone_webapi_breakeven.Enums;
using System.ComponentModel.DataAnnotations;

namespace stone_webapi_breakeven.Models
{
    public class Extract
    {
        [Key]
        public int ExtractId { get; set; }
        public int WalletId { get; set; }
        public int? ProductId { get; set; }
        public string TransactionStatus { get; set; }
        public int? Quantify { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime DateTime { get; set; }
    }
}
