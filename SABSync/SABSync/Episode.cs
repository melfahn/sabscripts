using System;

namespace SABSync
{
    public class Episode
    {
        public FeedItem FeedItem { get; set; }
        public string ShowName { get; set; }
        public string Name { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public int EpisodeNumber2 { get; set; }
        public DateTime FirstAired { get; set; }
        public bool IsFirstAired
        {
            get { return SeasonNumber == 0 && EpisodeNumber == 0; }
        }
    }
}