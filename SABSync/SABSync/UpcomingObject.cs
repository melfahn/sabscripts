using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SABSync
{
    class UpcomingObject
    {
        private string _airDate;
        private string _airTime;
        private DateTime _airs;
        
        public string ShowName { get; set; }
        public long? SeasonNumber { get; set; }
        public long? EpisodeNumber { get; set; }
        public string EpisodeName { get; set; }
        public DateTime Airs
        {
            get { GetAirs();
                return _airs; }
            set { _airs = value; }
        }
        public string Overview { get; set; }

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

        private void GetAirs()
        {
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

        private string CleanUpTime(string time)
        {
            time = time.Replace('.', ':'); //Replace period with colon
            DateTime dt;
            DateTime.TryParse(time, out dt);

            if (dt.Year == 0001) //If Year 0001 then Parse failed (Time Zone info added? Bad Data @ TvDb)
                return "12:59 AM";
            
            return dt.ToString("h:mm tt");
        }
    }
}
