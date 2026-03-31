using MarketPrices.Infrastructure.Fintacharts.Instument;
using MarketPrices.Infrastructure.Fintacharts.Price;
using Newtonsoft.Json;

namespace MarketPrices.Infrastructure.Fintacharts
{
    internal class FintachartsHttpClient
    {
        private readonly HttpClient _client;

        public FintachartsHttpClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<PriceBarResponse> GetCountBackAsync(CountBackQueryParams countBackQueryParams)
        {
            return await GetDeserializedAsync<PriceBarResponse>(
                "/api/bars/v1/bars/count-back" + countBackQueryParams.ToString());
        }

        public async Task<InstrumentsResponse> GetInstrumentsAsync(InstrumentsQueryParams instrumentsQueryParams)
        {
            return await GetDeserializedAsync<InstrumentsResponse>(
                "/api/instruments/v1/instruments" + instrumentsQueryParams.ToString());
        }

        private async Task<T> GetDeserializedAsync<T>(string requestUri)
        {
            var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var desObject = JsonConvert.DeserializeObject<T>(content)
                ?? throw new JsonException("Pagination deserialization returned null.");

            return desObject;
        }
    }
}
