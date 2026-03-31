using MarketPrices.Domain.Common.Pagination;
using MediatR;

namespace MarketPrices.Application.UseCases.Assets.Queries.GetAssets
{
    public record GetAssetsQuery(int PageNumber, int PageSize) : IRequest<PaginationList<AssetResponse>>
    {
    }
}
