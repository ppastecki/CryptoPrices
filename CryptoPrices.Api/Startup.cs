using CryptoPrices.Core.Data;
using CryptoPrices.Core.ModelFactories;
using CryptoPrices.Core.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace CryptoPrices.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<CryptoPricesContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("CryptoPrices");
                var migrationsAssembly = typeof(CryptoPricesContext).Assembly.FullName;

                options.UseSqlServer(connectionString, action => action.MigrationsAssembly(migrationsAssembly));
            });

            services.AddSingleton<ICryptoCurrencyModelFactory, CryptoCurrencyModelFactory>();
            services.AddTransient<ICryptocurrencyRepository, CryptocurrencyRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Crypto Prices", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crypto Prices v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
