using MarketPrices.Application.Common.Pagination;
using MarketPrices.Domain.Abstracts;
using MarketPrices.Domain.Common.Assets;
using MarketPrices.Domain.Common.Pagination;
using MarketPrices.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketPrices.Infrastructure.Repositories
{
    internal class AssetsRepository : IAssetsRepository
    {
        private readonly AppDbContext _dbContext;

        public AssetsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginationList<Asset>> GetAllFilteredAsync(AssetsQueryParams queryParams, CancellationToken ct = default)
        {
            var assets = _dbContext.Assets.AsQueryable();

            return await assets.ToPaginatingListAsync(queryParams.PageNumber, queryParams.PageSize, ct);
        }

        public async Task<List<Asset>> GetAllAsync(CancellationToken ct = default)
        {
            return await _dbContext.Assets.ToListAsync(ct);
        }

        public async Task ReplaceAllAsync(IEnumerable<Asset> assets, CancellationToken ct = default)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);

            try
            {
                await _dbContext.Assets.ExecuteDeleteAsync(ct);

                _dbContext.Assets.AddRange(assets);

                await _dbContext.SaveChangesAsync(ct);

                await transaction.CommitAsync(ct);
            }
            catch
            {
                await transaction.RollbackAsync(ct);
                throw;
            }
        }
    }
}
