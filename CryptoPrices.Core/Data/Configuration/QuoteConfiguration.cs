using CryptoPrices.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoPrices.Core.Data.Configuration
{
    public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            builder.HasKey(x => x.CryptoCurrencyId);

            builder
                .Property(x => x.Price)
                .HasColumnType("decimal(18,5)");

            builder
                .Property(x => x.Volume24h)
                .HasColumnType("decimal(18,5)");

            builder
                .Property(x => x.PercentChange1h)
                .HasColumnType("decimal(18,5)");

            builder
                .Property(x => x.PercentChange24h)
                .HasColumnType("decimal(18,5)");

            builder
                .Property(x => x.PercentChange7d)
                .HasColumnType("decimal(18,5)");

            builder
                .Property(x => x.MarketCap)
                .HasColumnType("decimal(18,5)");

            builder
                .Property(x => x.LastUpdated)
                .IsRequired();

            builder
                .HasOne(x => x.CryptoCurrency)
                .WithOne(x => x.Quote)
                .HasForeignKey<Quote>(x => x.CryptoCurrencyId);
        }
    }
}
