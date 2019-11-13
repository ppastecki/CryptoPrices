using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace CryptoPrices.Core.Migrations
{
    public partial class dboCryptoCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = File.ReadAllText(@"..\CryptoPrices.Core\Migrations\Sql\dbo.CryptoCurrency.sql");
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP TYPE dbo.CryptoCurrency";
            migrationBuilder.Sql(sql);
        }
    }
}
