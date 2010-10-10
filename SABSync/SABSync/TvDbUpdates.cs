using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SABSync
{
    public class TvDbUpdates
    {
        //Store Time, IEnumeranle<int> Series, IEnumerable<int> Episodes

        private List<int> _series;

        public List<int> Series
        {
            get { return _series; }
            set { _series = value; }
        }

        public int Time
        { get; set; }

    }
}
