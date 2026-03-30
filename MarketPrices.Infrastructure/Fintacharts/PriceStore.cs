using MarketPrices.Application.Abstracts;
using System.Collections.Concurrent;

namespace MarketPrices.Infrastructure.Fintacharts
{
    internal class PriceStore : IPriceStore
    {
        private readonly ConcurrentDictionary<string, decimal> _prices = new();
        private readonly ConcurrentDictionary<string, DateTime> _lastAccessed = new();

        public decimal? GetPrice(string instrumentId)
        {
            if (_prices.TryGetValue(instrumentId, out decimal price))
            {
                _lastAccessed[instrumentId] = DateTime.UtcNow;
                return price;
            }
            return null;
        }

        public void UpdatePrice(string instrumentId, decimal price)
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
