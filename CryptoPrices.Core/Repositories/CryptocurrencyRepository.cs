using CryptoPrices.Core.Data;
using CryptoPrices.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoPrices.Core.Repositories
{
    public class CryptocurrencyRepository : ICryptocurrencyRepository
    {
        private readonly CryptoPricesContext _context;

        public CryptocurrencyRepository(CryptoPricesContext context)
        {
            _context = context;
        }

        public async Task Merge(IEnumerable<CryptoCurrency> currencies)
        {
            var quotes = currencies
                .Where(c => c.Quote != null)
                .Select(c => c.Quote);

            var currenciesParam = GetCurrenciesParam(currencies);
            var quotesParam = GetQuotesParam(quotes);

            await _context.Database.ExecuteSqlCommandAsync("dbo.MergeListings @currencies = @currencies, @quotes = @quotes",
                currenciesParam, quotesParam);
        }

        private SqlParameter GetCurrenciesParam(IEnumerable<CryptoCurrency> currencies)
        {
            var table = new DataTable();

            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Symbol", typeof(string));
            table.Columns.Add("MaxSupply", typeof(decimal));
            table.Columns.Add("CirculatingSupply", typeof(decimal));
            table.Columns.Add("TotalSupply", typeof(decimal));
            table.Columns.Add("Rank", typeof(int));
            table.Columns.Add("LastUpdated", typeof(DateTime));

            foreach (var currency in currencies)
            {
                var row = table.NewRow();

                row["Id"] = currency.Id;
                row["Name"] = currency.Name;
                row["Symbol"] = currency.Symbol;
                row["MaxSupply"] = GetSqlValue(currency.MaxSupply);
                row["CirculatingSupply"] = GetSqlValue(currency.CirculatingSupply);
                row["TotalSupply"] = GetSqlValue(currency.TotalSupply);
                row["Rank"] = GetSqlValue(currency.Rank);
                row["LastUpdated"] = currency.LastUpdated;

                table.Rows.Add(row);
            }

            return new SqlParameter("@currencies", table)
            {
                TypeName = "dbo.CryptoCurrency",
                SqlDbType = SqlDbType.Structured
            };
        }

        private SqlParameter GetQuotesParam(IEnumerable<Quote> quotes)
        {
            var table = new DataTable();

            table.Columns.Add("CryptoCurrencyId", typeof(int));
            table.Columns.Add("Price", typeof(decimal));
            table.Columns.Add("Volume24h", typeof(decimal));
            table.Columns.Add("PercentChange1h", typeof(decimal));
            table.Columns.Add("PercentChange24h", typeof(decimal));
            table.Columns.Add("PercentChange7d", typeof(decimal));
            table.Columns.Add("MarketCap", typeof(decimal));
            table.Columns.Add("LastUpdated", typeof(DateTime));

            foreach (var quote in quotes)
            {
                var row = table.NewRow();

                row["CryptoCurrencyId"] = quote.CryptoCurrencyId;
                row["Price"] = GetSqlValue(quote.Price);
                row["Volume24h"] = GetSqlValue(quote.Volume24h);
                row["PercentChange1h"] = GetSqlValue(quote.PercentChange1h);
                row["PercentChange24h"] = GetSqlValue(quote.PercentChange24h);
                row["PercentChange7d"] = GetSqlValue(quote.PercentChange7d);
                row["MarketCap"] = GetSqlValue(quote.MarketCap);
                row["LastUpdated"] = quote.LastUpdated;

                table.Rows.Add(row);
            }

            return new SqlParameter("@quotes", table)
            {
                TypeName = "dbo.Quote",
                SqlDbType = SqlDbType.Structured
            };
        }

        private object GetSqlValue(decimal? value)
        {
            if (value.HasValue)
            {
                return value;
            }
            else
            {
                return DBNull.Value;
            }
        }

        private object GetSqlValue(int? value)
        {
            if (value.HasValue)
            {
                return value;
            }
            else
            {
                return DBNull.Value;
            }
        }
    }
}
