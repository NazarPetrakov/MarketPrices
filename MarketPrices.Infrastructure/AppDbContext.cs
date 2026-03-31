using MarketPrices.Domain.Entities;
using MarketPrices.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MarketPrices.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Asset> Assets => Set<Asset>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssetConfiguration).Assembly);
        }
    }
}
