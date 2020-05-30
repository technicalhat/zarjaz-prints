using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZarjazPrints2020.Application.Entities;

namespace ZarjazPrints2020.Application.Data
{
    /// <summary>
    /// Read .json prog data from the target folder
    /// </summary>
    public class FileSystemProgRawDataSource : IProgRawDataSource
    {
        private Settings settings;

        public class Settings
        {
            public string FileRoot { get; set; }
        }
        public FileSystemProgRawDataSource(IOptions<Settings> options, RelativePathResolver resolver)
        {
            this.settings = options.Value;
            // Q : Is there a way of automagically injecting the path resolution into the config binding?
            // that would be cool :)
            settings.FileRoot = resolver.Resolve(settings.FileRoot);
        }

        public async IAsyncEnumerable<ProgInfo> GetProgs()
        { 
            var files = Directory.GetFiles(settings.FileRoot, "*.json");
            foreach(var file in files)
            {
                using (var stream = System.IO.File.OpenText(file))
                {
                    var prog = JsonConvert.DeserializeObject<ProgInfo>(await stream.ReadToEndAsync());
                    yield return prog;
                }
            }
        }
    }
}
