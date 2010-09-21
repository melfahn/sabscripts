using System.Collections.Generic;

namespace SABSync.Services
{
    public interface IConfigController
    {
        List<string> GetTvRoots();
    }
}