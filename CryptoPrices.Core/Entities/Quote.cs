using System;

namespace CryptoPrices.Core.Entities
{
    public class Quote
    {
        public int CryptoCurrencyId { get; set; }

        public decimal? Price { get; set; }

        public decimal? Volume24h { get; set; }

        public decimal? PercentChange1h { get; set; }

        public decimal? PercentChange24h { get; set; }

        public decimal? PercentChange7d { get; set; }

        public decimal? MarketCap { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual CryptoCurrency CryptoCurrency { get; set; }
    }
}
