namespace MarketPrices.Infrastructure.Fintacharts.Instument
{
    public record InstrumentsQueryParams(int? Page, int? Size)
    {
        public override string ToString()
        {
            var query = new List<string>();

            if (Page.HasValue) query.Add($"page={Page.Value}");
            if (Size.HasValue) query.Add($"size={Size.Value}");

            return query.Count > 0 ? "?" + string.Join("&", query) : string.Empty;

        }
    }
}
