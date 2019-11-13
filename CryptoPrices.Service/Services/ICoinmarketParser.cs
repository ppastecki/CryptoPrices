using CryptoPrices.Core.Entities;
using System.Collections.Generic;

namespace CryptoPrices.Service.Services
{
    public interface ICoinmarketParser
    {
        IEnumerable<CryptoCurrency> ParseLatestListings(string json);
    }
}
