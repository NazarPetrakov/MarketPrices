namespace MarketPrices.Domain.Common.Pagination
{
    public class PaginationQueryParams
    {
        private const int _maxPageSize = 1000;

        private int _pageNumber = 1;
        private int _pageSize = 10;

        public int PageNumber
        {
            get => _pageNumber;
            set
            {
                if (value < 1) _pageNumber = 1;
                else _pageNumber = value;
            }
        }
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value < 1) _pageSize = 1;
                else if (value > _maxPageSize) _pageSize = _maxPageSize;
                else _pageSize = value;
            }
        }
    }
}
