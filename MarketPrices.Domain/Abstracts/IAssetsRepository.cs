using MarketPrices.Domain.Common.Assets;
using MarketPrices.Domain.Common.Pagination;
using MarketPrices.Domain.Entities;

namespace MarketPrices.Domain.Abstracts
{
    public interface IAssetsRepository
    {
        Task<List<Asset>> GetAllAsync(CancellationToken ct = default);

        Task<PaginationList<Asset>> GetAllFilteredAsync(AssetsQueryParams queryParams,
            CancellationToken ct = default);

        Task ReplaceAllAsync(IEnumerable<Asset> assets, CancellationToken ct = default);
    }
}
