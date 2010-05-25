using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SABSync
{
    public class NzbSite
    {
        private static readonly IList<NzbSite> Sites = new List<NzbSite>
        {
            new NzbSite {Name = "newzbin", Url = "newzbin.com", Pattern = @"\d{7,10}"},
            new NzbSite {Name = "nzbmatrix", Url = "nzbmatrix.com", Pattern = @"\d{6,10}"},
            new NzbSite {Name = "nzbsDotOrg", Url = "nzbs.org", Pattern = @"\d{5,10}"},
            new NzbSite {Name = "nzbsrus", Url = "nzbsrus.com", Pattern = @"\d{6,10}"},
            new NzbSite {Name = "tvnzb", Url = "tvnzb.com", Pattern = @"\d{5,10}"},
            new NzbSite {Name = "lilx", Url = "lilx.net", Pattern = @"\d{6,10}"}
        };

        public string Name { get; set; }
        public string Pattern { get; set; }
        public string Url { get; set; }

        // TODO: use HttpUtility.ParseQueryString();
        // https://nzbmatrix.com/api-nzb-download.php?id=626526
        public string ParseId(string url)
        {
            return Regex.Match(url, Pattern).Value;
        }

        public static NzbSite Parse(string url)
        {
            return Sites.Where(site => url.Contains(site.Url)).SingleOrDefault() ??
                new NzbSite {Name = "unknown", Pattern = @"\d{6,10}"};
        }
    }
}