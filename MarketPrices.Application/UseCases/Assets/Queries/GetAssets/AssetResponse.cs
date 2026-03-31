namespace MarketPrices.Application.UseCases.Assets.Queries.GetAssets
{
    public record AssetResponse(Guid Id, string Symbol, string Description)
    {
    }
}
