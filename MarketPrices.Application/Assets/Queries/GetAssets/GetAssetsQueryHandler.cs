using MarketPrices.Application.Abstracts;
using MarketPrices.Application.Common.Pagination;
using MarketPrices.Domain.Common.Pagination;
using MediatR;

namespace MarketPrices.Application.Assets.Queries.GetAssets
{
    internal class GetAssetsQueryHandler : IRequestHandler<GetAssetsQuery, PaginationList<AssetResponse>>
    {
        private readonly IAssetsRepository _assetsRepository;

        public GetAssetsQueryHandler(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository;
        }

        public async Task<PaginationList<AssetResponse>> Handle(GetAssetsQuery request,
            CancellationToken cancellationToken)
        {
            var filter = new AssetFilter(request.PageNumber, request.PageSize);

            var assets = await _assetsRepository.GetAllFilteredAsync(filter, cancellationToken);

            return assets.Select(a => new AssetResponse(a.Id, a.Symbol, a.Description))
                .ToPaginatingList(assets.PageNumber, assets.PageSize, assets.TotalCount);
        }
    }
}
