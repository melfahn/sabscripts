using System;

namespace SABSync
{
    public class Episode
    {
        public FeedItem FeedItem { get; set; }
        public string ShowName { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public int EpisodeNumber2 { get; set; }
        public DateTime FirstAired { get; set; }

        public bool IsDaily
        {
            get { return SeasonNumber == 0 && EpisodeNumber == 0 && EpisodeNumber2 == 0; }
        }

        public bool IsMulti
        {
            get { return SeasonNumber != 0 && EpisodeNumber != 0 && EpisodeNumber2 != 0; }
        }
    }
}