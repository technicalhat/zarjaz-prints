using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarjazPrints2020.Application.Entities;

namespace ZarjazPrints2020.Application.Data
{
    /// <summary>
    /// Write model interface for prog data 
    /// </summary>
    public interface IProgDataWriter
    {
        void IndexProg(ProgInfo prog);
    }
}
