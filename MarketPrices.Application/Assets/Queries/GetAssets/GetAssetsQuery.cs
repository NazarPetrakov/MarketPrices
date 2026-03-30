using MarketPrices.Domain.Common.Assets;
using MarketPrices.Domain.Common.Pagination;
using MediatR;

namespace MarketPrices.Application.Assets.Queries.GetAssets
{
    public record GetAssetsQuery(AssetsQueryParams AssetsQueryParams) : IRequest<PaginationList<AssetResponse>>
    {
    }
}
