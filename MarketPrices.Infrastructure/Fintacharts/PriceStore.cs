using MarketPrices.Application.Abstracts;
using MarketPrices.Application.Models.Fintacharts;
using System.Collections.Concurrent;

namespace MarketPrices.Infrastructure.Fintacharts
{
    internal class PriceStore : IPriceStore
    {
        private readonly ConcurrentDictionary<string, PriceInfo> _prices = new();
        private readonly ConcurrentDictionary<string, DateTime> _lastAccessed = new();

        public PriceInfo? GetPrice(string instrumentId)
        {
            if (_prices.TryGetValue(instrumentId, out PriceInfo price))
            {
                _lastAccessed[instrumentId] = DateTime.UtcNow;
                return price;
            }
            return null;
        }

        public void UpdatePrice(string instrumentId, PriceInfo price)
        {
            _prices[instrumentId] = price;
        }

        public void Touch(string instrumentId)
        {
            _lastAccessed[instrumentId] = DateTime.UtcNow;
        }

        public bool IsTracked(string instrumentId)
            => _lastAccessed.ContainsKey(instrumentId);

        public IEnumerable<string> GetStale(TimeSpan ttl)
            => _lastAccessed
                .Where(kv => DateTime.UtcNow - kv.Value > ttl)
                .Select(kv => kv.Key);

        public void Remove(string instrumentId)
        {
            _prices.TryRemove(instrumentId, out _);
            _lastAccessed.TryRemove(instrumentId, out _);
        }
    }
}
