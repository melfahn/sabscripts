using System.Collections.Generic;

namespace SABSync.Services
{
    internal interface IConfigController
    {
        List<string> GetTvRoots();
    }
}