using Newtonsoft.Json;

namespace CryptoPrices.Service.Configuration
{
    [JsonObject("ServiceConfiguration")]
    public class ServiceConfiguration
    {
        [JsonProperty("CoinmarketApiKey")]
        public string CoinmarketApiKey { get; set; }

        [JsonProperty("CoinmarketLatestListingsUrl")]
        public string CoinmarketLatestListingsUrl { get; set; }

        [JsonProperty("CoinmarketStart")]
        public int CoinmarketStart { get; set; }

        [JsonProperty("CoinmarketLimit")]
        public int CoinmarketLimit { get; set; }

        [JsonProperty("CoinmarketConvert")]
        public string CoinmarketConvert { get; set; }

        [JsonProperty("ImporterPeriod")]
        public int ImporterPeriod { get; set; }
    }
}
