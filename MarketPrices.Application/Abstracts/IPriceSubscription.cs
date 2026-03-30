namespace MarketPrices.Application.Abstracts
{
    public interface IPriceSubscription
    {
        Task StartSubscription(Guid instumentId);
        Task StopSubscription(Guid instumentId);
    }
}
