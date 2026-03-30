namespace MarketPrices.Presentation.Common
{
    internal record PaginationMetadata(int CurrentPage, int ItemsPerPage, int TotalItems,
        int TotalPages)
    {
    }
}
