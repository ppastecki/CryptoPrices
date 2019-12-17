using System;

namespace CryptoPrices.Core.Models
{
    public class CryptoCurrencyDetails
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public decimal? MaxSupply { get; set; }

        public decimal? CirculatingSupply { get; set; }

        public decimal? TotalSupply { get; set; }

        public decimal? Price { get; set; }

        public decimal? Volume24h { get; set; }

        public decimal? PercentChange1h { get; set; }

        public decimal? PercentChange24h { get; set; }

        public decimal? PercentChange7d { get; set; }

        public decimal? MarketCap { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
