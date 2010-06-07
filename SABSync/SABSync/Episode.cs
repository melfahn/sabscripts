using System;

namespace SABSync
{
    public class Episode
    {
        public FeedItem FeedItem { get; set; }
        public string ShowName { get; set; }
        public string EpisodeName { get; set; }
        public string EpisodeName2 { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public int EpisodeNumber2 { get; set; }
        public DateTime AirDate { get; set; }

        public bool IsDaily
        {
            get { return SeasonNumber == 0 && EpisodeNumber == 0 && EpisodeNumber2 == 0; }
        }

        public bool IsMulti
        {
            get { return SeasonNumber != 0 && EpisodeNumber != 0 && EpisodeNumber2 != 0; }
        }

        public string EpisodeTitle
        {
            get { return IsDaily ? GetDailyEpisode() : IsMulti ? GetMultiEpisode() : GetSeasonEpisode(); }
        }

        public string Title
        {
            get { return string.Format("{0} - {1}", ShowName, EpisodeTitle); }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", ShowName, EpisodeTitle);
        }

        private string GetDailyEpisode()
        {
            return AirDate.ToString("yyyy-MM-dd");
        }

        private string GetMultiEpisode()
        {
            return string.Format("{0}x{1:D2}-{0}x{2:D2}", SeasonNumber, EpisodeNumber, EpisodeNumber2);
        }

        private string GetSeasonEpisode()
        {
            return string.Format("{0}x{1:D2}", SeasonNumber, EpisodeNumber);
        }
    }
}