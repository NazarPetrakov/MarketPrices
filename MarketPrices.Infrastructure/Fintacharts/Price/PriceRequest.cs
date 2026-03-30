namespace MarketPrices.Infrastructure.Fintacharts.Price
{
    internal class PriceRequest
    {
        public PriceRequest(Guid instrumentId, bool subscribe)
        {
            InstrumentId = instrumentId;
            Subscribe = subscribe;
        }

        public string Type { get; set; } = "l1-subscription";
        public string Id { get; set; } = "1";
        public Guid InstrumentId { get; set; }
        public string Provider { get; set; } = "simulation";
        public bool Subscribe { get; set; } = true;
        public string[] Kinds { get; set; } = ["ask"];
    }
}
