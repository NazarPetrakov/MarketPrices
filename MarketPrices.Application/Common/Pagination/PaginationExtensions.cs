using MarketPrices.Domain.Common.Pagination;
using Microsoft.EntityFrameworkCore;

namespace MarketPrices.Application.Common.Pagination
{
    public static class PaginationExtensions
    {
        public static async Task<PaginationList<T>> ToPaginatingListAsync<T>(this IQueryable<T> source,
            int pageNumber, int pageSize,
            CancellationToken ct = default)
        {
            var totalCount = await source.CountAsync(ct);

            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(ct);

            return new PaginationList<T>(items, pageNumber, pageSize, totalCount);
        }

        public static PaginationList<T> ToPaginatingList<T>(this IEnumerable<T> source,
            int pageNumber, int pageSize, int totalCount)
        {
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationList<T>(items, pageNumber, pageSize, totalCount);
        }
    }
}
