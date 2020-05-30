using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ZarjazPrints2020.Application.Entities;

namespace ZarjazPrints2020.Application.Data
{
    /// <summary>
    /// Original version of ZarjazPrints used Elasticsearch as a backing store.
    /// For portability, the refactor is keeping the prog data in memory since it's just a demo and:
    /// 1. I don't want to have to re-learn EF this weekend
    /// 2. Not needing to worry about database config makes this example more plug-and-play
    /// 
    /// So this is a shared dictionary of progs with a lazy index implementaion that can be injected into the data reader/writers
    /// </summary>
    public class InMemoryProgStore
    {
        private Dictionary<int, ProgInfo> progData = new Dictionary<int, ProgInfo>();


        //quick access to all progs with a given cover artist
        private Dictionary<string, List<int>> artistIndex = new Dictionary<string, List<int>>();
        //quick access to all progs with a given cover story
        private Dictionary<string, List<int>> coverIndex = new Dictionary<string, List<int>>();
        //quick access to all progs in a specific month
        private Dictionary<string, List<int>> monthYearIndex = new Dictionary<string, List<int>>();
        //quick access to all progs in a specific year
        private Dictionary<string, List<int>> yearIndex = new Dictionary<string, List<int>>();

        public void IndexProg(ProgInfo prog)
        {
            progData[prog.Prog] = prog;
            Index(artistIndex, prog.Artist, prog.Prog);
            Index(coverIndex, prog.Cover, prog.Prog);
            Index(monthYearIndex, prog.MonthYear, prog.Prog);
            Index(yearIndex, prog.Year, prog.Prog);
        }

        public ProgInfo GetProg(int progId)
        {
            if (progData.ContainsKey(progId))
            {
                return progData[progId];
            }
            return null;
        }

        internal IEnumerable<ProgInfo> GetAllProgs()
        {
            return progData.Values.ToList();
        }
        public IEnumerable<ProgInfo> GetProgsByArtist(string artist)
        {
            return GetProgsFromIndex(artistIndex, artist);
        }
        public IEnumerable<ProgInfo> GetProgsByCover(string cover)
        {
            return GetProgsFromIndex(coverIndex, cover);
        }
        public IEnumerable<ProgInfo> GetProgsByMonthYear(string monthYear)
        {
            return GetProgsFromIndex(monthYearIndex, monthYear);
        }
        public IEnumerable<ProgInfo> GetProgsByYear(string year)
        {
            return GetProgsFromIndex(yearIndex, year);
        }


        /// <summary>
        /// Add a given prog Id to an index
        /// If this wasn't just a throwaway implementation there's an argument for moving this out to 
        /// an index class.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="key"></param>
        /// <param name="progId"></param>
        private void Index(Dictionary<string, List<int>> index, string key, int progId)
        {
            if (string.IsNullOrWhiteSpace(key)) return;

            if (!index.ContainsKey(key))
            {
                index[key] = new List<int>();
            }
            index[key].Add(progId);
        }

        /// <summary>
        /// Lookup prog Ids from a specific index and return their ProgInfo
        /// </summary>
        /// <param name="index"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private IEnumerable<ProgInfo> GetProgsFromIndex(Dictionary<string, List<int>> index, string key)
        {
            if (index.ContainsKey(key??""))
            {
                var ids = index[key];
                //force materialisation if the Enum here so we're not passing lambdas around
                return ids.Select(id => progData[id]).ToList();
            }
            return Enumerable.Empty<ProgInfo>(); 
        }

    }
}
