using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SABSync
{
    public class EpisodeObject : IComparable
    {
        public DateTime AirDate
        {
            get { return ParseDate(AirDateString);}
            set { AirDate = value; }
        }
        public long? SeasonNumber { get; set; }
        public long? EpisodeNumber { get; set; }
        public string EpisodeName { get; set; }
        public string AirDateString { get; set; }
        public long EpisodeId { get; set; }

        private DateTime ParseDate(string dateString)
        {
            DateTime date = DateTime.Today.AddYears(-100);
            DateTime.TryParse(dateString, out date);

            return date;
        }

        public int CompareTo(object obj)
        {
            EpisodeObject episode = (EpisodeObject)obj;

            if (this.AirDate > episode.AirDate)
                return 1;

            if (this.AirDate < episode.AirDate)
                return -1;

            else
                return 0;
        }
    }
}
