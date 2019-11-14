using CryptoPrices.Core.Repositories;
using System.Threading.Tasks;

namespace CryptoPrices.Service.Services
{
    public class CoinmarketImporter : ICoinmarketImporter
    {
        private readonly ICoinmarketClient _coinmarketClient;
        private readonly ICoinmarketParser _coinmarketParser;
        private readonly ICryptocurrencyRepository _cryptocurrencyRepository;

        public CoinmarketImporter(ICoinmarketClient coinmarketClient, ICoinmarketParser coinmarketParser,
            ICryptocurrencyRepository cryptocurrencyRepository)
        {
            _coinmarketClient = coinmarketClient;
            _coinmarketParser = coinmarketParser;
            _cryptocurrencyRepository = cryptocurrencyRepository;
        }

        public async Task Import()
        {
            var json = await _coinmarketClient.GetLatestListings();
            var cryptoCurrencies = _coinmarketParser.ParseLatestListings(json);

            await _cryptocurrencyRepository.MergeAsync(cryptoCurrencies);
        }
    }
}
