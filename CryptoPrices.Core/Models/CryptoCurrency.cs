﻿using System.ComponentModel.DataAnnotations;

namespace CryptoPrices.Core.Models
{
    public class CryptoCurrency
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Symbol { get; set; }

        public decimal? Price { get; set; }

        public decimal? Volume24h { get; set; }

        public decimal? PercentChange24h { get; set; }

        public decimal? MarketCap { get; set; }
    }
}
