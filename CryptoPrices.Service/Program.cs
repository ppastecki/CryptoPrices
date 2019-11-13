using CryptoPrices.Core.Data;
using CryptoPrices.Core.Repositories;
using CryptoPrices.Service.Configuration;
using CryptoPrices.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CryptoPrices.Service
{
    public static class Program
    {
        private static IConfiguration _configuration;
        private static IServiceProvider _serviceProvider;

        public static async Task Main(string[] args)
        {
            _configuration = GetConfiguration();
            _serviceProvider = ConfigureServices().BuildServiceProvider();

            var importer = _serviceProvider.GetService<ICoinmarketImporter>();
            await importer.Import();
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

            services.AddSingleton(provider => _configuration.GetSection("ServiceConfiguration").Get<ServiceConfiguration>());

            services.AddTransient<ICoinmarketClient, CoinmarketClient>();
            services.AddTransient<ICoinmarketImporter, CoinmarketImporter>();
            services.AddTransient<ICoinmarketParser, CoinmarketParser>();
            services.AddTransient<ICryptocurrencyRepository, CryptocurrencyRepository>();
            services.AddTransient<WebClient>();

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
