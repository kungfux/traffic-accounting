using System;
using System.Text.RegularExpressions;

namespace Traffic_Accounting
{
    internal class Traffic
    {
        public bool LastOperationCompletedSuccessfully
        { 
            get; 
            private set; 
        }

        private HttpRequest HttpRequest = new HttpRequest();
        private CachedTrafficHistory StatCache = new CachedTrafficHistory();
        private TrafficFilter TrafficFilter = new TrafficFilter();

        private enum TrafficEnumeration
        {
            B = 1,
            KB = 2,
            MB = 4,
            GB = 8
        }

        public TrafficHistory getByDay(DateTime date)
        {
            LastOperationCompletedSuccessfully = true;

            if (ClientParams.Parameters.TrafficCacheEnabled && StatCache.searchDay(date) != -1)
            {
                // check is results already exists in cache
                return StatCache.getDay(date);
            }

            string html = HttpRequest.readUrl(prepareStatUrl(date, ClientParams.Parameters.TrafficStatUrl), true);
            if (HttpRequest.LastOperationCompletedSuccessfully)
            {
                // if there are no errors during reading statistic
                TrafficHistory stat = new TrafficHistory();
                stat.DateTime = date;
                Regex regex = new Regex(ClientParams.Parameters.TrafficStatPattern);
                Match m = regex.Match(html);
                while (m.Success)
                {
                    stat.WebSite.Add(m.Groups[1].Value);
                    stat.UsedTraffic.Add(Convert.ToInt32(m.Groups[2].Value));
                    if (!ClientParams.Parameters.TrafficFilterEnabled || 
                        ClientParams.Parameters.TrafficFilterEnabled && !TrafficFilter.isInList(m.Groups[1].Value))
                    {
                        // add value to total amount only in case
                        // filtering is disabled or site is not present in filter
                        stat.TotalUsedTraffic += Convert.ToInt32(m.Groups[2].Value);
                    }
                    m = m.NextMatch();
                }
                if (ClientParams.Parameters.TrafficCacheEnabled)
                {
                    StatCache.updateCache(stat);
                }
                return stat;
            }
            else
            {
                LastOperationCompletedSuccessfully = false;
                return new TrafficHistory();
            }
        }

        // return merged traffic info for week
        // starting from sirst day of week
        // and for day specified in date value
        public MergedTrafficHistory getByWeek(DateTime date)
        {
            LastOperationCompletedSuccessfully = true;
            MergedTrafficHistory stat = new MergedTrafficHistory();
            for (int a = 0; a <= 7; a++)
            {
                if (date.DayOfWeek == ClientParams.Parameters.FirstDayOfTheWeek)
                {
                    a = 7;
                }
                // skip request for today's date
                // 'coz there is no statistic for active day
                if (date.DayOfYear != DateTime.Now.DayOfYear)
                {
                    TrafficHistory day = getByDay(date);
                    if (!LastOperationCompletedSuccessfully)
                    {
                        // if error occur
                        break;
                    }
                    stat.TrafficHistory.Add(day);
                    stat.TotalUsedTraffic += day.TotalUsedTraffic;
                }
                date = date.AddDays(-1);
            }
            return stat;
        }

        /// <summary>
        /// return traffic size and enum representation
        /// </summary>
        public long[] convertBytes(long bytes)
        {
            return convertBytes(bytes, 1, 8);
        }

        public long[] convertBytes(long bytes, int minEnum, int maxEnum)
        {
            int value = 1024;
            if (!ClientParams.Parameters.TrafficRoundUp) value = 10240; // 10 KB
            int enumeration = 1;
            double traffic = bytes;
            while ((traffic > value && enumeration < maxEnum) || enumeration < minEnum)
            {
                traffic = traffic / 1024;
                enumeration = enumeration * 2;
            }
            return new long[] { (long)Math.Round(traffic,0), enumeration };
        }

        /// <summary>
        /// return traffic with size representation
        /// </summary>
        public string getConvertedBytes(long bytes)
        {
            long[] result = convertBytes(bytes);
            return string.Format("{0} {1}", result[0].ToString(), ((TrafficEnumeration)result[1]).ToString());
        }

        /// <summary>
        /// parse string and insert date time to input string
        /// </summary>
        private string prepareStatUrl(DateTime date, string Url)
        {
            string dateInUrl = Url.Substring(Url.IndexOf('[') + 1, Url.LastIndexOf(']') - Url.IndexOf('[') - 1);
            string insertedDate = date.ToString(dateInUrl);
            if (insertedDate.Contains("WW"))
            {
                insertedDate = insertedDate.Replace("WW", getWeek(date));
            }
            return Url.Replace("[" + dateInUrl + "]", insertedDate);
        }

        /// <summary>
        /// retrieve week number in double digits format
        /// </summary>
        private string getWeek(DateTime date)
        {
            int week = getWeekNumber(date);
            return string.Format("{0:00}", week.ToString());
        }

        /// <summary>
        /// calculate week number
        /// </summary>
        private int getWeekNumber(DateTime date)
        {
            int weeks = 1;
            DateTime s = new DateTime(DateTime.Today.Year, 1, 1);
            while (s < date)
            {
                if (s.DayOfWeek == DayOfWeek.Sunday)
                {
                    weeks++;
                    s = s.AddDays(7);
                }
                else
                {
                    s = s.AddDays(1);
                }
            }
            return weeks;
        }
    }
}
