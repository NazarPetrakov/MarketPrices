namespace MarketPrices.Application.Models.Fintacharts
{
    public record BarPrice(
        DateTime Timestamp,
        decimal Open,
        decimal High,
        decimal Low,
        decimal Close,
        int Volume)
    {
    }
}
