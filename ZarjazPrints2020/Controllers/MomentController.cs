using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ZarjazPrints2020.Controllers
{
    /// <summary>
    /// Search and view of individual "moments"
    /// </summary>
    public class MomentController : Controller
    {
        [Route("moments")]
        public IActionResult MomentList()
        {
            return View();
        }

        [Route("moment/{momentId:int}")]
        public IActionResult SingleMoment(int momentId)
        {
            return View();
        }
    }
}
