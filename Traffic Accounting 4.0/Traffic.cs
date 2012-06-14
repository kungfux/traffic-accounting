using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

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

            if (ClientParams.Parameters.TrafficCacheEnabled && StatCache.searchDay(date, true) != -1)
            {
                // check is results already exists in cache
                return StatCache.getDay(date);
            }
            string url = prepareStatUrl(date, ClientParams.Parameters.TrafficStatDailyUrl);
            string html = HttpRequest.readUrl(url);
            if (HttpRequest.LastOperationCompletedSuccessfully)
            {
                // if there are no errors during reading statistic
                TrafficHistory stat = new TrafficHistory();
                if (ClientParams.Parameters.TOPenabled)
                {
                    stat.TOP = getTOP(html);
                }
                html = HttpRequest.cutHtml(html);
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

        // return traffic info for week
        // starting from first day of week
        // and for day specified in date value
        public List<TrafficHistory> getByWeek(DateTime date)
        {
            LastOperationCompletedSuccessfully = true;

            List<TrafficHistory> stat = new List<TrafficHistory>();
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
                    stat.Add(day);
                }
                date = date.AddDays(-1);
            }
            return stat;
        }

        // return weekly traffic info for specified week
        // warning! avoid using local cache 'coz
        // we are using weekly stat instead of daily
        public TrafficHistory getByWeek(DateTime dayInWeek, bool fakeParam)
        {
            LastOperationCompletedSuccessfully = true;

            if (ClientParams.Parameters.TrafficCacheEnabled && StatCache.searchDay(dayInWeek, false) != -1)
            {
                // check is results already exists in cache
                return StatCache.getWeek(dayInWeek);
            }
            string url = prepareStatUrl(dayInWeek, ClientParams.Parameters.TrafficStatWeeklyUrl);
            string html = HttpRequest.readUrl(url);
            if (HttpRequest.LastOperationCompletedSuccessfully)
            {
                // if there are no errors during reading statistic
                TrafficHistory stat = new TrafficHistory();
                stat.WeekNumber = getWeekNumber(dayInWeek);
                if (ClientParams.Parameters.TOPenabled)
                {
                    stat.TOP = getTOP(html);
                }
                html = HttpRequest.cutHtml(html);
                //stat.DateTime = date;
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
        private sbyte getWeekNumber(DateTime date)
        {
            sbyte weeks = 1;
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

        // return user position in TOP
        public TOP getTOP(string html)
        {
            TOP t = new TOP();
            t.MaxPositions = 0;
            t.Position = 0;
            Regex regex = new Regex(ClientParams.Parameters.TrafficTopPattern);
            Match m = regex.Match(html);
            while (m.Success)
            {
                // count positions available
                t.MaxPositions++;
                // check position of user
                if (m.Captures.Count > 0)
                {
                    if (m.Captures[0].Value.Contains(Environment.MachineName.ToLower()))
                    {
                        t.Position = t.MaxPositions;
                    }
                }
                // next iteration
                m = m.NextMatch();
            }
            return t;
        }

        // calculate % user will be in the top
        public int getFortuneTelling()
        {
            int result = 0;
            int[] days = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
            List<TrafficHistory> history = getByWeek(DateTime.Now);
            foreach (TrafficHistory record in history)
            {
                days[history.IndexOf(record)] = record.TOP.Position;
            }
            foreach (int record in days)
            {
                if (record >= 1 && record <= 10)
                {
                    result = (result + 100) / 2;
                }
            }
            return result;
        }
    }
}
