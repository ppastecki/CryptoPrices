using CryptoPrices.Core.Data.Configuration;
using CryptoPrices.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoPrices.Core.Data
{
    public class CryptoPricesContext : DbContext
    {
        public DbSet<CryptoCurrency> Cryptocurrencies { get; set; }

        public DbSet<Quote> Quotes { get; set; }

        public CryptoPricesContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CryptocurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new QuoteConfiguration());
        }
    }
}
