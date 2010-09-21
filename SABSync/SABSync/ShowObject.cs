using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SABSync
{
    class ShowObject
    {
        private string _airTime;

        public long? Id { get; set; }
        public string ShowName { get; set; }
        public long? TvDbId { get; set; }
        public string TvDbName { get; set; }
        public int Quality { get; set; }
        public int SeasonIgnore { get; set; }
        public string Aliases { get; set; }
        public string AirDay { get; set; }
        public string AirTime
        {
            get { return _airTime; }
            set { _airTime = CleanUpTime(value); }
        }
        public int RunTime { get; set; }
        public string Status { get; set; }
        public string PosterUrl { get; set; }
        public string BannerUrl { get; set;}
        public string ImdbId { get; set; }
        public string Genre { get; set; }
        public string Overview { get; set; }

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
