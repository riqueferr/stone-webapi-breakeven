using Microsoft.EntityFrameworkCore;
using stone_webapi_breakeven.Models;

namespace stone_webapi_breakeven.Data
{
    public class ReadContext :DbContext
    {

        public ReadContext(DbContextOptions<ReadContext> options) : base(options) { }
        public DbSet<AccountBanking> AccountsBanking { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Transaction> Transactions { get; set; }

    }
}
