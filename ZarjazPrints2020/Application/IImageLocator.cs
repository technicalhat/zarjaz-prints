using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZarjazPrints2020.Application
{
    public interface IImageLocator
    {
        string GetCoverPath(int progId);
        string GetThumbnailPath(int progId);
    }
}
