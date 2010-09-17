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
        private List<int> _episodes;

        public List<int> Series
        {
            get { return _series; }
            set { _series = value; }
        }

        public List<int> Episodes
        {
            get { return _episodes; }
            set { _episodes = value; }
        }

        public int Time
        { get; set; }

    }
}
