using CryptoPrices.Service.Configuration;
using System;
using System.Threading;
using Topshelf;

namespace CryptoPrices.Service.Services
{
    public class ImportService : ServiceControl, IDisposable
    {
        private readonly ICoinmarketImporter _coinmarketImporter;
        private readonly ServiceConfiguration _serviceConfiguration;
        private readonly Timer _timer;

        public ImportService(ICoinmarketImporter coinmarketImporter, ServiceConfiguration serviceConfiguration)
        {
            _coinmarketImporter = coinmarketImporter;
            _serviceConfiguration = serviceConfiguration;
            _timer = new Timer(Import);
        }

        public bool Start(HostControl hostControl)
        {
            _timer.Change(0, _serviceConfiguration.ImporterPeriod);

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);

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
        }
    }
}
