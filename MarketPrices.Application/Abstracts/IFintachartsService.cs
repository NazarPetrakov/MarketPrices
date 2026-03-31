using MarketPrices.Application.Models.Fintacharts;

namespace MarketPrices.Application.Abstracts
{
    public interface IFintachartsService
    {
        Task<IEnumerable<Instrument>> GetSimulationInstrumentsAsync();
        Task<BarPrice> GetLatestPriceAsync(Guid instrumentId);
    }
}
