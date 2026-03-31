using MarketPrices.Application.Abstracts;
using MediatR;

namespace MarketPrices.Application.UseCases.Prices.Queries.GetPrice
{
    internal class GetPriceQueryHandler : IRequestHandler<GetPriceQuery, PriceResponse>
    {
        private readonly IPriceStore _priceStore;
        private readonly IPriceSubscription _priceSubscription;
        private readonly IFintachartsService _fintachartsService;

        public GetPriceQueryHandler(IPriceStore priceStore, IPriceSubscription priceSubscription,
            IFintachartsService fintachartsService)
        {
            _priceStore = priceStore;
            _priceSubscription = priceSubscription;
            _fintachartsService = fintachartsService;
        }
        public async Task<PriceResponse> Handle(GetPriceQuery request,
            CancellationToken cancellationToken)
        {
            Guid id = request.AssetId;

            if (!_priceStore.IsTracked(id.ToString()))
                await _priceSubscription.StartSubscription(id);

            var priceInfo = _priceStore.GetPrice(id.ToString());

            if (priceInfo is null)
            {
                //return latest historical price from api if not tracked
                var latestPrice = await _fintachartsService.GetLatestPriceAsync(id);

                return new PriceResponse(id, latestPrice.Open, latestPrice.Timestamp);
            }

            //otherwise return latest price from web socket
            var value = priceInfo.Value;
            return new PriceResponse(id, value.Price, value.Timestamp);
        }
    }
}
