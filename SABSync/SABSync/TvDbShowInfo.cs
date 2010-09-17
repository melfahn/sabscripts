using System.Collections.Generic;

namespace SABSync
{
    public class TvDbShowInfo
    {
        //Used to Store information being returned by TvDbService for storage in SABSync.db.shows
        //AirDay
        //AirTime
        //Runtime
        //Status
        //Poster
        //ImdbId
        //Genre
        //Overview

        private List<TvDbEpisodeInfo> _episodes;

        public List<TvDbEpisodeInfo> Episodes
        {
            get { return _episodes; }
            set { _episodes = value; }
        }

        public int SeriesId
        { get; set; }

        public string SeriesName
        { get; set; }

        public string AirDay
        { get; set; }

        public string AirTime
        { get; set; }

        public int RunTime
        { get; set; }

        public string Status
        { get; set; }

        public string PosterUrl
        { get; set; }

        public string BannerUrl
        { get; set; }

        public string ImdbId
        { get; set; }

        public string Genre
        { get; set; }

        public string Overview
        { get; set; }
    }
}
