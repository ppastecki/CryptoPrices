using CryptoPrices.Service.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using Topshelf;

namespace CryptoPrices.Service.Services
{
    public class ImportService : ServiceControl, IDisposable
    {
        private readonly ICoinmarketImporter _coinmarketImporter;
        private readonly ILogger<ImportService> _logger;
        private readonly ServiceConfiguration _serviceConfiguration;
        private readonly Timer _timer;

        public ImportService(ICoinmarketImporter coinmarketImporter, ILogger<ImportService> logger, ServiceConfiguration serviceConfiguration)
        {
            _coinmarketImporter = coinmarketImporter;
            _logger = logger;
            _serviceConfiguration = serviceConfiguration;
            _timer = new Timer(Import);
        }

        public bool Start(HostControl hostControl)
        {
            _timer.Change(0, _serviceConfiguration.ImporterPeriod);
            _logger.LogInformation($"{DateTime.UtcNow}: Service started.");

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _logger.LogInformation($"{DateTime.UtcNow}: Service stopped.");

            return true;
        }

        public void Dispose()
        {
            var timer = _timer;

            if (_timer != null)
            {
                _timer.Dispose();
            }
        }

        private void Import(object state)
        {
            _coinmarketImporter.Import().Wait();
            _logger.LogInformation($"{DateTime.UtcNow}: Import from CoinMarket completed.");
        }
    }
}
