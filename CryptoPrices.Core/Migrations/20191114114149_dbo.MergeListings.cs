using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace CryptoPrices.Core.Migrations
{
    public partial class dboMergeListings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = File.ReadAllText(@"..\CryptoPrices.Core\Migrations\Sql\dbo.MergeListings.sql");
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROCEDURE dbo.MergeListings";
            migrationBuilder.Sql(sql);
        }
    }
}
