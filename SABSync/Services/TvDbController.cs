using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TvdbLib;
using TvdbLib.Cache;
using System.Windows.Forms;
using TvdbLib.Data;

namespace SABSync.Services
{
    class TvDbController
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
