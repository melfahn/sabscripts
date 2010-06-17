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
        [Test]
        public void Test1()
        {
            XElement rssFeed = XElement.Load(@"nzbs-rss.xml");

            IEnumerable<string> items =
                from item in rssFeed.Elements("channel").Elements("item")
                select (string) item.Element("title");

            Console.WriteLine(items.Count());
            foreach (string o in items)
            {
                Console.WriteLine(o);
            }
        }
    }
}