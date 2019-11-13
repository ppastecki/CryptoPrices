using CryptoPrices.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CryptoPrices.Service
{
    public class TestService
    {
        private readonly CryptoPricesContext _context;

        public TestService(CryptoPricesContext context)
        {
            _context = context;
        }

        public async Task PrintDiagnosticsAsync()
        {
            var cryptoCurrenciesCount = await _context.CryptoCurrencies.CountAsync();
            var quotesCount = await _context.Quotes.CountAsync();

            Console.WriteLine($"crypto currencies={cryptoCurrenciesCount}, quotes={quotesCount}");
        }
    }
}
