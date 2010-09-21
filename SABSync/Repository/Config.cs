using System;
using SubSonic.SqlGeneration.Schema;

namespace SABSync.Repository
{
    public class Config
    {
        [SubSonicPrimaryKey]
        public string Key
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }

       
    }
}
