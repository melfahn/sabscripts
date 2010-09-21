using System.Collections.Generic;
using TvdbLib.Data;

namespace SABSync.Controllers
{
    public interface ITvDbController
    {
        List<TvdbSearchResult> SearchSeries(string name);
    }
}