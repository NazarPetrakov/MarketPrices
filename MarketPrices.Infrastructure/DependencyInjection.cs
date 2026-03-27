using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MarketPrices.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, string? connectionString)
            services.Configure<FintachartsOptions>(configuration.GetSection(FintachartsOptions.SectionName));

            services.AddHttpClient<FintachartsHttpClient>(client =>
        {
                client.BaseAddress = new Uri(configuration.GetSection(FintachartsOptions.SectionName)
                    .Get<FintachartsOptions>()?.BaseUrl ?? "https://platform.fintacharts.com");
            });

            return services;
        }
    }
}
