using MediatR;

namespace MarketPrices.Application.Prices.Queries.GetPrice
{
    public record GetPriceQuery(Guid InstrumentId) : IRequest<PriceResponse>
    {
    }
}
