using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZarjazPrints2020.Application.Entities
{
    /// <summary>
    /// Details of a specific Prog
    /// </summary>
    public class ProgInfo
    {
        /// <summary>
        /// Cover artist
        /// </summary>
        public string Artist { get; set; }
        public string MonthYear { get; set; }
        /// <summary>
        /// Price as displayed on cover
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// Dates are only for display so don't need this to be a datetime
        /// </summary>
        public string FullDate { get; set; }
        /// <summary>
        /// Featured cover story
        /// </summary>
        public string Cover{ get; set; }
        public string Year { get; set; }
        /// <summary>
        /// Issue No.
        /// </summary>
        public int Prog { get; set; }

        /// <summary>
        /// "Moments" featured in this prog (specific panels available as standalone images)
        /// </summary>
        public List<ProgMomentInfo> Moments { get; set; } = new List<ProgMomentInfo>();

        //don't strictly need these since the files follow a convention but they're part of the source data
        public string ImageFile { get; set; }
        public string Thumbnail { get; set; }

        /// <summary>
        /// Just enough info about a prog's moments to be able to render a summary. For anything else, query moment data source
        /// </summary>
        public class ProgMomentInfo
        {
            public int MomentId { get; set; }
            public string MomentName { get; set; }
        }
    }
}
