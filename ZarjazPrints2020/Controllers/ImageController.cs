using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.VisualStudio.Web.CodeGeneration;
using ZarjazPrints2020.Application;

namespace ZarjazPrints2020.Controllers
{
    /// <summary>
    /// Cover images live outside of server space with the raw data.
    /// Route image requests to the correct files
    /// </summary>
    [Route("progimages")]
    public class ImageController : Controller
    {
        private IImageLocator imageLocator;
        public ImageController(IImageLocator imageLocator)
        {
            this.imageLocator = imageLocator;
        }

        /// <summary>
        /// Full cover image for requested prog
        /// </summary>
        /// <param name="progId"></param>
        /// <returns></returns>
        [Route("cover/{progId:int}")]
        public IActionResult Cover(int progId)
        {
            var imgPath = imageLocator.GetCoverPath(progId);
            return PhysicalFile(imgPath, "image/jpeg");
        }

        /// <summary>
        /// Thunbnail image for requested prog
        /// </summary>
        /// <param name="progId"></param>
        /// <returns></returns>
        [Route("thumb/{progId:int}")]
        public IActionResult Thumbnail(int progId)
        {
            var imgPath = imageLocator.GetThumbnailPath(progId);
            return PhysicalFile(imgPath, "image/jpeg");
        }
    }
}
