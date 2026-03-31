using MarketPrices.Application.Abstracts;
using MarketPrices.Domain.Entities;
using MediatR;

namespace MarketPrices.Application.UseCases.Assets.Commands.SeedAssets
{
    internal class SeedAssetsCommandHandler : IRequestHandler<SeedAssetsCommand>
    {
        private readonly IFintachartsService _fintachartsService;
        private readonly IAssetsRepository _repository;

        public SeedAssetsCommandHandler(IFintachartsService fintachartsService, IAssetsRepository repository)
        {
            _fintachartsService = fintachartsService;
            _repository = repository;
        }

        public async Task Handle(SeedAssetsCommand request, CancellationToken cancellationToken)
        {
            var instruments = await _fintachartsService.GetSimulationInstrumentsAsync();

            var assets = instruments.Select(i => new Asset(i.Id, i.Symbol, i.Description));

            await _repository.ReplaceAllAsync(assets, cancellationToken);
        }
    }
}
