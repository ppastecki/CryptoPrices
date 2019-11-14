using CryptoPrices.Service.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace CryptoPrices.Service.Services
{
    public class CoinmarketClient : ICoinmarketClient
    {
        private readonly ServiceConfiguration _serviceConfiguration;

        public CoinmarketClient(ServiceConfiguration serviceConfiguration)
        {
            _serviceConfiguration = serviceConfiguration;
        }

        public async Task<string> GetLatestListings()
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("X-CMC_PRO_API_KEY", _serviceConfiguration.CoinmarketApiKey);
                webClient.Headers.Add("Accepts", "application/json");

                var query = HttpUtility.ParseQueryString(string.Empty);
                query["start"] = _serviceConfiguration.CoinmarketStart.ToString();
                query["limit"] = _serviceConfiguration.CoinmarketLimit.ToString();
                query["convert"] = _serviceConfiguration.CoinmarketConvert;

                var builder = new UriBuilder(_serviceConfiguration.CoinmarketLatestListingsUrl);
                builder.Query = query.ToString();

                return await webClient.DownloadStringTaskAsync(builder.Uri);
            }
        }
    }
}
