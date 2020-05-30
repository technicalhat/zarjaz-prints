using System;
using Nancy;
using ZarjazPrints.ViewModels;
using Nest;
using Data;
using System.Linq;
using System.Linq.Expressions;
using Nancy.ModelBinding;

namespace ZarjazPrints.Modules
{
    public class ProgRequest
    {
        public int startIndex { get; set; }
        public int count { get; set; }
        public SearchOptions searchOptions { get; set; }
    }
    public class ProgsModule : NancyModule
    {
        private ElasticClient search;

        public ProgsModule()
        {
            var endpoint = "http://localhost:9200";
            var index = "zarjaz.progs";
            var datasource = "D:\\Development\\web\\ZarjazPrints\\extractor\\out";

            search = new ElasticClient(new ConnectionSettings
                (
                    new Uri(endpoint),
                    defaultIndex: index
                )
            );
            Get["/progs"] = parameters =>
            {
                string filter = Request.Query["filter"];
                var model = GetProgs(0, 12, String.IsNullOrEmpty(filter)? null :GetSearchParams(filter??""));
                return View["progs", model];
            };
            Get["/prog/{id}"] = parameters =>
            {
                var id = parameters.id;
                var model = GetProg(id);
                return View["prog", model];
            };
            Post["/progList"] = parameters =>
            {
                ProgRequest request = this.Bind();
                var model = GetProgs(request.startIndex, request.count, request.searchOptions);
                return View["_progList", model];
            };
        }

        private SearchOptions GetSearchParams(string filter)
        {
            var parts = filter.Split(':');
            if (parts.Length != 2) return new SearchOptions();
            switch (parts[0])
            {
                case "artist":
                    return new SearchOptions { Artist = parts[1] };

                case "cover":
                    return new SearchOptions { Cover = parts[1] };
            }
            return new SearchOptions();
        }

        private ProgViewModel GetProg(int id)
        {
            var prog = search.Get<ProgInfo>(p => p.Id(id));
            return new ProgViewModel
            {
                ProgInfo = prog.Source
            };
        }

        private ProgListViewModel GetProgs(int startIndex, int count, SearchOptions searchOptions = null)
        {
            searchOptions = searchOptions ?? new SearchOptions();


            var progs = search.Search<ProgInfo>(
                s => s.From(startIndex)
                .Size(count)
                .SortAscending(GetSorter(searchOptions.OrderBy))
                .Filter(f=>
                    f.Bool(
                        b=> b.Must(
                            m=> m.Term(t=> t.Artist, searchOptions.Artist),
                            m => m.Term(t => t.Cover, searchOptions.Cover)
                        )
                    )
                )
                
            );
            return new ProgListViewModel
            {
                SearchOptions = searchOptions,
                StartIndex = startIndex,
                Total = (int)progs.Total,
                Progs = progs.Hits.Select(h=> h.Source).ToList()
            };
        }

        private Expression<Func<ProgInfo, object>> GetSorter(string orderBy)
        {
            switch (orderBy)
            {
                case "Price":
                    return p => p.Price;
                case "Artist":
                    return p => p.Artist;
                case "Cover":
                    return p => p.Cover;
                case "FullDate":
                    return p => p.FullDate;
                case "MonthYear":
                    return p => p.MonthYear;
                case "Year":
                    return p => p.Year;
                default:
                    return p => p.Prog;
            }
        }
    }
}