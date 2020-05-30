using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZarjazPrints2020.Application.Data
{
    /// <summary>
    /// Fields that can be used to query the prog data source
    /// </summary>
    public class ProgQuery
    {
        public string Artist { get; set; }
        public string MonthYear { get; set; }
        public string Year { get; set; }
        public string Cover { get; set; }
        public int PageSize { get; set; } = 20;
        public int Page { get; set; } = 1;
    }
}
