using System;
using System.Collections.Generic;

namespace Traffic_Accounting
{
    [Serializable]
    public class TrafficStatDay
    {
        public DateTime Day;
        public List<string> WebSites = new List<string>();
        public List<int> SpentTraffic = new List<int>();
        public long TotalSpentTraffic = 0;
    }
}
