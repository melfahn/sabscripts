using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace SABSync.Tests
{
    [TestFixture]
    internal class FeedTest : AssertionHelper
    {
        private static IEnumerable<XElement> GetRssItems(string uri)
        {
            XElement rss = XElement.Load(uri);

            if (rss.Name != "rss")
                throw new Exception("Invalid RSS feed: no <rss> element found.");
            XElement channel = rss.Element("channel");

            if (channel == null)
                throw new Exception("Invalid RSS feed: no <channel> element found.");

            return channel.Elements("item");
        }

        [Test]
        public void Test1()
        {
            IEnumerable<string> titles =
                from item in GetRssItems(@"nzbs-rss.xml")
                select (string) item.Element("title");

            Console.WriteLine(titles.Count());
            foreach (string title in titles)
            {
                Console.WriteLine(title);
            }
        }
    }
}