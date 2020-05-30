using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarjazPrints2020.Application.Entities;

namespace ZarjazPrints2020.Application.Data
{
    /// <summary>
    /// Feeds prog data from an external source to the indexer.
    /// In a non-demo implementation this might be talking to a headless CMS
    /// </summary>
    public interface IProgRawDataSource
    {
        IAsyncEnumerable<ProgInfo> GetProgs();
    }
}
