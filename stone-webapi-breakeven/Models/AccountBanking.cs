using stone_webapi_breakeven.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stone_webapi_breakeven.Models
{
    public class AccountBanking
    {
        [Key]
        public int AccountBankingId { get; set; }
        public string Document { get; set; }

        public AccountBankingStatus Status { get; set; }

        public DateTime OpentedIn { get; set; }


        public int WalletId { get; set; }

        [ForeignKey("WalletId")]
        public virtual Wallet Wallet { get; set; }

    }
}
