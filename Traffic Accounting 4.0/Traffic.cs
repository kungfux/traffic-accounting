using System;
using System.Text.RegularExpressions;

namespace Traffic_Accounting
{
    public class Traffic
    {
        // for testing
        //public string DailyStatUrl = "http://192.168.1.5/[yyyy_MM_dd].html";
        //
        private string DailyStatUrl = "http://fw-br/squid/daily/[yyyy_MM_dd].html";
        private string DailyStatPattern = @"<TR><TD ALIGN=LEFT>([\S.]*)</TD><TD ALIGN=RIGHT>([0-9]*)</TD>";
        private DayOfWeek FirstDayOfTheWeek = DayOfWeek.Monday;
        private bool UseCache = true;
        private bool RoundUp = false;
        private bool UseFilter = false;
        public bool LastOperationCompletedSuccessfully
        { 
            get; 
            private set; 
        }

        private HttpRequest HttpRequest = new HttpRequest();
        private CachedTrafficHistory StatCache = new CachedTrafficHistory();
        private TrafficFilter TrafficFilter = new TrafficFilter();

        public Traffic()
        {
            ClientParams p = new ClientParams();
            DailyStatUrl = p.Parameters.TrafficStatUrl;
            DailyStatPattern = p.Parameters.TrafficStatPattern;
            FirstDayOfTheWeek = p.Parameters.FirstDayOfTheWeek;
            UseCache = p.Parameters.TrafficCacheEnabled;
            UseFilter = p.Parameters.TrafficFilterEnabled;
            StatCache.CacheSize = p.Parameters.TrafficCacheSize;
            TrafficFilter.addFormattedList(p.Parameters.TrafficSeparatedFilterList);
        }

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

            if (UseCache && StatCache.searchDay(date) != -1)
            {
                // check is results already exists in cache
                return StatCache.getDay(date);
            }

            string html = HttpRequest.readUrl(prepareStatUrl(date, DailyStatUrl), true);
            if (HttpRequest.LastOperationCompletedSuccessfully)
            {
                // if there are no errors during reading statistic
                TrafficHistory stat = new TrafficHistory();
                stat.DateTime = date;
                Regex regex = new Regex(DailyStatPattern);
                Match m = regex.Match(html);
                while (m.Success)
                {
                    stat.WebSite.Add(m.Groups[1].Value);
                    stat.UsedTraffic.Add(Convert.ToInt32(m.Groups[2].Value));
                    if (!UseFilter || UseFilter && !TrafficFilter.isInList(m.Groups[1].Value))
                    {
                        // add value to total amount only in case
                        // filtering is disabled or site is not present in filter
                        stat.TotalUsedTraffic += Convert.ToInt32(m.Groups[2].Value);
                    }
                    m = m.NextMatch();
                }
                if (UseCache)
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

        public MergedTrafficHistory getByWeek(DateTime date)
        {
            LastOperationCompletedSuccessfully = true;
            MergedTrafficHistory stat = new MergedTrafficHistory();
            for (int a = 0; a <= 7; a++)
            {
                if (date.DayOfWeek == FirstDayOfTheWeek)
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
            if (!RoundUp) value = 10240; // 10 KB
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
            if (week <= 9)
            {
                return string.Concat("0", week.ToString());
            }
            else
            {
                return week.ToString();
            }
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
