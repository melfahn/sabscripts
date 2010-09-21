using System.Collections.Generic;
using System.IO;
using TvdbLib;
using TvdbLib.Cache;
using System.Windows.Forms;
using TvdbLib.Data;

namespace SABSync.Controllers
{
    public class TvDbController : ITvDbController
    {
        private readonly TvdbLib.TvdbHandler _handler;
        private const string TvDbApiKey = "5D2D188E86E07F4F";
        public TvDbController()
        {
            _handler = new TvdbHandler(new XmlCacheProvider(Path.Combine(Application.StartupPath, @"\tvdbcache.xml")), TvDbApiKey);
        }

        public List<TvdbSearchResult> SearchSeries(string name)
        {
            return _handler.SearchSeries(name);
        }

    }
}
