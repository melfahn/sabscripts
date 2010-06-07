using System.Xml.Linq;
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
            Expect(job.MyShowsInFeedCount, Is.EqualTo(9), "MyShowsInFeedCount");
            Expect(job.RejectDownloadQualityCount, Is.EqualTo(1), "RejectDownloadQualityCount");
            Expect(job.RejectIgnoredSeasonCount, Is.EqualTo(1), "RejectIgnoredSeasonCount");
            Expect(job.RejectArchivedNzbCount, Is.EqualTo(1), "RejectArchivedNzbCount");
            Expect(job.RejectOnDiskCount, Is.EqualTo(1), "RejectOnDiskCount");
            Expect(job.RejectShowQualityCount, Is.EqualTo(1), "RejectShowQualityCount");
            Expect(job.RejectSabHistoryCount, Is.EqualTo(1), "RejectSabHistoryCount");
        }
    }

    public class MockTvDbService : ITvDbService
    {
        #region ITvDbService Members

        public void GetEpisodeName(Episode episode)
        {
            episode.EpisodeName = "Episode Name";
        }

        #endregion
    }

    public class MockSabService : ISabService
    {
        public MockSabService()
        {
            var sabRequest = new MockSabRequest { InputXml = XDocument.Load("SabHistory.xml") };
            Service = new SabService(new Config(), sabRequest);
        }

        public SabService Service { get; set; }

        #region ISabService Members

        public string AddByUrl(NzbInfo nzb)
        {
            return "ok - AddByUrl";
        }

        public bool IsInQueue(Episode episode)
        {
            return false;
        }

        public bool IsInHistory(Episode episode)
        {
            return Service.IsInHistory(episode);
        }

        #endregion
    }
}