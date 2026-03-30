using MarketPrices.Application.Assets.Queries.GetAssets;
using MarketPrices.Domain.Common.Assets;
using MarketPrices.Domain.Common.Pagination;
using MarketPrices.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MarketPrices.Presentation.Controllers
{
    [Route("api/assets")]
    public class AssetsController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAssets([FromQuery] AssetsQueryParams assetsQueryParams,
            CancellationToken cancellationToken)
        {
            var query = new GetAssetsQuery(assetsQueryParams);

            PaginationList<AssetResponse> assets = await Sender.Send(query, cancellationToken);

            Response.AddPaginationHeader(assets);

            return Ok(assets);
        }


    }
}
