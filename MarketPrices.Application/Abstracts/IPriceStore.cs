using MarketPrices.Application.Models.Fintacharts;

namespace MarketPrices.Application.Abstracts
{
    public interface IPriceStore
    {
        PriceInfo? GetPrice(string instrumentId);
        void UpdatePrice(string instrumentId, PriceInfo price);
        void Touch(string instrumentId);
        bool IsTracked(string instrumentId);
        IEnumerable<string> GetStale(TimeSpan ttl);
        void Remove(string instrumentId);
    }
}
