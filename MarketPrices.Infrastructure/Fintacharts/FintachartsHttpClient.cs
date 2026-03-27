using MarketPrices.Infrastructure.Fintacharts.Common;
using MarketPrices.Infrastructure.Fintacharts.Instument;
using Newtonsoft.Json;

namespace MarketPrices.Infrastructure.Fintacharts
{
    public class FintachartsHttpClient
    {
        private readonly HttpClient _client;

        public FintachartsHttpClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<InstrumentsResponse> GetAllInstrumentsAsync()
        {
            var totalInstrumentsCount = await GetInstrumentsCountAsync();

            var queryParams = new InstrumentsQueryParams(1, totalInstrumentsCount);

            var response = await _client.GetAsync("/api/instruments/v1/instruments" + queryParams.ToString());
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var instruments = JsonConvert.DeserializeObject<InstrumentsResponse>(content)
                ?? throw new JsonException("Instruments deserialization returned null.");

            return instruments;
        }

        private async Task<int> GetInstrumentsCountAsync()
        {
            var queryParams = new InstrumentsQueryParams(1, 1);

            var response = await _client.GetAsync("/api/instruments/v1/instruments" + queryParams.ToString());
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var pagination = JsonConvert.DeserializeObject<PaginationResponse>(content)
                ?? throw new JsonException("Pagination deserialization returned null.");

            return pagination.Paging.Items;
        }
    }
}
