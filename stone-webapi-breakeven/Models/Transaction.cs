using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stone_webapi_breakeven.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        //[Required, ForeignKey("Wallet")]
       // public Wallet Wallet { get; set; }

       // [Required, ForeignKey("Product")]
       // public Product Product { get; set; }
    }
}
