using System;
using NUnit.Framework;

namespace SABSync.Tests
{
    [TestFixture]
    public class TvDbServiceTest : AssertionHelper
    {
        [Test]
        [Ignore("Uses live TheTvDb service")]
        public void CheckTvDb()
        {
            //http://thetvdb.com/api/GetSeries.php?seriesname=The+Daily+Show
            //http://thetvdb.com/api/5D2D188E86E07F4F/series/71256/default/15/64
            // "The Daily Show 2010 05 06 Mario Batali";
            var episode = new Episode
            {
                ShowName = "The Daily Show",
                AirDate = DateTime.Parse("2010-05-06"),
            };
            var service = new TvDbService();

            service.GetEpisodeName(episode);

            Expect(episode.EpisodeName, Is.EqualTo("Mario Batali"));
        }
    }
}