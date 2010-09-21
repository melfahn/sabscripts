using System.Linq;
using SABSync.Repository;

namespace SABSync.Controllers
{
    public interface ISeriesController
    {
        IQueryable<Series> GetSeries();
        void SyncSeriesWithDisk();
    }
}