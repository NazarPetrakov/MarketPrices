using MarketPrices.Domain.Exceptions.Abstract;

namespace MarketPrices.Domain.Exceptions
{
    public class PriceNotFoundException : NotFoundException
    {
        public PriceNotFoundException(Guid instrumentId)
            : base($"Price for asset with ID {instrumentId} was not found, try again later.")
        {
        }
    }
}
