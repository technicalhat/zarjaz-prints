using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Nest;

namespace ZarjazPrints.ViewModels
{
    //encapsulates everything I need to render a page of progs
    public class ProgListViewModel
    {
        public int StartIndex { get; set; }
        public int Total { get; set; }

        public SearchOptions SearchOptions { get; set; }
        public List<ProgInfo> Progs { get; internal set; }
    }

    public class SearchOptions
    {
        public string Artist { get; set; }
        public string MonthYear { get; set; }
        public string Price { get; set; }
        public string FullDate { get; set; }
        public string Cover { get; set; }
        public string Year { get; set; }
        public string OrderBy { get; set; }
        
    }
}
