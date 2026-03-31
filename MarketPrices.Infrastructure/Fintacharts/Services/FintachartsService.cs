using MarketPrices.Application.Abstracts;
using MarketPrices.Application.Models.Fintacharts;
using MarketPrices.Domain.Exceptions;
using MarketPrices.Infrastructure.Fintacharts.Instument;
using MarketPrices.Infrastructure.Fintacharts.Price;

namespace MarketPrices.Infrastructure.Fintacharts.Services
{
    internal class FintachartsService : IFintachartsService
    {
        private readonly FintachartsHttpClient _httpClient;

        public FintachartsService(FintachartsHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BarPrice> GetLatestPriceAsync(Guid instrumentId)
        {
            var queryParams = new CountBackQueryParams(instrumentId);
            var priceResponse = await _httpClient.GetCountBackAsync(queryParams);

            var bar = priceResponse.Data.FirstOrDefault()
                ?? throw new PriceNotFoundException(instrumentId);

            return new BarPrice(bar.Timestamp, bar.Open, bar.High, bar.Low, bar.Close, bar.Volume);
        }

        public async Task<IEnumerable<Instrument>> GetSimulationInstrumentsAsync()
        {
            string provider = "simulation";

            var totalInstrumentsCount = await GetInstrumentsCountAsync(provider);

            var queryParams = new InstrumentsQueryParams(1, totalInstrumentsCount, provider);

            var instrumentsResponse = await _httpClient.GetInstrumentsAsync(queryParams);

            return instrumentsResponse.Data
                .Select(i => new Instrument(i.Id, i.Symbol ?? "", i.Description ?? ""));
        }

        private async Task<int> GetInstrumentsCountAsync(string provider)
        {
            var queryParams = new InstrumentsQueryParams(1, 1, provider);

            var a = await _httpClient.GetInstrumentsAsync(queryParams);

            return a.Paging.Items;
        }
    }
}
