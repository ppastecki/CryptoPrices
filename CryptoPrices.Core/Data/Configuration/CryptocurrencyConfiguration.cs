using CryptoPrices.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoPrices.Core.Data.Configuration
{
    public class CryptocurrencyConfiguration : IEntityTypeConfiguration<CryptoCurrency>
    {
        public void Configure(EntityTypeBuilder<CryptoCurrency> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();

            builder
                .Property(x => x.Name)
                .IsRequired();

            builder
                .Property(x => x.Symbol)
                .IsRequired();

            builder
                .Property(x => x.MaxSupply)
                .HasColumnType("decimal(18,5)");

            builder
                .Property(x => x.CirculatingSupply)
                .HasColumnType("decimal(18,5)");

            builder
                .Property(x => x.TotalSupply)
                .HasColumnType("decimal(18,5)");

            builder
                .Property(x => x.LastUpdated)
                .IsRequired();
        }
    }
}
