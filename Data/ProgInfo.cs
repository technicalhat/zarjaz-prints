using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Data
{
    [ElasticType(IdProperty="Prog")]
    public class ProgInfo
    {
        public string Artist { get; set; }
        public string MonthYear { get; set; }
        public string Price { get; set; }
        public string FullDate { get; set; }
        public string Cover { get; set; }
        public string ImageFile { get; set; }
        public string Year { get; set; }
        public int Prog { get; set; }
        public string Thumbnail{ get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
