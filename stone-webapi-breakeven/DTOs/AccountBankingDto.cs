namespace stone_webapi_breakeven.DTOs
{
    public class AccountBankingDto
    {

        public string Document { get; set; }
        public string Status { get; set; }

        public DateTime OpentedIn { get; set; }

        public int? WalletId { get; set; }
    }
}
