using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarjazPrints2020.Application.Entities;

namespace ZarjazPrints2020.Application.Data
{
    /// <summary>
    /// Write model data provider using in-memory data store
    /// </summary>
    public class InMemoryProgDataWriter : IProgDataWriter
    {
        private InMemoryProgStore progStore;

        public InMemoryProgDataWriter(InMemoryProgStore progStore)
        {
            this.progStore = progStore;
        }

        public void IndexProg(ProgInfo prog) => progStore.IndexProg(prog);
    }
}
