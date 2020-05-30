using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using ZarjazPrints2020.Application.Entities;

namespace ZarjazPrints2020.Application.Data
{
    /// <summary>
    /// Read model data provider using the in-memory data store
    /// </summary>
    public class InMemoryProgDataReader : IProgDataReader
    {
        private InMemoryProgStore progStore;

        public InMemoryProgDataReader(InMemoryProgStore progStore)
        {
            this.progStore = progStore;
        }

        public IPagedList<ProgInfo> FindProgs(ProgQuery query)
        {
            List<ProgInfo> progs;
            //nearly forgot to handle the degenerate case. If we don't have any query parameters, return everything
            if (QueryIsEmpty(query))
            {
                progs = progStore
                    .GetAllProgs()
                    .OrderBy(prog => prog.Prog)
                    .ToList();
            }
            else
            {
                progs = progStore.GetProgsByArtist(query.Artist)
                    .Concat(progStore.GetProgsByMonthYear(query.MonthYear))
                    .Concat(progStore.GetProgsByCover(query.Cover))
                    .Concat(progStore.GetProgsByYear(query.Year))
                    .DistinctBy(prog => prog.Prog)
                    .OrderBy(prog => prog.Prog)
                    .ToList();
            }

            return progs.ToPagedList(query.Page, query.PageSize);
        }

        public ProgInfo GetProg(int progId) => progStore.GetProg(progId);
        private bool QueryIsEmpty(ProgQuery query)
        {
            return string.IsNullOrWhiteSpace(query.Artist)
                && string.IsNullOrWhiteSpace(query.MonthYear)
                && string.IsNullOrWhiteSpace(query.Cover)
                && string.IsNullOrWhiteSpace(query.Year);
        }
    }
}
