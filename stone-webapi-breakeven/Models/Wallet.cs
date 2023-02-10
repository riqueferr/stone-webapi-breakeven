using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stone_webapi_breakeven.Models
{
    public class Wallet
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public double Balance { get; set; }

        //public List<Product>? Products { get; set; }
    }
}
