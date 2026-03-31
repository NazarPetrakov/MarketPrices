namespace MarketPrices.Application.Models.Fintacharts
{
    public struct PriceInfo
    {
        public decimal Price { get; set; }
        public DateTime Timestamp { get; set; }

        public PriceInfo(decimal price, DateTime timestamps)
        {
            Price = price;
            Timestamp = timestamps;
        }
    }
}
