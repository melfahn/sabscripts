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

            Expect(job.AcceptCount, Is.EqualTo(3), "AcceptCount");
            Expect(job.FeedItemCount, Is.EqualTo(20), "FeedItemCount");
            Expect(job.MyFeedsCount, Is.EqualTo(2), "MyFeedsCount");
            Expect(job.MyShowsCount, Is.EqualTo(5), "MyShowsCount");
            Expect(job.RejectPasswordedCount, Is.EqualTo(2), "RejectPasswordedCount");
            Expect(job.MyShowsInFeedCount, Is.EqualTo(8), "MyShowsInFeedCount");
            Expect(job.RejectDownloadQualityCount, Is.EqualTo(1), "RejectDownloadQualityCount");
            Expect(job.RejectIgnoredSeasonCount, Is.EqualTo(1), "RejectIgnoredSeasonCount");
            Expect(job.RejectInNzbArchive, Is.EqualTo(1), "RejectInNzbArchive");
            Expect(job.RejectOnDiskCount, Is.EqualTo(1), "RejectOnDiskCount");
            Expect(job.RejectShowQualityCount, Is.EqualTo(1), "RejectShowQualityCount");
        }
    }

    public class MockTvDbService : ITvDbService
    {
        #region ITvDbService Members

        public void CheckTvDb(Episode episode)
        {
            episode.Name = "Episode Name";
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