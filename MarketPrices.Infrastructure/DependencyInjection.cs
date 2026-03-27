using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MarketPrices.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<AppDbContext>(conf => conf.UseNpgsql(connectionString));

            return services;
        }
    }
}
