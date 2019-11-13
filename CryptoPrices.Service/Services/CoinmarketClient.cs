using CryptoPrices.Service.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace CryptoPrices.Service.Services
{
    public class CoinmarketClient : ICoinmarketClient, IDisposable
    {
        private readonly ServiceConfiguration _serviceConfiguration;
        private readonly WebClient _webClient;

        public CoinmarketClient(ServiceConfiguration serviceConfiguration, WebClient webClient)
        {
            _serviceConfiguration = serviceConfiguration;
            _webClient = webClient;
        }

        public async Task<string> GetLatestListings()
        {
            _webClient.Headers.Add("X-CMC_PRO_API_KEY", _serviceConfiguration.CoinmarketApiKey);
            _webClient.Headers.Add("Accepts", "application/json");

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["start"] = _serviceConfiguration.CoinmarketStart.ToString();
            query["limit"] = _serviceConfiguration.CoinmarketLimit.ToString();
            query["convert"] = _serviceConfiguration.CoinmarketConvert;

            var builder = new UriBuilder(_serviceConfiguration.CoinmarketLatestListingsUrl);
            builder.Query = query.ToString();

            return await _webClient.DownloadStringTaskAsync(builder.Uri);
        }

        public void Dispose()
        {
            var webClient = _webClient;

            if (webClient != null)
            {
                webClient.Dispose();
            }
        }
    }
}
