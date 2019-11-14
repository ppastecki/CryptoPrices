using CryptoPrices.Core.Data;
using CryptoPrices.Core.Repositories;
using CryptoPrices.Service.Configuration;
using CryptoPrices.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Topshelf;

namespace CryptoPrices.Service
{
    public class Program
    {
        private static IConfiguration _configuration;
        private static IServiceProvider _serviceProvider;
        private static ILogger<Program> _logger;

        public static void Main(string[] args)
        {
            _configuration = GetConfiguration();
            _serviceProvider = ConfigureServices().BuildServiceProvider();
            _logger = _serviceProvider.GetService<ILogger<Program>>();

            HostFactory.Run(x =>
            {
                x.Service(() => _serviceProvider.GetService<ImportService>());
                x.EnableServiceRecovery(r => r.RestartService(TimeSpan.FromSeconds(30)));
                x.SetServiceName("CryptoPricesService");
                x.StartAutomatically();
                x.OnException(ex =>
                {
                    _logger.LogError($"{DateTime.UtcNow}: {nameof(ImportService)} has thrown exception.", ex);
                });
            });
        }

        private static IServiceCollection ConfigureServices()
        {
            var configuration = GetConfiguration();
            var services = new ServiceCollection();

            services.AddDbContext<CryptoPricesContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("CryptoPrices");
                var migrationsAssembly = typeof(CryptoPricesContext).Assembly.FullName;

                options.UseSqlServer(connectionString, action => action.MigrationsAssembly(migrationsAssembly));
            });

            services.AddLogging(options => options.AddConsole())
                .AddSingleton<ImportService>()
                .AddSingleton<Program>();

            services.AddSingleton(provider => _configuration.GetSection("ServiceConfiguration").Get<ServiceConfiguration>());
            services.AddSingleton<ImportService>();

            services.AddTransient<ICoinmarketClient, CoinmarketClient>();
            services.AddTransient<ICoinmarketImporter, CoinmarketImporter>();
            services.AddTransient<ICoinmarketParser, CoinmarketParser>();
            services.AddTransient<ICryptocurrencyRepository, CryptocurrencyRepository>();

            return services;
        }

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
