using System.Collections.Generic;
namespace Traffic_Accounting
{
    public class TrafficStatWeek
    {
        public List<TrafficStatDay> TrafficStatOneDay = new List<TrafficStatDay>();
        public long TotalSpentTraffic = 0;
    }
}
