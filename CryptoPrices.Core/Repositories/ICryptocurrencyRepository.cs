using CryptoPrices.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoPrices.Core.Repositories
{
    public interface ICryptocurrencyRepository
    {
        Task Merge(IEnumerable<CryptoCurrency> currencies);
    }
}
