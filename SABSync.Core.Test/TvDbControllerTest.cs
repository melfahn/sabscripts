using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using SABSync.Controllers;

namespace SABSync.Core.Test
{
    [TestFixture]
    public class TvDbControllerTest
    {
        [Test]
        [Row("The Simpsons")]
        [Row("Family Guy")]
        [Row("South Park")]
        public void TestSearch(string title)
        {
            var tvCont =new TvDbController();
            var result = tvCont.SearchSeries(title);

            Assert.AreNotEqual(0, result.Count);
            Assert.AreEqual(title, result[0].SeriesName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
