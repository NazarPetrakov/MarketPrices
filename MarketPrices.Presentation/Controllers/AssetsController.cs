using MarketPrices.Application.UseCases.Assets.Queries.GetAssets;
using MarketPrices.Application.UseCases.Prices.Queries.GetPrice;
using MarketPrices.Domain.Common.Pagination;
using MarketPrices.Presentation.Common.Assets;
using MarketPrices.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MarketPrices.Presentation.Controllers
{
    [Route("api/assets")]
    public class AssetsController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAssets([FromQuery] AssetsQueryParams assetsParams,
            CancellationToken cancellationToken)
        {
            var query = new GetAssetsQuery(assetsParams.PageNumber, assetsParams.PageSize);

            PaginationList<AssetResponse> assets = await Sender.Send(query, cancellationToken);

            Response.AddPaginationHeader(assets);

            return Ok(assets);
        }

        [HttpGet("{id}/price")]
        public async Task<IActionResult> GetPrice(Guid id)
        {
            PriceResponse price = await Sender.Send(new GetPriceQuery(id));

            return Ok(price);
        }
    }
}
