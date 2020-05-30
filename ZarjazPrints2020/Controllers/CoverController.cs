using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ZarjazPrints2020.Application.Data;
using ZarjazPrints2020.Models.Response;

namespace ZarjazPrints2020.Controllers
{
    /// <summary>
    /// Search and view of prog covers
    /// </summary>
    public class CoverController : Controller
    {
        private IProgDataReader reader;

        public CoverController(IProgDataReader reader)
        {
            this.reader = reader;
        }
        [Route("covers")]
        public IActionResult CoverList(ProgQuery query)
        {
            var model = new CoverListViewModel
            {
                Query = query,
                Progs = reader.FindProgs(query)
            };
            return View(model);
        }

        [Route("prog/{progId:int}")]
        public IActionResult SingleProg(int progId)
        {
            var model = new SingleProgViewModel
            {
                Prog = reader.GetProg(progId)
            };
            return View(model);
        }
    }
}
