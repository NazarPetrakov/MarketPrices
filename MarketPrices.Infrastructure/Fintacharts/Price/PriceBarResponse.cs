using Newtonsoft.Json;

namespace MarketPrices.Infrastructure.Fintacharts.Price
{
    internal class PriceBarResponse
    {
        public List<RawBarPrice> Data { get; set; } = [];
    }
    internal class RawBarPrice
    {
        [JsonProperty("t")]
        public DateTime Timestamp { get; set; }
        [JsonProperty("o")]
        public decimal Open { get; set; }
        [JsonProperty("h")]
        public decimal High { get; set; }
        [JsonProperty("l")]
        public decimal Low { get; set; }
        [JsonProperty("c")]
        public decimal Close { get; set; }
        [JsonProperty("v")]
        public int Volume { get; set; }
    }
}
