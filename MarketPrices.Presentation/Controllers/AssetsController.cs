using MarketPrices.Application.Assets.Queries.GetAssets;
using MarketPrices.Application.Prices.Queries.GetPrice;
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

        [HttpGet("{id}/price")]
        public async Task<IActionResult> GetPrice(Guid id)
        {
            var price = await Sender.Send(new GetPriceQuery(id));

            return Ok(price);
        }
    }
}
