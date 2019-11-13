using CryptoPrices.Core.Entities;
using CryptoPrices.Service.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace CryptoPrices.Service.Services
{
    public class CoinmarketParser : ICoinmarketParser
    {
        ServiceConfiguration _serviceConfiguration;

        public CoinmarketParser(ServiceConfiguration serviceConfiguration)
        {
            _serviceConfiguration = serviceConfiguration;
        }

        public IEnumerable<CryptoCurrency> ParseLatestListings(string json)
        {
            dynamic jobject = JObject.Parse(json);
            var convert = _serviceConfiguration.CoinmarketConvert;
            var currencies = new List<CryptoCurrency>();

            foreach (dynamic currencyObj in jobject.data)
            {
                dynamic quoteObj = ((JObject)currencyObj.quote)[convert];

                var quote = new Quote
                {
                    CryptoCurrencyId = currencyObj.id,
                    Price = quoteObj.price,
                    Volume24h = quoteObj.volume_24h,
                    PercentChange1h = quoteObj.percent_change_1h,
                    PercentChange24h = quoteObj.percent_change_24h,
                    PercentChange7d = quoteObj.percent_change_7d,
                    MarketCap = quoteObj.market_cap,
                    LastUpdated = quoteObj.last_updated
                };

                var currency = new CryptoCurrency
                {
                    Id = currencyObj.id,
                    Name = currencyObj.name,
                    Symbol = currencyObj.symbol,
                    MaxSupply = currencyObj.max_supply,
                    CirculatingSupply = currencyObj.circulating_supply,
                    Rank = currencyObj.cmc_rank,
                    LastUpdated = currencyObj.last_updated,
                    Quote = quote
                };

                currencies.Add(currency);
            }

            return currencies;
        }
    }
}
