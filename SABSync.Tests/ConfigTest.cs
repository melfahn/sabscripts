using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using NUnit.Framework;

namespace SABSync.Tests
{
    [TestFixture]
    public class ConfigTest : AssertionHelper
    {
        private Config Config { get; set; }

        public ConfigTest()
        {
            Config = new Config();
        }

        [Test]
        public void Feeds()
        {
            IList<FeedInfo> feeds = new List<FeedInfo>
            {
                new FeedInfo(name: "NzbMatrix", url: @"Feed.nzbmatrix.com.xml"),
                new FeedInfo(name: "Feed", url: @"Feed.xml"),
            };

            Expect(Config.Feeds, Is.EquivalentTo(feeds));
        }

        [Test]
        public void TvRootFolders()
        {
            var folders = new List<DirectoryInfo>
            {
                new DirectoryInfo(@"..\..\TV"),
                new DirectoryInfo(@"..\..\TV2"),
            };

            Expect(Config.TvRootFolders, Is.EquivalentTo(folders));
        }

        [Test]
        public void MyShows()
        {
            var shows = new[]
            {
                "24", 
                "The Mentalist", 
                "Bones", 
                "CSI- Crime Scene Investigation", 
                "The Daily Show with Jon Stewart"
            };

            Expect(Config.MyShows, Is.EquivalentTo(shows));
        }
    }
}