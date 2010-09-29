using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SABSync.Web.Models
{
    public class HistoryModel
    {
        public long? Id { get; set; }
        public string ShowName { get; set; }
        public long? SeasonNumber { get; set; }
        public long? EpisodeNumber { get; set; }
        public string EpisodeName { get; set; }
        public string FeedTitle { get; set; }
        public int? Quality { get; set; }
        public bool Proper { get; set; }
        public string Provider { get; set; }
        public DateTime Date { get; set; }

        public long? ProperLong
        {
            get { return Convert.ToInt64(Proper); }
            set { Proper = Convert.ToBoolean(value); }
        }

        public string DateString
        {
            get { return Convert.ToString(Date); }
            set { Date = Convert.ToDateTime(value); }
        }
    }
}