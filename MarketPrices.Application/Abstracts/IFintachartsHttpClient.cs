using MarketPrices.Application.Models.Fintacharts;

namespace MarketPrices.Application.Abstracts
{
    public interface IFintachartsHttpClient
    {
        Task<IEnumerable<Instrument>> GetInstrumentsAsync();
    }
}
