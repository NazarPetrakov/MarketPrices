namespace MarketPrices.Application.Abstracts
{
    public interface IPriceStore
    {
        decimal? GetPrice(string instrumentId);
        void UpdatePrice(string instrumentId, decimal price);
        void Touch(string instrumentId);
        bool IsTracked(string instrumentId);
        IEnumerable<string> GetStale(TimeSpan ttl);
        void Remove(string instrumentId);
    }
}
