using System.Collections.Generic;
using System.Linq;

namespace CryptoPrices.Web.ModelFactories
{
    public class CryptoCurrencyModelFactory : ICryptoCurrencyModelFactory
    {
        public IEnumerable<Models.CryptoCurrency> GetIndexModel(IEnumerable<Core.Entities.CryptoCurrency> currencies)
        {
            var model = new List<Models.CryptoCurrency>();

            foreach (var currency in currencies.OrderBy(c => c.Rank))
            {
                model.Add(new Models.CryptoCurrency
                {
                    Id = currency.Id,
                    Name = currency.Name,
                    Symbol = currency.Symbol,
                    Price = currency.Quote?.Price,
                    Volume24h = currency.Quote?.Volume24h,
                    PercentChange24h = currency.Quote?.PercentChange24h,
                    MarketCap = currency.Quote?.MarketCap
                });
            }

            return model;
        }

        public Models.CryptoCurrencyDetails GetDetailsModel(Core.Entities.CryptoCurrency currency)
        {
            var lastUpdated = currency.LastUpdated;

            if (currency.Quote?.LastUpdated > lastUpdated)
            {
                lastUpdated = currency.Quote.LastUpdated;
            }

            return new Models.CryptoCurrencyDetails
            {
                Id = currency.Id,
                Name = currency.Name,
                Symbol = currency.Symbol,
                MaxSupply = currency.MaxSupply,
                CirculatingSupply = currency.CirculatingSupply,
                TotalSupply = currency.TotalSupply,
                Price = currency.Quote?.Price,
                Volume24h = currency.Quote?.Volume24h,
                PercentChange1h = currency.Quote?.PercentChange1h,
                PercentChange24h = currency.Quote?.PercentChange24h,
                PercentChange7d = currency.Quote?.PercentChange7d,
                MarketCap = currency.Quote?.MarketCap,
                LastUpdated = lastUpdated
            };
        }
    }
}
