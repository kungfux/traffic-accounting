using System;
using System.Collections.Generic;

namespace Traffic_Accounting
{
    [Serializable]
    public class TrafficHistory
    {
        public sbyte WeekNumber = -1;
        public DateTime DateTime;
        public List<string> WebSite = new List<string>();
        public List<int> UsedTraffic = new List<int>();
        public TOP TOP = new TOP();
        public long TotalUsedTraffic = 0;
    }
}
