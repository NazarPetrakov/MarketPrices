namespace MarketPrices.Application.Prices.Queries.GetPrice
{
    public record PriceResponse(Guid InstrumentId, decimal Price, DateTime Date)
    {
    }
}
