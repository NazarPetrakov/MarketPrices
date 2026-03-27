using MarketPrices.Infrastructure.Fintacharts.Common;

namespace MarketPrices.Infrastructure.Fintacharts.Instument
{
    public class InstrumentsResponse : PaginationResponse
    {
        public List<InstrumentResponse> Data { get; set; } = [];
    }
    public record InstrumentResponse(string Id, string Symbol, string Description)
    {
    }
}
