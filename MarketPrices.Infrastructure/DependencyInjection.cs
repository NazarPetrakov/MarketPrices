using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddHttpClient<IFintachartsHttpClient, FintachartsHttpClient>((serviceProvider, client) =>
        {
                var options = serviceProvider.GetRequiredService<IOptions<FintachartsOptions>>().Value;

                client.BaseAddress = new Uri(options.BaseUrl);
            });

            return services;
        }
    }
}
