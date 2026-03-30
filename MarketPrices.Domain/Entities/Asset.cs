namespace MarketPrices.Domain.Entities
{
    public class Asset
    {
        public Guid Id { get; private set; }
        public string Symbol { get; private set; }
        public string Description { get; private set; }

        private Asset()
        {
            Symbol = string.Empty;
            Description = string.Empty;
        }

        public Asset(Guid id, string symbol, string description)
        {
            Id = id;
            Symbol = symbol;
            Description = description;
        }
    }
}
