using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZarjazPrints2020.Application
{
    /// <summary>
    /// Return paths of cover and thumbnail images from a prog Id
    /// </summary>
    public class ImageLocator : IImageLocator
    {
        private Settings settings;

        public class Settings
        {
            public string FileRoot { get; set; }
        }
        public ImageLocator(IOptions<Settings> options, RelativePathResolver resolver)
        {
            this.settings = options.Value;
            settings.FileRoot = resolver.Resolve(settings.FileRoot);
        }

        public string GetCoverPath(int progId)
        {
            return Path.Combine(settings.FileRoot, $"{progId:0000}.jpg");
        }

        public string GetThumbnailPath(int progId)
        {
            return Path.Combine(settings.FileRoot, $"{progId:0000}_thumb.jpg");
        }
    }
}
