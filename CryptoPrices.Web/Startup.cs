﻿using CryptoPrices.Core.Data;
using CryptoPrices.Core.ModelFactories;
using CryptoPrices.Core.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoPrices.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<CryptoPricesContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("CryptoPrices");
                var migrationsAssembly = typeof(CryptoPricesContext).Assembly.FullName;

                options.UseSqlServer(connectionString, action => action.MigrationsAssembly(migrationsAssembly));
            });

            services.AddSingleton<ICryptoCurrencyModelFactory, CryptoCurrencyModelFactory>();
            services.AddTransient<ICryptocurrencyRepository, CryptocurrencyRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=CryptoCurrency}/{action=Index}/{id?}");
            });
        }
    }
}
