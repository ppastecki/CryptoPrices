using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoPrices.Core.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CryptoCurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Symbol = table.Column<string>(nullable: false),
                    MaxSupply = table.Column<decimal>(type: "decimal", nullable: true),
                    CirculatingSupply = table.Column<decimal>(type: "decimal", nullable: true),
                    Rank = table.Column<int>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoCurrencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    CryptoCurrencyId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal", nullable: true),
                    Volume24h = table.Column<decimal>(type: "decimal", nullable: true),
                    PercentChange1h = table.Column<decimal>(type: "decimal", nullable: true),
                    PercentChange24h = table.Column<decimal>(type: "decimal", nullable: true),
                    PercentChange7d = table.Column<decimal>(type: "decimal", nullable: true),
                    MarketCap = table.Column<decimal>(type: "decimal", nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.CryptoCurrencyId);
                    table.ForeignKey(
                        name: "FK_Quotes_CryptoCurrencies_CryptoCurrencyId",
                        column: x => x.CryptoCurrencyId,
                        principalTable: "CryptoCurrencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "CryptoCurrencies");
        }
    }
}
