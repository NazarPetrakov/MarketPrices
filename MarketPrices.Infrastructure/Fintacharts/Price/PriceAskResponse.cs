namespace MarketPrices.Infrastructure.Fintacharts.Price
{
    internal class PriceAskResponse
    {
        public string? Type { get; set; } = null;
        public Guid InstrumentId { get; set; }
        public string? Provider { get; set; } = null;
        public bool Subscribe { get; set; }
        public AskPriceInfo? Ask { get; set; }
    }
    internal class AskPriceInfo
    {
        public DateTime Timestamp { get; set; }
        public decimal Price { get; set; }
        public int Volume { get; set; }
    }
}
