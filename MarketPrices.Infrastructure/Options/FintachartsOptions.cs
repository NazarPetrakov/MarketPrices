namespace MarketPrices.Infrastructure.Options
{
    public class FintachartsOptions
    {
        public const string SectionName = "Fintacharts";
        public string BaseUrl { get; set; } = default!;
    }
}
