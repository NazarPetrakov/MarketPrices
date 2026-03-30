using MarketPrices.Application.Abstracts;
using MediatR;

namespace MarketPrices.Application.Prices.Queries.GetPrice
{
    internal class GetPriceQueryHandler : IRequestHandler<GetPriceQuery, PriceResponse>
    {
        private readonly IPriceStore _priceStore;
        private readonly IPriceSubscription _priceSubscription;

        public GetPriceQueryHandler(IPriceStore priceStore, IPriceSubscription priceSubscription)
        {
            _priceStore = priceStore;
            _priceSubscription = priceSubscription;
        }
        public async Task<PriceResponse> Handle(GetPriceQuery request,
            CancellationToken cancellationToken)
        {
            Guid id = request.InstrumentId;

            if (!_priceStore.IsTracked(id.ToString()))
                await _priceSubscription.StartSubscription(id);

            var price = _priceStore.GetPrice(id.ToString());

            if (price is null)
            {
                //TODO: return historical price from API
                return new PriceResponse(id, 0);
            }

            return new PriceResponse(id, price.Value);
        }
    }
}
