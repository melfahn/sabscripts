using System.IO;
using System.Xml.Linq;
using NUnit.Framework;

namespace SABSync.Tests
{
    [TestFixture]
    public class SabServiceTest : AssertionHelper
    {
        [Test]
        public void IsInHistory_ErrorResult_ReturnsFalse()
        {
            var input = new StringReader(
                @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
                    <result><status>False</status>
                        <error>API Key Required</error>
                    </result>");
            var sabRequest = new MockSabRequest { InputXml = XDocument.Load(input) };
            ISabService sabService = new SabService(new Config(), sabRequest);
            var episode = new Episode
            {
                ShowName = "Stargate Universe",
                SeasonNumber = 1,
                EpisodeNumber = 19
            };

            bool actual = sabService.IsInHistory(episode);

            Expect(actual, Is.False);
        }

        [Test]
        public void IsInHistory_InHistory_ReturnsTrue()
        {
            var sabRequest = new MockSabRequest {InputXml = XDocument.Load("SabHistory.xml")};
            ISabService sabService = new SabService(new Config(), sabRequest);
            var episode = new Episode
            {
                ShowName = "Stargate Universe",
                SeasonNumber = 1,
                EpisodeNumber = 19
            };

            bool actual = sabService.IsInHistory(episode);

            Expect(actual, Is.True);
        }

        [Test]
        public void IsInHistory_NotInHistory_ReturnsFalse()
        {
            var sabRequest = new MockSabRequest { InputXml = XDocument.Load("SabHistory.xml") };
            ISabService sabService = new SabService(new Config(), sabRequest);
            var episode = new Episode
            {
                ShowName = "Stargate Universe",
                SeasonNumber = 2,
                EpisodeNumber = 1
            };

            bool actual = sabService.IsInHistory(episode);

            Expect(actual, Is.False);
        }
    }

    public class MockSabRequest : ISabRequest
    {
        public XDocument InputXml { get; set; }

        #region ISabRequest Members

        public XDocument GetHistory()
        {
            return InputXml;
        }

        #endregion
    }
}