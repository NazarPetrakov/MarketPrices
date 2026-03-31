using MarketPrices.Application.Abstracts;
using MarketPrices.Infrastructure.Fintacharts;
using MarketPrices.Infrastructure.Fintacharts.Services;
using MarketPrices.Infrastructure.Fintacharts.WebSockets;
using MarketPrices.Infrastructure.Options;
using MarketPrices.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MarketPrices.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(conf => conf.UseNpgsql(configuration
                .GetConnectionString(ConnectionStrings.DefaultConnection)));

            services.Configure<FintachartsOptions>(configuration.GetSection(FintachartsOptions.SectionName));

            services.AddHttpClient<FintachartsHttpClient>((serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<FintachartsOptions>>().Value;

                client.BaseAddress = new Uri(options.BaseUrl);
            });

            services.AddScoped<IAssetsRepository, AssetsRepository>();
            services.AddScoped<IFintachartsService, FintachartsService>();


            services.AddSingleton<IPriceStore, PriceStore>();
            services.AddSingleton<PriceListener>();
            services.AddSingleton<IPriceSubscription>(sp => sp.GetRequiredService<PriceListener>());
            services.AddHostedService(sp => sp.GetRequiredService<PriceListener>());

            return services;
        }
    }
}
