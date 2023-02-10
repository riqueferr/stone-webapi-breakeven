using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stone_webapi_breakeven.Models
{
    public class AccountBanking
    {
        [Key]
        public int Id { get; set; }
        public string Document { get; set; }

        //[ForeignKey("Wallet")]
        //public int? WalletId { get; set; }
        //public virtual Wallet? Wallet { get; set; }

    }
}
