using MarketPrices.Application.Abstracts;
using MarketPrices.Domain.Abstracts;
using MarketPrices.Domain.Entities;
using MediatR;

namespace MarketPrices.Application.Assets.Commands.SeedAssets
{
    internal class SeedAssetsCommandHandler : IRequestHandler<SeedAssetsCommand>
    {
        private readonly IFintachartsHttpClient _htppClient;
        private readonly IAssetsRepository _repository;

        public SeedAssetsCommandHandler(IFintachartsHttpClient htppClient, IAssetsRepository repository)
        {
            _htppClient = htppClient;
            _repository = repository;
        }

        public async Task Handle(SeedAssetsCommand request, CancellationToken cancellationToken)
        {
            var instruments = await _htppClient.GetInstrumentsAsync();

            var assets = instruments.Select(i => new Asset(i.Id, i.Symbol, i.Description));

            await _repository.ReplaceAllAsync(assets, cancellationToken);
        }
    }
}
