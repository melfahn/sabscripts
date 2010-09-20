using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SABSync.Repository
{
    class Series
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public int Quality { get; set; }
        public string AirDay { get; set; }
        public string AirTime { get; set; }
        public string RunTime { get; set; }
        public string Status { get; set; }
        public string PosterUrl { get; set; }
        public string BannerUrl { get; set; }
        public string ImdbId { get; set; }
        public string Genre { get; set; }
        public string Overview { get; set; }
    }
}
