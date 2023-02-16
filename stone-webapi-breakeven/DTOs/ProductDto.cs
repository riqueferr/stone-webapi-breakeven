namespace stone_webapi_breakeven.DTOs
{
    public class ProductDto
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public double? Price { get; set; }

        public double? AverageTicket { get; set; }

        public double? TotalPrice { get; set; }

        public string? Type { get; set; }

        public int? PercentageEvolution { get; set; }

        public int Quantify { get;set; }
    }
}
