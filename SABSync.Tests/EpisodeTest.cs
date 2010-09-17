using NUnit.Framework;
using SABSync.Entities;

namespace SABSync.Tests
{
    [TestFixture]
    public class EpisodeTest : AssertionHelper
    {
        // ReSharper disable InconsistentNaming
        [Test]
        public void Parse_SEMatch_ShowName()
        {
            Episode e = GetSEMatch();

            Expect(e.ShowName, Is.EqualTo("Junior Apprentice"));
        }

        [Test]
        public void Parse_SEMatch_SeasonNumber()
        {
            Episode e = GetSEMatch();

            Expect(e.SeasonNumber, Is.EqualTo(1));
        }

        [Test]
        public void Parse_SEMatch_EpisodeNumber()
        {
            Episode e = GetSEMatch();

            Expect(e.EpisodeNumber, Is.EqualTo(5));
        }

        [Test]
        public void Parse_SEMatch_EpisodeTitle()
        {
            Episode e = GetSEMatch();

            Expect(e.EpisodeTitle, Is.EqualTo("1x05"));
        }

        [Test]
        public void Parse_SEMatch_EpisodeName()
        {
            Episode e = GetSEMatch();

            Expect(e.EpisodeName, Is.EqualTo(string.Empty));
        }

        private static Episode GetSEMatch()
        {
            var item = new FeedItem
            {
                Title = "Junior.Apprentice.S01E05.720p.HDTV.x264-ANGELiC"
            };

            return Episode.Parse(item);
        }

        [Test]
        public void Parse_XMatch_ShowName()
        {
            Episode e = GetXMatch();

            Expect(e.ShowName, Is.EqualTo("Father And Son"));
        }

        [Test]
        public void Parse_XMatch_SeasonNumber()
        {
            Episode e = GetXMatch();

            Expect(e.SeasonNumber, Is.EqualTo(4));
        }

        [Test]
        public void Parse_XMatch_EpisodeNumber()
        {
            Episode e = GetXMatch();

            Expect(e.EpisodeNumber, Is.EqualTo(3));
        }

        [Test]
        public void Parse_XMatch_EpisodeTitle()
        {
            Episode e = GetXMatch();

            Expect(e.EpisodeTitle, Is.EqualTo("4x03"));
        }

        [Test]
        public void Parse_XMatch_EpisodeName()
        {
            Episode e = GetSEMatch();

            Expect(e.EpisodeName, Is.EqualTo(string.Empty));
        }

        private static Episode GetXMatch()
        {
            var item = new FeedItem
            {
                Title = "Father And Son.4x03.720p HDTV x264-FoV"
            };

            return Episode.Parse(item);
        }

        [Test]
        public void Parse_HasEpisodeName_EpisodeName()
        {
            var item = new FeedItem
            {
                Title = "The.First.48.S06E16.Motel.No.Tell.Brotherly.Love.PROPER.HDTV.XviD-MOMENTUM"
            };

            Episode e = Episode.Parse(item);

            Expect(e.EpisodeName, Is.EqualTo("Motel No Tell Brotherly Love"));
        }

        [Test]
        public void Parse_HasRelease_Release()
        {
            var item = new FeedItem
            {
                Title = "The.First.48.S06E16.Motel.No.Tell.Brotherly.Love.PROPER.HDTV.XviD-MOMENTUM"
            };

            Episode e = Episode.Parse(item);

            Expect(e.Release, Is.EqualTo("PROPER HDTV XviD MOMENTUM"));
        }
    }
}