using MediatR;

namespace MarketPrices.Application.UseCases.Prices.Queries.GetPrice
{
    public record GetPriceQuery(Guid AssetId) : IRequest<PriceResponse>
    {
    }
}
