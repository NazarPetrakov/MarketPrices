using MarketPrices.Application.Assets.Queries.GetAssets;
using MarketPrices.Domain.Common.Pagination;
using MarketPrices.Domain.Entities;

namespace MarketPrices.Application.Abstracts
{
    public interface IAssetsRepository
    {
        Task<List<Asset>> GetAllAsync(CancellationToken ct = default);

        Task<PaginationList<Asset>> GetAllFilteredAsync(AssetFilter filter,
            CancellationToken ct = default);

        Task ReplaceAllAsync(IEnumerable<Asset> assets, CancellationToken ct = default);
    }
}
