using CryptoPrices.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoPrices.Core.Repositories
{
    public interface ICryptocurrencyRepository
    {
        Task<CryptoCurrency> GetAsync(int id);

        Task<IEnumerable<CryptoCurrency>> GetAllAsync();

        Task MergeAsync(IEnumerable<CryptoCurrency> currencies);
    }
}
