using System.Collections.Generic;

namespace CryptoPrices.Core.ModelFactories
{
    public interface ICryptoCurrencyModelFactory
    {
        IEnumerable<Models.CryptoCurrency> GetIndexModel(IEnumerable<Core.Entities.CryptoCurrency> currencies);

        Models.CryptoCurrencyDetails GetDetailsModel(Core.Entities.CryptoCurrency currencies);
    }
}
