using MarketPrices.Domain.Common.Pagination;
using MarketPrices.Presentation.Common;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MarketPrices.Presentation.Extensions
{
    internal static class HttpExtensions
    {
        public static void AddPaginationHeader<T>(this HttpResponse response, PaginationList<T> data)
        {
            var metaData = new PaginationMetadata(data.PageNumber, data.PageSize, data.TotalCount, data.TotalPages);

            var jsonData = JsonConvert.SerializeObject(metaData);

            response.Headers.Append("Pagination", jsonData);
            response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
