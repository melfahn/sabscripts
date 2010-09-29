using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SABSync.Web.Models
{
    public class ShowModel
    {
        public string Name { get; set; }
        public long? TvDbId { get; set; }
        public string TvDbName { get; set; }
        public long? Quality { get; set; }
        public long? IgnoreSeason { get; set; }
        public string Aliases { get; set; }
        public string AirDay { get; set; }
        public string AirTime { get; set; }
        public string Status { get; set; }
        public string Genre { get; set; }
    }
}