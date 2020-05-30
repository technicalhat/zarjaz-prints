using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using ZarjazPrints2020.Application.Data;
using ZarjazPrints2020.Application.Entities;

namespace ZarjazPrints2020.Models.Response
{
    public class CoverListViewModel
    {
        public ProgQuery Query { get; internal set; }
        public IPagedList<ProgInfo> Progs { get; internal set; }
    }
}
