using System;
using System.Text.RegularExpressions;

namespace Traffic_Accounting
{
    public class Traffic
    {
        //public string DailyStatUrl = "http://fw-br/squid/daily/[yyyy_MM_dd].html";
        public string DailyStatUrl = "http://192.168.1.5/[yyyy_MM_dd].html";
        public string DailyStatPattern = "<TR><TD ALIGN=LEFT>([a-zA-Z.]*)</TD><TD ALIGN=RIGHT>([0-9]*)</TD>";
        public string WeeklyStatUrl;
        public string DailyPattern;
        public DayOfWeek FirstDayOfTheWeek = DayOfWeek.Monday;
        public bool UseCache = true;
        public bool RoundUp = true;
        public bool UseFilter = false;

        private HttpRequest HttpRequest = new HttpRequest();
        private TrafficStatCache StatCache = new TrafficStatCache();
        private TrafficFilter TrafficFilter = new TrafficFilter();

        private enum TrafficEnumeration
        {
            B = 1,
            KB = 2,
            MB = 4,
            GB = 8
        }

        public TrafficStatDay getByDay(DateTime date)
        {
            if (UseCache && StatCache.searchDay(date) != -1)
            {
                return StatCache.getDay(date);
            }
            string html = HttpRequest.readUrl(prepareStatUrl(date, DailyStatUrl), true);
            TrafficStatDay stat = new TrafficStatDay();
            stat.Day = date;
            Regex regex = new Regex(DailyStatPattern);
            Match m = regex.Match(html);
            while (m.Success)
            {
                if (!UseFilter || UseFilter && !TrafficFilter.isInList(m.Groups[1].Value))
                {
                    stat.WebSites.Add(m.Groups[1].Value);
                    stat.SpentTraffic.Add(Convert.ToInt32(m.Groups[2].Value));
                    stat.TotalSpentTraffic += Convert.ToInt32(m.Groups[2].Value);
                }
                m = m.NextMatch();
            }
            if (UseCache)
            {
                StatCache.updateCache(stat);
            }
            return stat;
        }

        public TrafficStatWeek getByWeek(DateTime date)
        {
            TrafficStatWeek stat = new TrafficStatWeek();
            for (int a = 0; a <= 7; a++)
            {
                if (date.DayOfWeek == FirstDayOfTheWeek)
                {
                    a = 7;
                }
                TrafficStatDay day = getByDay(date);
                stat.TrafficStatOneDay.Add(day);
                stat.TotalSpentTraffic += day.TotalSpentTraffic;
                date = date.AddDays(-1);
            }
            return stat;
        }

        /// <summary>
        /// return traffic size and enum representation
        /// </summary>
        public long[] convertBytes(long bytes)
        {
            int value = 1024;
            if (!RoundUp) value = 10240; // 10 KB
            int enumeration = 1;
            while (bytes > value)
            {
                bytes = bytes / 1024;
                enumeration = enumeration * 2;
            }
            return new long[] { bytes, enumeration };
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
