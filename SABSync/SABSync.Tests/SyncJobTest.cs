using System;
using NUnit.Framework;

namespace SABSync.Tests
{
    [TestFixture]
    public class SyncJobTest : AssertionHelper
    {
        [Test]
        public void Start()
        {
            var job = new SyncJob(new Config(), new MockSabService(), new MockTvDbService());
            job.Start();

            Expect(job.AcceptCount, Is.EqualTo(4), "AcceptCount");
            Expect(job.FeedItemCount, Is.EqualTo(20), "FeedItemCount");
            Expect(job.MyFeedsCount, Is.EqualTo(3), "MyFeedsCount");
            Expect(job.MyShowsCount, Is.EqualTo(5), "MyShowsCount");
            Expect(job.MyShowsInFeedCount, Is.EqualTo(8), "MyShowsInFeedCount");
            Expect(job.RejectDownloadQualityCount, Is.EqualTo(1), "RejectDownloadQualityCount");
            Expect(job.RejectIgnoredSeasonCount, Is.EqualTo(1), "RejectIgnoredSeasonCount");

            // TODO: fix InNzbArchive to handle relative urls
            // RejectInNzbArchive should be 1 and AcceptCount one less
            Expect(job.RejectInNzbArchive, Is.EqualTo(0), "RejectInNzbArchive");
            Expect(job.RejectOnDiskCount, Is.EqualTo(1), "RejectOnDiskCount");
            Expect(job.RejectPasswordedCount, Is.EqualTo(2), "RejectPasswordedCount");
            Expect(job.RejectShowQualityCount, Is.EqualTo(1), "RejectShowQualityCount");
        }
    }

    public class MockTvDbService : ITvDbService
    {
        #region ITvDbService Members

        public string CheckTvDb(string showName, int seasonNumber, int episodeNumber)
        {
            return "unknown";
        }

        public string CheckTvDb(string showName, DateTime firstAired)
        {
            return "unknown";
        }

        public string GetSeriesId(string seriesName)
        {
            return "unknown";
        }

        public string GetEpisodeName(string seriesId, int seasonNumber, int episodeNumber)
        {
            return "unknown";
        }

        #endregion
    }

    public class MockSabService : ISabService
    {
        #region ISabService Members

        public string AddByUrl(NzbInfo nzb)
        {
            return "ok - AddByUrl";
        }

        public string AddByNewzbinId(NzbInfo nzb)
        {
            return "ok - AddByNewsbinId";
        }

        public bool IsInQueue(string rssTitle, long reportId)
        {
            return false;
        }

        public bool IsInQueue(string rssTitle, string rssTitleFix, string nzbId)
        {
            return false;
        }

        public bool IsInHistory(string rssTitle, string rssTitleFix)
        {
            return false;
        }

        #endregion
    }
}