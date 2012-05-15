using System;
using System.Collections.Generic;

namespace Traffic_Accounting
{
    [Serializable]
    public class TrafficHistory
    {
        public DateTime DateTime;
        public List<string> WebSite = new List<string>();
        public List<int> UsedTraffic = new List<int>();
        public long TotalUsedTraffic = 0;
    }
}
