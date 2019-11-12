using System;
using System.IO;
using System.Net;
using System.Web;

namespace CryptoPrices.Service
{
    public static class Program
    {
        private const string ApiKey = "ea8c99fb-49ac-4621-a366-ce3a00f244b3";

        public static void Main(string[] args)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("X-CMC_PRO_API_KEY", ApiKey);
                client.Headers.Add("Accepts", "application/json");

                var url = new UriBuilder("https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
                url.Query = $"start=1&limit=5000&convert=USD";

                var result = client.DownloadString(url.ToString());
                File.WriteAllText(@"c:\src\prices.json", result);
            }
        }
    }
}
