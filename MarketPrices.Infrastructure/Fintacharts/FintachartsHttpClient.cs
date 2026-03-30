using MarketPrices.Infrastructure.Fintacharts.Common;
using MarketPrices.Infrastructure.Fintacharts.Instument;
using Newtonsoft.Json;

namespace MarketPrices.Infrastructure.Fintacharts
{
    public class FintachartsHttpClient : IFintachartsHttpClient
    {
        private readonly HttpClient _client;

        public FintachartsHttpClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<InstrumentsResponse> GetAllInstrumentsAsync()
        public async Task<IEnumerable<Instrument>> GetInstrumentsAsync()
        {
            var totalInstrumentsCount = await GetInstrumentsCountAsync();

            var queryParams = new InstrumentsQueryParams(1, totalInstrumentsCount, "simulation");

            var instrumentsResponse = await GetDeserializedAsync<InstrumentsResponse>(
                "/api/instruments/v1/instruments" + queryParams.ToString());

            var instruments = instrumentsResponse.Data.Select(i => new Instrument(
                i.Id,
                i.Symbol ?? "",
                i.Description ?? ""
            ));

            return instruments;
        }

        private async Task<int> GetInstrumentsCountAsync()
        {
            var queryParams = new InstrumentsQueryParams(1, 1);

            var pagination = await GetDeserializedAsync<PaginationResponse>(
                "/api/instruments/v1/instruments" + queryParams.ToString());

            return pagination.Paging.Items;
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
