namespace MarketPrices.Infrastructure.Fintacharts.Price
{
    public record CountBackQueryParams(
        Guid InstrumentId,
        string Provider = "simulation",
        int Interval = 1,
        string Periodicity = "minute",
        int BarsCount = 1)
    {
        public override string ToString()
        {
            var query = new List<string>
            {
                $"instrumentId={InstrumentId}",
                $"provider={Provider}",
                $"interval={Interval}",
                $"periodicity={Periodicity}",
                $"barsCount={BarsCount}"
            };

            return query.Count > 0 ? "?" + string.Join("&", query) : string.Empty;
        }
    }
}
