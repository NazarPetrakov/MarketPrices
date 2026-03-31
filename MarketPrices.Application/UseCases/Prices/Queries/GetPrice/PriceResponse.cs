namespace MarketPrices.Application.UseCases.Prices.Queries.GetPrice
{
    public record PriceResponse(Guid InstrumentId, decimal Price, DateTime Date)
    {
    }
}
