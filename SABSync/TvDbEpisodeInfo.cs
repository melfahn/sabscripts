using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SABSync
{
    public class TvDbEpisodeInfo
    {
        public int EpisodeId
        { get; set; }

        public int SeasonNumber
        { get; set; }

        public int EpisodeNumber
        { get; set; }

        public string EpisodeName
        { get; set; }

        public string FirstAired
        { get; set; }

        public string Overview
        { get; set; }
    }
}
