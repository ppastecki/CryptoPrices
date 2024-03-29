﻿// <auto-generated />
using System;
using CryptoPrices.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CryptoPrices.Core.Migrations
{
    [DbContext(typeof(CryptoPricesContext))]
    partial class CryptoPricesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CryptoPrices.Core.Entities.CryptoCurrency", b =>
                {
                    b.Property<int>("Id");

                    b.Property<decimal?>("CirculatingSupply")
                        .HasColumnType("decimal(18,5)");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<decimal?>("MaxSupply")
                        .HasColumnType("decimal(18,5)");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("Rank");

                    b.Property<string>("Symbol")
                        .IsRequired();

                    b.Property<decimal?>("TotalSupply")
                        .HasColumnType("decimal(18,5)");

                    b.HasKey("Id");

                    b.ToTable("CryptoCurrencies");
                });

            modelBuilder.Entity("CryptoPrices.Core.Entities.Quote", b =>
                {
                    b.Property<int>("CryptoCurrencyId");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<decimal?>("MarketCap")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal?>("PercentChange1h")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal?>("PercentChange24h")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal?>("PercentChange7d")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,5)");

                    b.Property<decimal?>("Volume24h")
                        .HasColumnType("decimal(18,5)");

                    b.HasKey("CryptoCurrencyId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("CryptoPrices.Core.Entities.Quote", b =>
                {
                    b.HasOne("CryptoPrices.Core.Entities.CryptoCurrency", "CryptoCurrency")
                        .WithOne("Quote")
                        .HasForeignKey("CryptoPrices.Core.Entities.Quote", "CryptoCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
