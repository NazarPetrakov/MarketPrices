using MarketPrices.Infrastructure.Fintacharts.Common;

namespace MarketPrices.Infrastructure.Fintacharts.Instument
{
    public class InstrumentsResponse : PaginationResponse
    {
        public List<InstrumentResponse> Data { get; set; } = [];
    }
    public record InstrumentResponse(
        Guid Id,
        string? Symbol,
        string? Kind,
        string? Description,
        string? Currency,
        string? BaseCurrency,
        int? ContractSize)
    {
    }
}
