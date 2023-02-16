using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stone_webapi_breakeven.Models
{
    public class Wallet
    {
        [Key]
        [Required]
        public int WalletId { get; set; }
        public double Balance { get; set; }
        public double? InvestedAmount { get; set; }
        public double? FreeAmount { get; set; }
        public double? TotalAmount { get; set; }
        public ICollection<AccountBankingProduct>? Products { get; set; }
    }
}
