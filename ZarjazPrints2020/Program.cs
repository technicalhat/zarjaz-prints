using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ZarjazPrints2020.Application;
using ZarjazPrints2020.Application.Data;

namespace ZarjazPrints2020
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            //add a hosted indexer background service as well as the website
            .ConfigureServices(services=>
            {
                services.AddSingleton<InMemoryProgStore>();
                services.AddScoped<IProgDataReader, InMemoryProgDataReader>();
                services.AddHostedService<IndexingService>();
            })
            ;
    }
}
