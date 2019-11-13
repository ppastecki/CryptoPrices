using System;

namespace CryptoPrices.Core.Entities
{
    public class CryptoCurrency
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public decimal? MaxSupply { get; set; }

        public decimal? CirculatingSupply { get; set; }

        public int? Rank { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual Quote Quote { get; set; }
    }
}
