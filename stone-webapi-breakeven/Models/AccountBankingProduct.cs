using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stone_webapi_breakeven.Models
{
    public class AccountBankingProduct
    {
        [Key]
        public int AccountBankingProductId { get; set; }

        public int WalletId { get; set; }
        public int ProductId { get; set; }
        public int Quantify { get; set; }
        public double TotalPrice { get; set; }
        public double Percentage { get; set; }
        public double AverageTicket { get; set; }
    }
}
