using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SABSync.Tests
{
    [TestFixture]
    public class NzbInfoTest : AssertionHelper
    {
        // TODO: redo these tests once IsValidQuality is added to handle both DownloadQuality and ShowQuality

        //private readonly List<NzbSite> _sites = new List<NzbSite>
        //{
        //    new NzbSite {Name = "nzbsDotOrg", Url = "nzbs.org", Pattern = @"\d{5,10}"},
        //    new NzbSite {Name = "tvnzb", Url = "tvnzb.com", Pattern = @"\d{5,10}"},
        //    new NzbSite {Name = "nzbmatrix", Url = "nzbmatrix.com", Pattern = @"\d{6,10}"},
        //    new NzbSite {Name = "nzbsrus", Url = "nzbsrus.com", Pattern = @"\d{6,10}"},
        //};

        //private NzbSite TvNzbSite
        //{
        //    get { return _sites.Single(s => s.Name == "tvnzb"); }
        //}

        //private NzbSite NzbMatrixSite
        //{
        //    get { return _sites.Single(s => s.Name == "nzbmatrix"); }
        //}

        //private readonly Config _xvid = new Config {DownloadQualities = new[] {"xvid"}};
        //private readonly Config _xvid720P = new Config {DownloadQualities = new[] {"xvid", "720p"}};

        //[Test]
        //public void IsValidQuality_SiteDoesNotUseQuality_ReturnsTrue()
        //{
        //    var nzb = new NzbInfo(_xvid)
        //    {
        //        Site = NzbMatrixSite,
        //        Title = "Show S01E01 Episode 720p",
        //    };

        //    Expect(nzb.IsValidFeedQuality(), Is.True);
        //}

        //[Test]
        //public void IsValidQuality_SiteNullInvalidQuality_ReturnsFalse()
        //{
        //    var nzb = new NzbInfo(_xvid)
        //    {
        //        Site = null,
        //        Title = "Show S01E01 Episode 720p",
        //    };

        //    Expect(nzb.IsValidFeedQuality(), Is.False);
        //}

        //[Test]
        //public void IsValidQuality_SiteNullValidQuality_ReturnsTrue()
        //{
        //    var nzb = new NzbInfo(_xvid)
        //    {
        //        Site = null,
        //        Title = "Show S01E01 Episode Xvid",
        //    };

        //    Expect(nzb.IsValidFeedQuality(), Is.True);
        //}

        //[Test]
        //public void IsValidQuality_SiteUsesQualityInvalidQuality_ReturnsFalse()
        //{
        //    var nzb = new NzbInfo(_xvid)
        //    {
        //        Site = TvNzbSite,
        //        Title = "Show S01E01 Episode 720p",
        //    };

        //    Expect(nzb.IsValidFeedQuality(), Is.False);
        //}

        //[Test]
        //public void IsValidQuality_SiteUsesQualityMultiQualityValidQuality_ReturnsTrue()
        //{
        //    var nzb = new NzbInfo(_xvid720P)
        //    {
        //        Site = TvNzbSite,
        //        Title = "Show S01E01 Episode 720p",
        //    };

        //    Expect(nzb.IsValidFeedQuality(), Is.True);
        //}

        //[Test]
        //public void IsValidQuality_SiteUsesQualityValidQuality_ReturnsTrue()
        //{
        //    var nzb = new NzbInfo(_xvid)
        //    {
        //        Site = TvNzbSite,
        //        Title = "Show S01E01 Episode Xvid",
        //    };

        //    Expect(nzb.IsValidFeedQuality(), Is.True);
        //}
    }
}