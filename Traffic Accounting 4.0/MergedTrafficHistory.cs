using System.Collections.Generic;
namespace Traffic_Accounting
{
    internal class MergedTrafficHistory
    {
        public List<TrafficHistory> TrafficHistory = new List<TrafficHistory>();
        public long TotalUsedTraffic = 0;
    }
}
