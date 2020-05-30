using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using ZarjazPrints2020.Application.Entities;

namespace ZarjazPrints2020.Application.Data
{
    /// <summary>
    /// Read model interface for prog data
    /// </summary>
    public interface IProgDataReader
    {
        ProgInfo GetProg(int progId);
        IPagedList<ProgInfo> FindProgs(ProgQuery query);
    }
}
