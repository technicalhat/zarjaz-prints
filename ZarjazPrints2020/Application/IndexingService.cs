using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZarjazPrints2020.Application.Data;
using ZarjazPrints2020.Application.Entities;

namespace ZarjazPrints2020.Application
{
    /// <summary>
    /// Runs on startup to import prog data into the index
    /// </summary>
    public class IndexingService : BackgroundService
    {
        private IServiceScopeFactory serviceScopeFactory;
       
        public IndexingService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var writer = scope.ServiceProvider.GetService<IProgDataWriter>();
                var source = scope.ServiceProvider.GetService<IProgRawDataSource>();

                await foreach(var prog in source.GetProgs())
                {
                    writer.IndexProg(prog);
                }   
            }
        }
    }
}
