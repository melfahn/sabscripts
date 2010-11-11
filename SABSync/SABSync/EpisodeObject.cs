using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SABSync
{
    public class EpisodeObject : IComparable
    {
        private string _airDate;
        private string _airTime;
        private DateTime _airs;

        //public DateTime AirDate
        //{
        //    get { return ParseDate(AirDateString);}
        //    set { AirDate = value; }
        //}
        public long? SeasonNumber { get; set; }
        public long? EpisodeNumber { get; set; }
        public string EpisodeName { get; set; }
        public long EpisodeId { get; set; }

        public DateTime Airs
        {
            get
            {
                GetAirs();
                return _airs;
            }
            set { _airs = value; }
        }

        public string AirDate
        {
            get { return _airDate; }
            set { _airDate = value; }
        }
        public string AirTime
        {
            get { return _airTime; }
            set { _airTime = value; }
        }

        private DateTime ParseDate(string dateString)
        {
            DateTime date = DateTime.Today.AddYears(-100);
            DateTime.TryParse(dateString, out date);

            return date;
        }

        public int CompareTo(object obj)
        {
            EpisodeObject episode = (EpisodeObject)obj;

            if (this.Airs > episode.Airs)
                return 1;

            if (this.Airs < episode.Airs)
                return -1;

            else
                return 0;
        }

        private void GetAirs()
        {
            if (String.IsNullOrEmpty(_airDate))
                _airDate = DateTime.Today.AddYears(-100).ToString("yyyy-MM-dd");

            string time = _airTime.Replace('.', ':'); //Replace period with colon
            DateTime.TryParse(time + _airDate, out _airs);

            if (_airs.Year != 0001)
                return;

            //Retry this shit... first fixing the Time
            string timeFix;
            DateTime dt;

            DateTime.TryParse(time, out dt);
            timeFix = dt.ToString("h:mm tt");

            DateTime.TryParse(timeFix + _airDate, out _airs);
        }
    }
}
